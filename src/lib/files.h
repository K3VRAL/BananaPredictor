#ifndef FILES_H
#define FILES_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <string.h>

typedef struct {
    int tp;
    int ho;
} fileRelated;

void *xrealloc(void *ptr, size_t size);

#endif