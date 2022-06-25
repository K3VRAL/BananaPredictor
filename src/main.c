#include "../include/main.h"

int main(int argc, char **argv) {
    screen_init();

    menu_bar_init((struct_menu []){
            menu_init("File", 'f', (struct_menu_list []){
                menu_list_init("New", 'n', menu_list_file_new),
                menu_list_init("Open", 'o', menu_list_file_open),
                menu_list_init("Save", 's', NULL),
                menu_list_init("Save As", 'a', NULL),
                menu_list_init("Import", 'i', NULL),
                menu_list_init("Export", 'e', NULL),
                menu_list_init("Exit", 'x', menu_list_file_exit),
            }, 7),
            menu_init("Edit", 'e', (struct_menu_list []){
                menu_list_init("Undo", 'u', NULL),
                menu_list_init("Redo", 'r', NULL),
                menu_list_init("Cut", 'x', NULL),
                menu_list_init("Copy", 'c', NULL),
                menu_list_init("Paste", 'p', NULL),
            }, 5),
            menu_init("Help", 'h', (struct_menu_list []){
                menu_list_init("Get Started", 'g', menu_list_help_getstarted),
                menu_list_init("About", 'a', menu_list_help_about),
                menu_list_init("Source Code", 's', menu_list_help_sourcecode),
            }, 3),
        }, 3);
    menu_bar_draw();

    signal(SIGINT, menu_bar_handleSignal);
    char ch;
    while ((ch = wgetch(screen.window))) {
        menu_bar_handleTriggers(ch);
        menu_bar_draw();
    }

    return 0;
}