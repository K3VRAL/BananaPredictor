#include <args.h>

void args_handle(int argc, char **argv) {
    for (int i = 1; i < argc; i++) {
        if (strcmp(*(argv + i), "-s") == 0 || strcmp(*(argv + i), "--start") == 0) {
            predictor.start = (unsigned int)strtol(*(argv + ++i), NULL, 10);
        } else if (strcmp(*(argv + i), "-e") == 0 || strcmp(*(argv + i), "--end") == 0) {
            predictor.end = (unsigned int)strtol(*(argv + ++i), NULL, 10);
        } else if (strcmp(*(argv + i), "-l") == 0 || strcmp(*(argv + i), "--length") == 0) {
            predictor.length = (unsigned int)strtol(*(argv + ++i), NULL, 10);
        } else if (strcmp(*(argv + i), "-d") == 0 || strcmp(*(argv + i), "--distance") == 0) {
            predictor.distance = (unsigned int)strtol(*(argv + ++i), NULL, 10);
        } else if (strcmp(*(argv + i), "-i") == 0 || strcmp(*(argv + i), "--invert") == 0) {
            predictor.invert = true;
        } else if (strcmp(*(argv + i), "-p") == 0 || strcmp(*(argv + i), "--points") == 0) {
            enum {
                x,
                time,
                bezier
            } type = x;
            char *argv_copy = strdup(*(argv + ++i));
            char *token = strtok(argv_copy, ":");
            Point temp_point = {0};
            unsigned int skip = 0;
            while (token != NULL) {
                char *token_contains = strchr(token, '|');
                if (token_contains != NULL) {
                    token = strtok(token, "|");
                }
                switch (type) {
                    case x:
                        temp_point.x = strtol(token, NULL, 10);
                        break;
                    case time:
                        temp_point.time = strtol(token, NULL, 10);
                        break;
                    case bezier:
                        temp_point.bezier = strtol(token, NULL, 10);
                        break;
                }
                type++;
                if (token_contains == NULL) {
                    token = strtok(NULL, ":");
                } 
                
                if (token_contains != NULL || token == NULL) {
                    if (predictor.points_len != 0) {
                        predictor.points = realloc(predictor.points, sizeof(Point));
                    } else {
                        predictor.points = malloc(sizeof(Point));
                        temp_point.bezier = false;
                    }
                    predictor.points_len++;
                    memcpy((predictor.points + predictor.points_len - 1), &temp_point, sizeof(Point));

                    argv_copy = realloc(argv_copy, (strlen(*(argv + i)) + 1) * sizeof(char));
                    strcpy(argv_copy, *(argv + i));
                    token = strtok(argv_copy, "|");
                    skip++;
                    unsigned int temp_skip = 0;
                    while (temp_skip != skip) {
                        token = strtok(NULL, "|");
                        temp_skip++;
                    }
                    token = strtok(token, ":");
                    type = x;
                    memset(&temp_point, 0, sizeof(Point));
                }
            }
            free(argv_copy);
        } else {
            fprintf(stdout, "Argument not found: %s\n", *(argv + i));
            exit(1);
        }
    }
}