#ifndef PRINT_H
#define PRINT_H

#include <stddef.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>
#include <ctype.h>

typedef struct {
    void (*function)(void);
    int *set;
    bool isbool;
    bool ismap;
    bool *bset;
} FunCallback;

typedef struct {
    unsigned int startTime;
    unsigned int endTime;
    int distance;
    bool onlySpin;
    bool inverted;
} listMap;

typedef struct {
    int startLPos;
    int endLPos;
    int startRPos;
    int endRPos;
} listSpin;

typedef struct {
    int id;
    listMap listM;
    listSpin listS;
} listAll;

listAll *addAsk();
listAll *editAsk(listAll *);

#endif