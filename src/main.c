#include "main.h"

int main(int argc, char **argv) {
    // Handle arguments given
    bool keep_running = false;
    args_handle(&keep_running, argc, argv);

    // Make a few tests before we run things
    if (keep_running) {
        // Where the magic happens
        predictor_run();
    }

    // Free
    if (predictor.output != NULL) {
        fclose(predictor.output);
    }
    if (predictor.beatmap != NULL) {
        fclose(predictor.beatmap);
    }
    if (predictor.points != NULL) {
        free(predictor.points);
    }

    return !keep_running;
}