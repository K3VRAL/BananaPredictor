#include "../include/fastrandom.h"

const double int_to_real = 1.0 / (INT_MAX + 1.0);
const unsigned int int_mask = 0x7FFFFFFF;
const unsigned int y_initial = 842502087;
const unsigned int z_initial = 3579807591;
const unsigned int w_initial = 273326509;

fastrandom_struct fastrandom_FastRandom(int seed) {
    fastrandom_struct frStruct;
    frStruct.x = (unsigned int)seed;
    frStruct.y = y_initial;
    frStruct.z = z_initial;
    frStruct.w = w_initial;
    frStruct.bitIndex = 32;
    return frStruct;
}

unsigned int fastrandom_NextUInt(fastrandom_struct *frStruct) {
    fastrandom_struct dereffrStruct = *frStruct;
    unsigned int t = dereffrStruct.x ^ (dereffrStruct.x << 11);
    dereffrStruct.x = dereffrStruct.y;
    dereffrStruct.y = dereffrStruct.z;
    dereffrStruct.z = dereffrStruct.w;
    dereffrStruct.w = dereffrStruct.w ^ (dereffrStruct.w >> 19) ^ t ^ (t >> 8);
    return dereffrStruct.w;
}

int fastrandom_Next(fastrandom_struct *frStruct) {
    return (int)(int_mask & fastrandom_NextUInt(&*frStruct));
}

double fastrandom_NextDouble(fastrandom_struct *frStruct) {
    return int_to_real * fastrandom_Next(&*frStruct);
}

int fastrandom_NextUpper(fastrandom_struct *frStruct, int upperBound) {
    return (int)(fastrandom_NextDouble(&*frStruct) * upperBound);
}

int fastrandom_NextLowerUpper(fastrandom_struct *frStruct, int lowerBound, int upperBound) {
    return (int)(lowerBound + fastrandom_NextDouble(&*frStruct) * (upperBound - lowerBound));
}

int fastrandom_NextDoubleLowerUpper(fastrandom_struct *frStruct, double lowerBound, double upperBound) {
    return (int)(lowerBound + fastrandom_NextDouble(&*frStruct) * (upperBound - lowerBound));
}

bool fastrandom_NextBool(fastrandom_struct *frStruct) {
    fastrandom_struct dereffrStruct = *frStruct;
    if (dereffrStruct.bitIndex == 32) {
        dereffrStruct.bitBuffer = fastrandom_NextUInt(&dereffrStruct);
        dereffrStruct.bitIndex = 1;
        return (dereffrStruct.bitBuffer & 1) == 1;
    }
    dereffrStruct.bitIndex++;
    dereffrStruct.bitBuffer >>= 1;
    return (dereffrStruct.bitBuffer & 1) == 1;
}