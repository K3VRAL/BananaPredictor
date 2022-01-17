#ifndef PRINT_H
#define PRINT_H

#include <stddef.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <ctype.h>

typedef struct FunCallback {
    void (*function)(void);
    int *set;
    bool isbool;
    bool maplimits;
    bool *bset;
} FunCallback;

typedef struct listA {
    unsigned int startTime;
    unsigned int endTime;
    int distance;
    bool onlySpin;
    bool inverted;
    int startLPos;
    int endLPos;
    int startRPos;
    int endRPos;
} listA;

typedef struct listS {
    unsigned int time;
    int pos;
} listS;

typedef union listSpin {
    listA als;
    listS sls;
} listSpin;
typedef enum listEnum {
    als,
    sls
} listEnum;

typedef struct listSpinners {
    listEnum lid;
    listSpin ls;
} listSpinners;

listSpin *addAsk(listEnum);

#endif