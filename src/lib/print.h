#ifndef PRINT_H
#define PRINT_H

#include <stdio.h>
#include <stdbool.h>
#include <ctype.h>

#include "files.h"

typedef struct {
    void (*function)(void);
    size_t *set;
    bool isbool;
    bool *bset;
} FunCallback;

typedef struct {
    size_t startTime;
    size_t endTime;
    size_t distance;
    bool onlySpin;
    bool inverted;
} listMap;

typedef struct {
    size_t startLPos;
    size_t endLPos;
    size_t startRPos;
    size_t endRPos;
} listSpin;

typedef struct {
    listMap listM;
    listSpin listS;
} listAll;

listAll addAsk();

extern listMap lMap;
extern listSpin lSpin;

#endif