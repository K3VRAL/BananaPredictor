#include "../include/fastrandom.h"

const double int_to_real = 1.0 / (INT_MAX + 1.0);
const unsigned int int_mask = 0x7FFFFFFF;
const unsigned int y_initial = 842502087;
const unsigned int z_initial = 3579807591;
const unsigned int w_initial = 273326509;
unsigned int x, y = y_initial, z = z_initial, w = w_initial;

void FastRandom(int seed) {
    x = (unsigned int)seed;
}

unsigned int NextUInt() {
    unsigned int t = x ^ (x << 11);
    x = y;
    y = z;
    z = w;
    return w = w ^ (w >> 19) ^ t ^ (t >> 8);
}

int Next() {
    return (int)(int_mask & NextUInt());
}

double NextDouble() {
    return int_to_real * Next();
}

int NextUpper(int upperBound) {
    return (int)(NextDouble() * upperBound);
}

int NextLowerUpper(int lowerBound, int upperBound) {
    return (int)(lowerBound + NextDouble() * (upperBound - lowerBound));
}

int NextDoubleLowerUpper(double lowerBound, double upperBound) {
    return (int)(lowerBound + NextDouble() * (upperBound - lowerBound));
}

unsigned int bitBuffer;
int bitIndex = 32;

bool NextBool() {
    if (bitIndex == 32) {
        bitBuffer = NextUInt();
        bitIndex = 1;
        return (bitBuffer & 1) == 1;
    }
    bitIndex++;
    return ((bitBuffer >>= 1) & 1) == 1;
}