#ifndef FILES_H
#define FILES_H

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void *xrealloc(void *ptr, size_t size);

void targetPath(char *input);
void outputPath(char *input);
void freePath();

extern char *target;
extern char *output;

#endif