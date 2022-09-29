#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <limits.h>
#include <unistd.h>
#include <sys/ioctl.h>

#include <osu.h>

typedef struct XLine {
    unsigned short *areas;
    unsigned int len;
} XLine;

typedef struct Coefficient {
    double a;
    double b;
    double c;
} Coefficient;

typedef struct Point {
    float x;
    float time;
} Point;

typedef struct Shape {
    Point *points;
    unsigned int len;
} Shape;

typedef struct Predictor {
    FILE *output;
    bool prefer_circles;
    FILE *beatmap;
    Shape *shapes;
    unsigned int shapes_len;
    double distance;
} Predictor;

extern Predictor predictor;

/*
    The BananaPredictor running and evaluating based off of the inputs given
    
    return
        void
*/
void predictor_main(void);

/*
    Sets the values for the points to start and stop the main loop

    return
        void
    argsreturn
        int *
        int *
*/
void predictor_points(int *, int *);

/*
    Sets the values of the beatmap and the catch objects

    return
        void
    argsreturn
        CatchHitObject **
        unsigned int *
        Beatmap *
    args
        int
*/
void predictor_beatmap(CatchHitObject **, unsigned int *, Beatmap *, int);

/*
    Prints out the progress bar in the terminal. Resizing the terminal will also resize the output

    return
        void
    args
        unsigned int
*/
void predictor_progressbar(unsigned int);

// Basing below functions off of https://stackoverflow.com/a/20679579 because I suck at math
/*
    Calculate the intersections of each line and whether they should be used for calculation

    return
        void
    argsreturn
        unsigned short **
        unsigned int *
    args
        int
*/
void predictor_areas(XLine **, unsigned int *, int);

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
void predictor_line(Coefficient *, Point, Point);

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
void predictor_intersection(Point **, Coefficient, Coefficient);
// End of stackoverflow

/*
    Either we create a new Juice Stream and have a minimum of 3 nested objects; or if the previously created Juice Stream is not long enough then we can keep expanding it

    return
        void
    argsreturn
        CatchHitObject **
        unsigned int *
    args
        int
        int
        Beatmap
*/
void predictor_generatejs(CatchHitObject **, unsigned int *, int, int, Beatmap);

/*
    Returns if we need to retry or store the current positions of the objects

    return
        bool
    args
        unsigned short *
        unsigned int
        CatchHitObject *
        unsigned int
*/
bool predictor_breakout(XLine *, unsigned int, CatchHitObject *, unsigned int);

/*
    Stores the objects and amount of them including the new rng for future reference

    return
        void
    argsreturn
        CatchHitObject **
        unsigned int *
        LegacyRandom *
    args
        LegacyRandom
        CatchHitObject *
        unsigned int
*/
void predictor_storeobjects(CatchHitObject **, unsigned int *, LegacyRandom *, CatchHitObject *, unsigned int, LegacyRandom);

/*
    Outputs data

    return
        void
    args
        CatchHitObject *
        unsigned int
*/
void predictor_output(CatchHitObject *, unsigned int);

#endif