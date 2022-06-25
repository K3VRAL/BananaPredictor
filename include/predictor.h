#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdbool.h>

typedef struct {
    unsigned int x;
    unsigned int time;
    bool bezier;
} Point;

typedef struct {
    unsigned int start;
    unsigned int length;
    unsigned int shower_length;
    unsigned int shower_distance;
    bool invert;
    Point *points;
    unsigned int points_len;
} Predictor;

extern Predictor predictor;

#endif