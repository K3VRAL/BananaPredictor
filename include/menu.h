#ifndef MENU_H
#define MENU_H

#include <ncurses.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>

#include "screen.h"

typedef struct {
    char *text, trigger;
    void (*function)(void);
} struct_menu_list;
struct_menu_list menu_list_init(char *, char, void (*)(void));
void menu_list_file_new(void);
void menu_list_file_open(void);
void menu_list_file_exit(void);
void menu_list_help_getstarted(void);
void menu_list_help_about(void);
void menu_list_help_sourcecode(void);

typedef struct {
    unsigned int start_x, num_items;
    char *text, trigger;
    struct_menu_list *items;
    int selected_item;
} struct_menu;
struct_menu menu_init(char *, char, struct_menu_list *, unsigned int);
void menu_handleTriggers(struct_menu *, char);
void menu_selectUpItem(struct_menu *);
void menu_selectDownItem(struct_menu *);

typedef struct {
    WINDOW *window_menu;
    struct_menu *menus;
    unsigned int num_menus;
    int selected_menu;
} struct_menu_bar;
extern struct_menu_bar menu_bar;
void menu_bar_init(struct_menu *, int);
void menu_bar_handleSignal(int);
void menu_bar_handleTriggers(char);
void menu_bar_selectLeftItem(void);
void menu_bar_selectRightItem(void);
void menu_bar_draw(void);
void menu_bar_drawMenu(struct_menu *, bool);
void menu_bar_drawMenuItems(struct_menu *);

#endif