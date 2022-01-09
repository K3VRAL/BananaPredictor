#ifndef OBJECTS_H
#define OBJECTS_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

typedef enum Types {
    circle,
    slider,
    spinner
} Types;

char *getType(Types type);

typedef struct allTP {
    int time, meter, sset, sindex, volume, effects;
    float beatlength;
    bool uninherited;
} allTP;

typedef struct HOCircle {} HOCircle;
typedef struct HOSlider {
    // TODO curveType|curvePoints, edgeSounds, edgeSets
    int slides;
    double length;
} HOSlider;
typedef struct HOSpinner {
    int endtime;
} HOSpinner;
typedef union HO {
    HOCircle circle;
    HOSlider slider;
    HOSpinner spinner;
} HO;
typedef struct allHO {
    int x, y, time, hsound, *hsample;
    bool ncombo;
    Types type;
    HO hobject;
} allHO;
typedef union Tag {
    allHO aho;
    allTP atp;
} Tag;

typedef enum TagID {
    atp,
    aho
} TagID;
typedef struct Node {
    struct Node *next;
    TagID tagid;
    Tag tag;
} Node;

int ll_length(Node *head);
Node *ll_add(Node *head, TagID tagid, Tag tag);
Node *ll_remove(Node *head, int key);
Node *ll_get(Node *head, int key);
Node *ll_sort(Node *head, char *delim);
Node *ll_free(Node *head);

typedef struct Objects {
    char *tFile, *oFile;
    int lineTimingPoints, lineHitObjects;
    Node *llTP, *llHO;
    float sliderMultiplier, sliderTickrate;
} Objects;

#endif