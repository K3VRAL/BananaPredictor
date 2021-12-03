#include "files.h"

void *xrealloc(void *ptr, size_t size) {
    void *newptr = realloc(ptr, size);
    if (!newptr) { perror("xrealloc"); exit(EXIT_FAILURE); }
    return newptr;
}