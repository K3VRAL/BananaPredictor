#ifndef OBJECTS_H
#define OBJECTS_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

#include "print.h"

typedef struct allTP {
    int time, meter, sset, sindex, volume, effects;
    float beatlength;
    bool uninherited;
} allTP;

typedef struct HOSlider {
    // TODO curveType|curvePoints, edgeSounds, edgeSets
    int slides;
    double length;
} HOSlider;
typedef struct HOSpinner {
    int endtime;
} HOSpinner;

typedef union HO {
    HOSlider slider;
    HOSpinner spinner;
} HO;
typedef enum Types {
    circle,
    slider,
    spinner
} Types;

typedef struct allHO {
    int x, y, time, hsound, *hsample;
    bool ncombo;
    Types type;
    HO hobject;
} allHO;

typedef union Tag {
    listAll inp;
    allHO aho;
    allTP atp;
} Tag;
typedef enum TagID {
    inp,
    atp,
    aho
} TagID;

typedef struct Node {
    struct Node *next;
    TagID tagid;
    Tag tag;
} Node;

int ll_length(Node *);
void ll_add(Node **, TagID, Tag);
void ll_remove(Node **, int);
Node *ll_get(Node *, int);
void ll_sort(Node **);
void ll_free(Node *);

typedef struct Objects {
    char *tFile, *oFile;
    float sliderMultiplier, sliderTickrate;
    int lineTimingPoints, lineHitObjects;
    Node *llTP, *llHO;
} Objects;

#endif