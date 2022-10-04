#include "main.h"

int main(int argc, char **argv) {
	// Handle arguments given
	bool keep_running = true;
	args_main(&keep_running, argc, argv);

	// Make a few tests before we run things
	if (keep_running) {
		// Where the magic happens
		predictor_main();
	}

	// Free
	if (predictor.output != NULL) {
		fclose(predictor.output);
	}
	if (predictor.beatmap != NULL) {
		fclose(predictor.beatmap);
	}
	if (predictor.shapes != NULL) {
		for (int i = 0; i < predictor.shapes_len; i++) {
			if ((predictor.shapes + i)->vectors != NULL) {
				free((predictor.shapes + i)->vectors);
			}
		}
		free(predictor.shapes);
	}
	if (predictor.jspoints != NULL) {
		for (int i = 0; i < predictor.jspoints_len; i++) {
			if ((predictor.jspoints + i)->vectors != NULL) {
				free((predictor.jspoints + i)->vectors);
			}
		}
		free(predictor.jspoints);
	}

	return !keep_running;
}