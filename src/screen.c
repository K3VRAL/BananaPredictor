#include "../include/screen.h"

screen_struct screen;
void screen_init(void) {
    initscr();
    noecho();
    cbreak();
    curs_set(0);

    screen_update();
    screen.window = newwin(0, 0, 0, 0);
    box(screen.window, 0, 0);
    wrefresh(screen.window);
}

void screen_close(void) {
    endwin();
}

void screen_update(void) {
    getbegyx(stdscr, screen.yBeg, screen.xBeg);
    getmaxyx(stdscr, screen.yMax, screen.xMax);
}