// Ported code from https://github.com/ppy/osu/blob/master/osu.Game.Rulesets.Catch/MathUtils/FastRandom.cs
// All credit goes to peppy and team
#ifndef FASTRANDOM_H
#define FASTRANDOM_H

#include <stdbool.h>
#include <limits.h>

void FastRandom(int seed);
unsigned int NextUInt();
int Next();
double NextDouble();
int NextUpper(int upperBound);
int NextLowerUpper(int lowerBound, int upperBound);
int NextDoubleLowerUpper(double lowerBound, double upperBound);
bool NextBool();

#endif