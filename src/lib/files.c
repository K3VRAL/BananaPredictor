#include "files.h"
#include <string.h>

char *target;
char *output;

void *xrealloc(void *ptr, size_t size) {
    void *newptr = realloc(ptr, size);
    if (!newptr) { perror("xrealloc"); exit(EXIT_FAILURE); }
    return newptr;
}

void targetPath(char *input) {
    target = xrealloc(NULL, strlen(input) * sizeof (char) + 1);
    strcpy(target, input);
}

void outputPath(char *input) {
    output = xrealloc(NULL, strlen(input) * sizeof (char) + 1);
    strcpy(output, input);
}

void freePath() {
    free(target);
    free(output);
}