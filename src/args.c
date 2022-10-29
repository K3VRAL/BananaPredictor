#include "args.h"

#define args_arg_num 8
char *args_arg[args_arg_num][2] = {
	{ "-b", "--beatmap" },
	{ "-o", "--output" },
	{ "-s", "--shapes" },
	{ "-j", "--juice-points" },
	{ "-d", "--distance" },
	{ "-p", "--prefer-circles" },
	{ "-r", "--record-objects" },
	{ "-h", "--help" }
};

bool args_beatmap(char *option) {
	FILE *fp = fopen(option, "r");
	if (fp == NULL) {
		return true;
	}
	predictor.beatmap = fp;
	return false;
}

bool args_output(char *option) {
	FILE *fp = fopen(option, "w");
	if (fp == NULL) {
		return true;
	}
	predictor.output = fp;
	return false;
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

	char *arguments[args_arg_num][2] = {
		{ "file", "inputs the beatmap from the file location" },
		{ "[file]", "outputs the BananaPredictor to the file location" },
		{ "x:time|x:time|x:time[|...]", "the points for the vector of the shape" },
		{ "[x:y|...]", "the points for the vector of the Juice Streams" },
		{ "[time]", "gives the distance for each Banana Shower as a double" },
		{ "", "outputs the Banana Shower's bananas instead of the Juice Stream and Banana Shower" },
		{ "", "records and outputs the entire map file" },
		{ "", "gives this help message" }
	};
	// For the spaces
	int space_num = 0;
	for (int i = 0; i < args_arg_num; i++) {
		int num = strlen(*(*(args_arg + i) + 0)) + 2 + strlen(*(*(args_arg + i) + 1)) + strlen(*(*(arguments + i) + 0));
		if (num > space_num) {
			space_num = num;
		}
	}
	// Printing text with the spaces
	fprintf(stdout, "arguments:\n");
	for (int i = 0; i < args_arg_num; i++) {
		int num = space_num - (strlen(*(*(args_arg + i) + 0)) + 2 + strlen(*(*(args_arg + i) + 1)) + strlen(*(*(arguments + i) + 0)));
		fprintf(stdout, "\t%s, %s %s%*c%s\n", *(*(args_arg + i) + 0), *(*(args_arg + i) + 1), *(*(arguments + i) + 0), num + 1, ' ', *(*(arguments + i) + 1));
	}
}

void args_unknown(char *option) {
	fprintf(stdout, "Argument not found: %s\n", option);
}

bool args_main(int argc, char **argv) {
	for (int i = 1; i < argc; i++) {
		if (!strcmp(*(*(args_arg + 0) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 0) + 1), *(argv + i))) {
			if (args_beatmap(*(argv + ++i))) {
				continue;
			}
		} else if (!strcmp(*(*(args_arg + 1) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 1) + 1), *(argv + i))) {
			if (args_output(*(argv + ++i))) {
				continue;
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
		} else if (!strcmp(*(*(args_arg + 7) + 0), *(argv + i)) || !strcmp(*(*(args_arg + 7) + 1), *(argv + i))) {
			args_help();
			return false;
		} else {
			args_unknown(*(argv + i));
			return false;
		}
	}

	if (predictor.output == NULL) {
		predictor.output = stdout;
	}

	if (predictor.beatmap == NULL || predictor.shapes == NULL) {
		return false;
	}

	for (int i = 0; i < predictor.shapes_len; i++) {
		if ((predictor.shapes + i)->points.len < 3) {
			return false;
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
				return false;
			}
		}
	}

	return true;
}
