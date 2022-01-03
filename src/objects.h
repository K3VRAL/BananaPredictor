#ifndef OBJECTS_H
#define OBJECTS_H

#include <stdio.h>
#include <stdbool.h>

typedef enum {
    circle = 0,
    slider = 1,
    spinner = 2
} types;

typedef struct {
    char *line;
    size_t fileline;

    int time, meter, sset, sindex, volume, effects;
    float beatLength;
    bool uninherited;
} allTP;

typedef struct {
    char *line;
    size_t fileline;

    // Hit Object/Cirlce
    int tlen;
    types type;
    int x, y, time, hsound, *hsample;
    bool ncombo;

    // Slider
    // TODO curveType|curvePoints, edgeSounds, edgeSets
    int slides;
    double length;
    
    // Spinner
    int endtime;
} allHO;

typedef struct {
    char *tFile;
    char *oFile;
    float smultiplier, stickrate;
    allTP *atp;
    size_t numAtp;
    allHO *aho;
    size_t numAho;
} objects;

char *etsType(types typ);

#endif