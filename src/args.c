#include "args.h"

#define args_arg_num 8
char *args_arg[args_arg_num][4] = {
	{ "-b", "--beatmap", "file", "inputs the beatmap from the file location" },
	{ "-o", "--output", "[file]", "outputs the BananaPredictor to the file location" },
	{ "-s", "--shapes", "x:time|x:time|x:time[|...]", "the points for the vector of the shape" },
	{ "-j", "--juice-points", "[x:y|x:y[|...]]", "the points for the vector of the Juice Streams" },
	{ "-d", "--distance", "[time]", "gives the distance for each Banana Shower as a double" },
	{ "-p", "--prefer-circles", "", "outputs the Banana Shower's bananas instead of the Juice Stream and Banana Shower" },
	{ "-r", "--record-objects", "", "records and outputs the entire map file" },
	{ "-h", "--help", "", "gives this help message" }
};

void args_beatmap(bool *assign, char *option) {
	FILE *fp = fopen(option, "r");
	if (fp == NULL) {
		*assign = false;
		return;
	}
	predictor.beatmap = fp;
}

void args_output(bool *assign, char *option) {
	FILE *fp = fopen(option, "w");
	if (fp == NULL) {
		*assign = false;
		return;
	}
	predictor.output = fp;
}

void args_shape(char *option) {
	char *copy = strdup(option);
	char *token = strtok(option, ":|\0");
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
		used_delim = *(copy + (token - option + strlen(token)));
		token = strtok(NULL, ":|\0");
	}
	free(copy);
}

void args_juice_point(char *option) {
	char *copy = strdup(option);
	char *token = strtok(option, ":|\0");
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
		used_delim = *(copy + (token - option + strlen(token)));
		token = strtok(NULL, ":|\0");
	}
	free(copy);
}

void args_distance(char *option) {
	predictor.distance = strtod(option, NULL);
}

void args_prefer_circles(void) {
	predictor.prefer_circles = true;
}

void args_record_objects(void) {
	predictor.record_objects = true;
}

void args_help(void) {
	char *title = "BananaPredictor";
	fprintf(stdout, "%s\n\n", title);

	char *usage = "bnprdctr [arguments]";
	fprintf(stdout, "usage:\n\t%s\n\n", usage);

	// For the spaces
	int space_num = 0;
	for (int i = 0; i < args_arg_num; i++) {
		int num = strlen(*(*(args_arg + i) + 0)) + 2 + strlen(*(*(args_arg + i) + 1)) + strlen(*(*(args_arg + i) + 2));
		if (num > space_num) {
			space_num = num;
		}
	}
	// Printing text with the spaces
	fprintf(stdout, "arguments:\n");
	for (int i = 0; i < args_arg_num; i++) {
		int num = space_num - (strlen(*(*(args_arg + i) + 0)) + 2 + strlen(*(*(args_arg + i) + 1)) + strlen(*(*(args_arg + i) + 2)));
		fprintf(stdout, "\t%s, %s %s%*c%s\n", *(*(args_arg + i) + 0), *(*(args_arg + i) + 1), *(*(args_arg + i) + 2), num + 1, ' ', *(*(args_arg + i) + 3));
	}
}

void args_unknown_argument(char *option) {
	fprintf(stdout, "Argument not found: %s\n", option);
}

void args_main(bool *keep_running, int argc, char **argv) {
	for (int i = 1; i < argc; i++) {
		if (!strcmp(*(*(args_arg + 0) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 0) + 1), *(argv + i))) {
			bool assign = true;
			args_beatmap(&assign, *(argv + ++i));
			if (!assign) {
				fprintf(stdout, "Beatmap file not found: %s\n", *(argv + i));
			}
		} else if (!strcmp(*(*(args_arg + 1) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 1) + 1), *(argv + i))) {
			bool assign = true;
			args_output(&assign, *(argv + ++i));
			if (!assign) {
				fprintf(stdout, "Output file not possible: %s - defaulting to stdout\n", *(argv + i));
			}
		} else if (!strcmp(*(*(args_arg + 2) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 2) + 1), *(argv + i))) {
			args_shape(*(argv + ++i));
		} else if (!strcmp(*(*(args_arg + 3) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 3) + 1), *(argv + i))) {
			args_juice_point(*(argv + ++i));
		} else if (!strcmp(*(*(args_arg + 4) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 4) + 1), *(argv + i))) {
			args_distance(*(argv + ++i));
		} else if (!strcmp(*(*(args_arg + 5) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 5) + 1), *(argv + i))) {
			args_prefer_circles();
		} else if (!strcmp(*(*(args_arg + 6) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 6) + 1), *(argv + i))) {
			args_record_objects();
		} else if (!strcmp(*(*(args_arg + args_arg_num - 1) + 0), *(argv + i)) || !strcmp(*(*(args_arg + args_arg_num - 1) + 1), *(argv + i))) {
			args_help();
			*keep_running = false;
			return;
		} else {
			args_unknown_argument(*(argv + i));
			*keep_running = false;
			return;
		}
	}

	if (predictor.output == NULL) {
		predictor.output = stdout;
	}

	if (predictor.beatmap == NULL || predictor.shapes == NULL) {
			*keep_running = false;
			return;
	}

	for (int i = 0; i < predictor.shapes_len; i++) {
		if ((predictor.shapes + i)->points.len < 3) {
			*keep_running = false;
			return;
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
}
