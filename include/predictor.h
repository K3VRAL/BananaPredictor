#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <limits.h>

#include <osu.h>

typedef struct Point {
    float x;
    float time;
} Point;

typedef struct Line {
    double a;
    double b;
    double c;
} Line;

typedef struct Predictor {
    char *beatmap;
    Point *points;
    unsigned int points_len;
} Predictor;

extern Predictor predictor;

/*
    The BananaPredictor running and evaluating based off of the inputs given
    
    return
        void
*/
void predictor_run(void);

// Basing below functions off of https://stackoverflow.com/a/20679579 because I suck at math

/*
    Calculating the Line using Cramer's Rule

    return
        void
    argsreturn
        Line *
    args
        Point
        Point
*/
void predictor_line(Line *, Point, Point);

/*
    Calculating the Line of Intersection with Determinants

    return
        void
    argsreturn
        Point ** - NULLABLE
    args
        Line
        Line
*/
void predictor_intersection(Point **, Line, Line);

#endif