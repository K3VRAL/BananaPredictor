#include "args.h"

void args_main(bool *keep_running, int argc, char **argv) {
	if (argc < 2) {
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
		} else if (strcmp(*(argv + i), "--record-objects") == 0) {
			predictor.record_objects = true;
		} else if (strcmp(*(argv + i), "-b") == 0 || strcmp(*(argv + i), "--beatmap") == 0) {
			FILE *fp = fopen(*(argv + ++i), "r");
			if (fp == NULL) {
				continue;
			}
			predictor.beatmap = fp;
		} else if (strcmp(*(argv + i), "-d") == 0 || strcmp(*(argv + i), "--distance") == 0) {
			predictor.distance = strtod(*(argv + ++i), NULL);
		} else if (strcmp(*(argv + i), "-s") == 0 || strcmp(*(argv + i), "--shapes") == 0) {
			char *copy = strdup(*(argv + ++i));
			char *token = strtok(*(argv + i), ":|\0");
			char used_delim = '\0';
			predictor.shapes = realloc(predictor.shapes, (++predictor.shapes_len) * sizeof(*predictor.shapes));
			(predictor.shapes + predictor.shapes_len - 1)->points.vectors = NULL;
			(predictor.shapes + predictor.shapes_len - 1)->points.len = 0;
			while (token != NULL) {
				if (used_delim == '|' || (predictor.shapes + predictor.shapes_len - 1)->points.len == 0) {
					(predictor.shapes + predictor.shapes_len - 1)->points.vectors = realloc((predictor.shapes + predictor.shapes_len - 1)->points.vectors, ++(predictor.shapes + predictor.shapes_len - 1)->points.len * sizeof(*(predictor.shapes + predictor.shapes_len - 1)->points.vectors));
				}
				switch (used_delim) {
					case '\0':
					case '|':
						((predictor.shapes + predictor.shapes_len - 1)->points.vectors + (predictor.shapes + predictor.shapes_len - 1)->points.len - 1)->x = strtol(token, NULL, 10);
						break;

					case ':':
						((predictor.shapes + predictor.shapes_len - 1)->points.vectors + (predictor.shapes + predictor.shapes_len - 1)->points.len - 1)->ty = strtol(token, NULL, 10);
						break;
				}
				used_delim = *(copy + (token - *(argv + i) + strlen(token)));
				token = strtok(NULL, ":|\0");
			}
			free(copy);
		} else if (strcmp(*(argv + i), "-j") == 0 || strcmp(*(argv + i), "--juice-points") == 0) {
			char *copy = strdup(*(argv + ++i));
			char *token = strtok(*(argv + i), ":|\0");
			char used_delim = '\0';
			predictor.jspoints = realloc(predictor.jspoints, (++predictor.jspoints_len) * sizeof(*predictor.jspoints));
			(predictor.jspoints + predictor.jspoints_len - 1)->points.vectors = NULL;
			(predictor.jspoints + predictor.jspoints_len - 1)->points.len = 0;
			while (token != NULL) {
				if (used_delim == '|' || (predictor.jspoints + predictor.jspoints_len - 1)->points.len == 0) {
					(predictor.jspoints + predictor.jspoints_len - 1)->points.vectors = realloc((predictor.jspoints + predictor.jspoints_len - 1)->points.vectors, ++(predictor.jspoints + predictor.jspoints_len - 1)->points.len * sizeof(*(predictor.jspoints + predictor.jspoints_len - 1)->points.vectors));
				}
				switch (used_delim) {
					case '\0':
					case '|':
						((predictor.jspoints + predictor.jspoints_len - 1)->points.vectors + (predictor.jspoints + predictor.jspoints_len - 1)->points.len - 1)->x = strtol(token, NULL, 10);
						break;

					case ':':
						((predictor.jspoints + predictor.jspoints_len - 1)->points.vectors + (predictor.jspoints + predictor.jspoints_len - 1)->points.len - 1)->ty = strtol(token, NULL, 10);
						break;
				}
				used_delim = *(copy + (token - *(argv + i) + strlen(token)));
				token = strtok(NULL, ":|\0");
			}
			free(copy);
		} else if (strcmp(*(argv + i), "-h") == 0 || strcmp(*(argv + i), "--help") == 0) {
			char *title = "BananaPredictor";
			fprintf(stdout, "%s\n\n", title);
			
			char *usage = "bnprdctr [arguments]";
			fprintf(stdout, "usage:\n\t%s\n\n", usage);

			unsigned int arguments_num = 8 * 2;
			char *arguments[8 * 2] = {
				"-o, --output [file]", "outputs the BananaPredictor to the file location",
				"--prefer-circles", "outputs the Banana Shower's bananas instead of the Juice Stream and Banana Shower",
				"--record-objects", "records and outputs the entire map file",
				"-b, --beatmap [file]", "inputs the beatmap from the file location",
				"-d, --distance [time]", "gives the distance for each Banana Shower as a double",
				"-s, --shapes [x:time|...]", "the points for the vector of the shape",
				"-j, --juice-points [x:y|...]", "the points for the vector of the Juice Streams",
				"-h, --help", "gives this help message"
			};
			fprintf(stdout, "arguments:\n");
			
			// For the spaces
			int space_num = 0;
			for (int j = 0; j < arguments_num; j += 2) {
				int num = strlen(*(arguments + j));
				if (num > space_num) {
					space_num = num;
				}
			}

			// Printing text with the spaces
			for (int j = 0; j < arguments_num; j += 2) {
				int num = space_num - strlen(*(arguments + j)) + 1;
				fprintf(stdout, "\t%s%*c%s\n", *(arguments + j), num, ' ', *(arguments + j + 1));
			}
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
	if (predictor.shapes == NULL) {
		*keep_running = false;
		return;
	} else {
		for (int i = 0; i < predictor.shapes_len; i++) {
			if ((predictor.shapes + i)->points.len < 3) {
				*keep_running = false;
				return;
			}
		}
	}
	if (predictor.jspoints == NULL) {
		predictor.jspoints = calloc(++predictor.jspoints_len, sizeof(*predictor.jspoints));
		(predictor.jspoints + 0)->points.len = 2;
		(predictor.jspoints + 0)->points.vectors = calloc((predictor.jspoints + 0)->points.len, sizeof(*(predictor.jspoints + 0)->points.vectors));
		((predictor.jspoints + 0)->points.vectors + 0)->x = 256;
		((predictor.jspoints + 0)->points.vectors + 0)->ty = 384;
		((predictor.jspoints + 0)->points.vectors + 1)->x = 256;
		((predictor.jspoints + 0)->points.vectors + 1)->ty = 0;
	} else {
		for (int i = 0; i < predictor.jspoints_len; i++) {
			if ((predictor.jspoints + i)->points.len < 2) {
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
