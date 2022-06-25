// Ported code from https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/MathUtils/FastRandom.cs
// All credit goes to peppy and team
#ifndef FASTRANDOM_H
#define FASTRANDOM_H

#include <stdbool.h>
#include <limits.h>

typedef struct {
    unsigned int x;
    unsigned int y;
    unsigned int z;
    unsigned int w;
    unsigned int bitBuffer;
    int bitIndex;
} FastRandom;

FastRandom fastrandom_FastRandom(int);
unsigned int fastrandom_NextUInt(FastRandom *);
int fastrandom_Next(FastRandom *);
double fastrandom_NextDouble(FastRandom *);
int fastrandom_NextUpper(FastRandom *, int);
int fastrandom_NextLowerUpper(FastRandom *, int, int);
int fastrandom_NextDoubleLowerUpper(FastRandom *, double, double);
bool fastrandom_NextBool(FastRandom *);

#endif