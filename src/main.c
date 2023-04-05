#include "main.h"

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

int main(int argc, char **argv) {
	// Handle arguments given and make a few tests before we run things
	bool keep_running = args_main(argc, argv);

	if (keep_running) {
		// Where the magic happens
		predictor_main();
	}

	// Free
	if (predictor.beatmap != NULL) {
		fclose(predictor.beatmap);
	}
	if (predictor.output != stdout && predictor.output != NULL) {
		fclose(predictor.output);
	}
	if (predictor.shapes != NULL) {
		for (int i = 0; i < predictor.shapes_len; i++) {
			if ((predictor.shapes + i)->points.vectors != NULL) {
				free((predictor.shapes + i)->points.vectors);
			}
		}
		free(predictor.shapes);
	}
	switch (predictor.points_type) {
		case hit_object:
			if (predictor.points.ho != NULL) {
				free(predictor.points.ho);
			}
			break;

		case juice_stream:
			if (predictor.points.js != NULL) {
				for (int i = 0; i < predictor.points_len; i++) {
					if ((predictor.points.js + i)->points.vectors != NULL) {
						free((predictor.points.js + i)->points.vectors);
					}
				}
				free(predictor.points.js);
			}
			break;

		case banana_shower:
			if (predictor.points.bs != NULL) {
				free(predictor.points.bs);
			}
			break;
	}

	return !keep_running;
}
