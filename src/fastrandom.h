// Ported code from https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/MathUtils/FastRandom.cs
// All credit goes to peppy and team
#ifndef FASTRANDOM_H
#define FASTRANDOM_H

#include <stdbool.h>
#include <limits.h>

void FastRandom(int);
unsigned int NextUInt();
int Next();
double NextDouble();
int NextUpper(int);
int NextLowerUpper(int, int);
int NextDoubleLowerUpper(double, double);
bool NextBool();

#endif