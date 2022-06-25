// Ported code from https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/MathUtils/FastRandom.cs
// All credit goes to peppy and team
#ifndef FASTRANDOM_H
#define FASTRANDOM_H

#include <stdbool.h>
#include <limits.h>

typedef struct {
    unsigned int x, y, z, w, bitBuffer;
    int bitIndex;
} fastrandom_struct;
fastrandom_struct fastrandom_FastRandom(int);
unsigned int fastrandom_NextUInt(fastrandom_struct *);
int fastrandom_Next(fastrandom_struct *);
double fastrandom_NextDouble(fastrandom_struct *);
int fastrandom_NextUpper(fastrandom_struct *, int);
int fastrandom_NextLowerUpper(fastrandom_struct *, int, int);
int fastrandom_NextDoubleLowerUpper(fastrandom_struct *, double, double);
bool fastrandom_NextBool(fastrandom_struct *);

#endif