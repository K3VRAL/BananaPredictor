#ifndef SCREEN_H
#define SCREEN_H

#include <ncurses.h>
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>
#include <signal.h>

typedef struct {
    unsigned int yBeg, xBeg, yMax, xMax;
    WINDOW *window;
} screen_struct;
extern screen_struct screen;
void screen_update(void);
void screen_init(void);
void screen_close(void);

#endif