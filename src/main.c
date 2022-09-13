#include "main.h"

int main(int argc, char **argv) {
    // Handle arguments given
    if (argc <= 1) {
        printf("Use `-h` or check out the documentation if you need help.\n");
        return 1;
    }
    args_handle(argc, argv);

    // Make a few tests before we run things
    FILE *fp = fopen(predictor.beatmap, "r");
    if (predictor.beatmap == NULL || predictor.points == NULL || fp == NULL) {
        return 1;
    }

    // Where the magic happens
    predictor_run();

    // Free
    if (predictor.beatmap != NULL) {
        free(predictor.beatmap);
    }
    if (predictor.points != NULL) {
        free(predictor.points);
    }

    return 0;
}