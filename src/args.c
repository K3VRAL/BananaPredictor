#include "args.h"

void args_main(bool *keep_running, int argc, char **argv) {
    if (argc < 1) {
        fprintf(stdout, "Use `-h` or check out the documentation if you need help.\n");
        *keep_running = false;
    }
    bool argument_404 = false;
    for (int i = 1; i < argc; i++) {
        if (strcmp(*(argv + i), "-o") == 0 || strcmp(*(argv + i), "--output") == 0) {
            FILE *fp = fopen(*(argv + ++i), "w");
            if (fp == NULL) {
                continue;
            }
            predictor.output = fp;
        } else if (strcmp(*(argv + i), "--prefer-circles") == 0) {
            predictor.prefer_circles = true;
        } else if (strcmp(*(argv + i), "-b") == 0 || strcmp(*(argv + i), "--beatmap") == 0) {
            FILE *fp = fopen(*(argv + ++i), "r");
            if (fp == NULL) {
                continue;
            }
            predictor.beatmap = fp;
        } else if (strcmp(*(argv + i), "-s") == 0 || strcmp(*(argv + i), "--shapes") == 0) {
            char *copy = strdup(*(argv + ++i));
            char *token = strtok(*(argv + i), ":|\0");
            enum {
                x,
                time,
            } type;
            char used_delim = '\0';
            predictor.shapes = realloc(predictor.shapes, (++predictor.shapes_len) * sizeof(*predictor.shapes));
            (predictor.shapes + predictor.shapes_len - 1)->points = NULL;
            (predictor.shapes + predictor.shapes_len - 1)->len = 0;
            while (token != NULL) {
                if (used_delim == '|' || (predictor.shapes + predictor.shapes_len - 1)->len == 0) {
                    (predictor.shapes + predictor.shapes_len - 1)->points = realloc((predictor.shapes + predictor.shapes_len - 1)->points, ++(predictor.shapes + predictor.shapes_len - 1)->len * sizeof(*(predictor.shapes + predictor.shapes_len - 1)->points));
                    type = x;
                }
                switch (type) {
                    case x:
                        ((predictor.shapes + predictor.shapes_len - 1)->points + (predictor.shapes + predictor.shapes_len - 1)->len - 1)->x = strtol(token, NULL, 10);
                        type = time;
                        break;

                    case time:
                        ((predictor.shapes + predictor.shapes_len - 1)->points + (predictor.shapes + predictor.shapes_len - 1)->len - 1)->time = strtol(token, NULL, 10);
                        type = x; // Why not
                        break;
                }
                used_delim = *(copy + (token - *(argv + i) + strlen(token)));
                token = strtok(NULL, ":|\0");
            }
            free(copy);
        } else if (strcmp(*(argv + i), "-d") == 0 || strcmp(*(argv + i), "--distance") == 0) {
            predictor.distance = strtod(*(argv + ++i), NULL);
        } else if (strcmp(*(argv + i), "-h") == 0 || strcmp(*(argv + i), "--help") == 0) {
            char *title = "K 3 V R A L's BananaPredictor";
            fprintf(stdout, "%s\n\n", title);
            
            char *usage = "bnprdctr [arguments]";
            fprintf(stdout, "usage:\n\t%s\n\n", usage);

            unsigned int arguments_num = 6 * 2;
            char **arguments = calloc(arguments_num, sizeof(*arguments));
            *(arguments + 0) = "-o, --output [file]";
            *(arguments + 1) = "outputs the BananaPredictor to the file location";
            *(arguments + 2) = "--prefer-circles";
            *(arguments + 3) = "outputs the Banana Shower's bananas instead of the Juice Stream and Banana Shower";
            *(arguments + 4) = "-b, --beatmap [file]";
            *(arguments + 5) = "inputs the beatmap from the file location";
            *(arguments + 6) = "-s, --shapes [x:time|...]";
            *(arguments + 7) = "the points of the vector";
            *(arguments + 8) = "-d, --distance [time]";
            *(arguments + 9) = "gives the distance for the best Banana Shower";
            *(arguments + 10) = "-h, --help";
            *(arguments + 11) = "gives this help message";
            fprintf(stdout, "arguments:\n");
            int space_num = 0;
            for (int j = 0; j < arguments_num; j += 2) {
                int num = strlen(*(arguments + j));
                if (num > space_num) {
                    space_num = num;
                }
            }
            for (int j = 0; j < arguments_num; j += 2) {
                int num = space_num - strlen(*(arguments + j)) + 1;
                fprintf(stdout, "\t%s%*c%s\n", *(arguments + j), num, ' ', *(arguments + j + 1));
            }
            free(arguments);
        } else {
            fprintf(stdout, "Argument not found: %s\n", *(argv + i));
            argument_404 = true;
        }
    }
    if (predictor.output == NULL) {
        predictor.output = stdout;
    }
    if (predictor.beatmap == NULL) {
        *keep_running = false;
        return;
    }
    if (predictor.shapes_len < 1) {
        *keep_running = false;
        return;
    } else {
        for (int i = 0; i < predictor.shapes_len; i++) {
            if ((predictor.shapes + i)->len < 3) {
                *keep_running = false;
                return;
            }
        }
    }
    if (argument_404) {
        *keep_running = false;
        return;
    }
}