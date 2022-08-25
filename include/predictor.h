#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdbool.h>

#include <osu.h>

typedef struct {
    unsigned int x;
    unsigned int time;
    bool bezier;
} Point;

typedef struct {
    unsigned int start;
    unsigned int end;
    unsigned int length;
    unsigned int distance;
    bool invert;
    Point *points;
    unsigned int points_len;
} Predictor;

extern Predictor predictor;

void predictor_run(void);

#endif