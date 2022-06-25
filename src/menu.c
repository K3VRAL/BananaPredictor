#include "../include/menu.h"

// Menu list
struct_menu_list menu_list_init(char *text, char trigger, void (*function)(void)) {
    struct_menu_list list;
    list.text = text;
    list.trigger = trigger;
    list.function = function;
    return list;
}

// Menu
struct_menu menu_init(char *text, char trigger, struct_menu_list *items, unsigned int num_items) {
    struct_menu menu;
    menu.start_x = 0;
    menu.text = text;
    menu.trigger = trigger;
    menu.items = items;
    menu.num_items = num_items;
    menu.selected_item = 0;
    return menu;
}

void menu_handleTriggers(struct_menu *menu, char trigger) {
    for (unsigned int i = 0; i < menu->num_items; i++) {
        if ((menu->items + i)->trigger == trigger) {
            menu->selected_item = i;
        }
    }
}

void menu_selectUpItem(struct_menu *menu) {
    menu->selected_item--;
    if (menu->selected_item < 0) {
        menu->selected_item = menu->num_items - 1;
    }
}

void menu_selectDownItem(struct_menu *menu) {
    menu->selected_item++;
    if (menu->selected_item >= menu->num_items) {
        menu->selected_item = 0;
    }
}

// Menu bar
struct_menu_bar menu_bar;
void menu_bar_init(struct_menu *menus, int num_menus) {
    menu_bar.menus = menus;
    menu_bar.num_menus = num_menus;
    menu_bar.selected_menu = -1;

    unsigned int yBeg, xBeg, yMax, xMax;
    getbegyx(screen.window, yBeg, xBeg);
    getmaxyx(screen.window, yMax, xMax);
    menu_bar.window_menu = newwin(yMax - 2, xMax - 2, yBeg + 1, xBeg + 1);
    keypad(menu_bar.window_menu, true);
    wrefresh(menu_bar.window_menu);

    for (unsigned int i = 0, current_pos = 2; i < menu_bar.num_menus; i++) {
        (menu_bar.menus + i)->start_x = current_pos;
        current_pos += (strlen((menu_bar.menus + i)->text) + 3) + 1;
    }
}

bool handleSignal = false;
void menu_bar_handleSignal(int num) {
    handleSignal = true;
    werase(menu_bar.window_menu);
    wrefresh(menu_bar.window_menu);

    for (unsigned int i = 0; i < menu_bar.num_menus; i++) {
        mvwprintw(screen.window, 0, (menu_bar.menus + i)->start_x, "%s(%c)", (menu_bar.menus + i)->text, (menu_bar.menus + i)->trigger);
    }
    wrefresh(screen.window);
}

void menu_bar_handleTriggers(char trigger) {
    for (unsigned int i = 0; i < menu_bar.num_menus; i++) {
        if ((menu_bar.menus + i)->trigger == trigger) {
            menu_bar.selected_menu = i;
        }
    }
}

void menu_bar_draw(void) {
    for (unsigned int i = 0; i < menu_bar.num_menus; i++) {
        menu_bar_drawMenu(&*(menu_bar.menus + i), menu_bar.selected_menu == i);
    }
    menu_bar.selected_menu = -1;
}

void menu_bar_drawMenu(struct_menu *menu, bool is_selected) {
    if (is_selected) {
        wattron(screen.window, A_STANDOUT);
    }
    mvwprintw(screen.window, 0, menu->start_x, "%s(%c)", menu->text, menu->trigger);
    wattroff(screen.window, A_STANDOUT);
    wrefresh(screen.window);

    int ch;
    menu_bar_drawMenuItems(&*menu);
    wrefresh(menu_bar.window_menu);
    while (is_selected && (ch = wgetch(menu_bar.window_menu))) {
        switch (ch) {
            case KEY_UP:
                menu_selectUpItem(&*menu);
                break;
            case KEY_DOWN:
                menu_selectDownItem(&*menu);
                break;
            case '.':
                werase(menu_bar.window_menu);
                wrefresh(menu_bar.window_menu);
                int yPos = getmaxy(screen.window);
                if ((menu->items + menu->selected_item)->function != NULL) {
                    (menu->items + menu->selected_item)->function();
                } else {
                    mvwprintw(menu_bar.window_menu, yPos - 4, 0, "Function not working right now");
                }
                if (!handleSignal) {
                    mvwprintw(menu_bar.window_menu, yPos - 3, 0, "Press key to continue...");
                    wgetch(menu_bar.window_menu);
                } else {
                    handleSignal = false;
                }
                is_selected = false;
                break;
            case ',':
                is_selected = false;
                break;
            default:
                menu_handleTriggers(&*menu, ch);
        }
        menu_bar_drawMenuItems(&*menu);
    }
    werase(menu_bar.window_menu);
    wrefresh(menu_bar.window_menu);

    for (unsigned int i = 0; i < menu_bar.num_menus; i++) {
        mvwprintw(screen.window, 0, (menu_bar.menus + i)->start_x, "%s(%c)", (menu_bar.menus + i)->text, (menu_bar.menus + i)->trigger);
    }
    wrefresh(screen.window);
}

void menu_bar_drawMenuItems(struct_menu *menu) {
    for (unsigned int i = 0; i < menu->num_items; i++) {
        if (menu->selected_item == i) {
            wattron(menu_bar.window_menu, A_STANDOUT);
        }
        mvwprintw(menu_bar.window_menu, i, 0, "%s(%c)", (menu->items + i)->text, (menu->items + i)->trigger);
        wattroff(menu_bar.window_menu, A_STANDOUT);
    }
}

// Menu list: File
char *input_data(char *message, int y, int x) {
    mvwprintw(menu_bar.window_menu, y, x, "> %s: ", message);
    unsigned int size = 5, i = 0;
    char *inp = malloc((size + 1) * sizeof(char)), ch;
    bool isnotdone = true;
    while (isnotdone && !handleSignal && (ch = wgetch(menu_bar.window_menu))) {
        switch (ch) {
            case '\n':
                isnotdone = false;
                break;
            default:
                *(inp + i) = ch;
                *(inp + ++i) = '\0';
                mvwprintw(menu_bar.window_menu, y, x + strlen(message), "%s", inp);
                break;
        }
        if (i == size) {
            size += 5;
            inp = realloc(inp, (size + 1) * sizeof(char));
        }
    }
    if (!handleSignal) {
        return inp;
    }
    return NULL;
}

void menu_list_file_new(void) {
    char *input_name = input_data("Create a name for the new project", 0, 0);

    free(input_name);
}

void menu_list_file_open(void) {
    char *input_file = input_data("Input the name of the file to open", 0, 0);
    if (input_file != NULL) {
        FILE *fp = fopen(input_file, "r");
        if (fp != NULL) {
            mvwprintw(menu_bar.window_menu, 1, 1, "File [%s] imported", input_file);
            fclose(fp);
        } else {
            mvwprintw(menu_bar.window_menu, 1, 1, "File [%s] could not be found", input_file);
        }
        fclose(fp);
        free(input_file);
    }
}

void menu_list_file_exit(void) {
    screen_close();
    exit(0);
}

// Menu list: Help
void menu_list_help_getstarted(void) {
    mvwprintw(menu_bar.window_menu, 0, 0, 
        "TODO - Blah blah blah"
    );
}

void menu_list_help_about(void) {
    mvwprintw(menu_bar.window_menu, 0, 0, 
        "TODO - Blah blah blah"
    );
}

void menu_list_help_sourcecode(void) {
    mvwprintw(menu_bar.window_menu, 1, 1, 
        "TODO - Blah blah blah"
    );
}