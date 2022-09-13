#include "args.h"

void args_handle(int argc, char **argv) {
    for (int i = 1; i < argc; i++) {
        if (strcmp(*(argv + i), "-b") == 0 || strcmp(*(argv + i), "--beatmap") == 0) {
            predictor.beatmap = strdup(*(argv + ++i));
        } else if (strcmp(*(argv + i), "-p") == 0 || strcmp(*(argv + i), "--points") == 0) {
            char *copy = strdup(*(argv + ++i));
            char *token = strtok(*(argv + i), ":|\0");
            enum {
                x,
                time,
            } type;
            char used_delim = '\0';
            while (token != NULL) {
                if (used_delim == '|' || predictor.points_len == 0) {
                    predictor.points = realloc(predictor.points, (++predictor.points_len) * sizeof(*predictor.points));
                    type = x;
                }
                switch (type) {
                    case x:
                        (predictor.points + predictor.points_len - 1)->x = strtol(token, NULL, 10);
                        type = time;
                        break;

                    case time:
                        (predictor.points + predictor.points_len - 1)->time = strtol(token, NULL, 10);
                        type = x; // Why not
                        break;
                }
                used_delim = *(copy + (token - *(argv + i) + strlen(token)));
                token = strtok(NULL, ":|\0");
            }
            free(copy);
        } else if (strcmp(*(argv + i), "-h") == 0 || strcmp(*(argv + i), "--help") == 0) {
            printf("Help section\n"); // TODO
        } else {
            fprintf(stdout, "Argument not found: %s\n", *(argv + i));
            exit(1);
        }
    }
}