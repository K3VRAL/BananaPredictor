#include "args.h"
#include "predictor.h"

#include <stdbool.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>

void args_beatmap(char *option) {
	if (predictor.beatmap != NULL) {
		fprintf(stdout, "Error: Output file has already been inputted\n");
		return;
	}
	FILE *fp = fopen(option, "r");
	if (fp == NULL) {
		return;
	}
	predictor.beatmap = fp;
}

void args_output(char *option) {
	if (predictor.output != NULL) {
		fprintf(stdout, "Error: Output file has already been inputted\n");
		return;
	}
	FILE *fp = fopen(option, "w");
	if (fp == NULL) {
		fprintf(stdout, "Error: Output file is not writable\n");
		return;
	}
	predictor.output = fp;
	predictor.output_beatmap = false;
}

void args_output_beatmap(char *option) {
	if (predictor.output != NULL) {
		fprintf(stdout, "Error: Output file has already been inputted\n");
		return;
	}
	args_output(option);
	predictor.output_beatmap = true;
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
	(predictor.jspoints + predictor.jspoints_len - 1)->follow = false;
	while (token != NULL) {
		if (used_delim == '|' || (predictor.jspoints + predictor.jspoints_len - 1)->points.len == 0) {
			(predictor.jspoints + predictor.jspoints_len - 1)->points.vectors = realloc((predictor.jspoints + predictor.jspoints_len - 1)->points.vectors, ++(predictor.jspoints + predictor.jspoints_len - 1)->points.len * sizeof(*(predictor.jspoints + predictor.jspoints_len - 1)->points.vectors));
		}
		switch (used_delim) {
			case '\0':
			case '|':
				if (!strcmp("f", token)) {
					(predictor.jspoints + predictor.jspoints_len - 1)->follow = true;
					break;
				}
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

void args_help(void);

typedef struct Args {
	char *i;
	char *item;
	char *argument;
	char *description;
	enum {
		cp,
		v,
		rv
	} e_function;
	union {
		void (*cp)(char *);
		void (*v)(void);
	} function;
} Args;
#define args_num 9
Args args_arg[args_num] = {
	{
		.i = "-b",
		.item = "--beatmap",
		.argument = "file",
		.description = "inputs the beatmap from the file location",
		.e_function = cp,
		.function = {
			.cp = args_beatmap
		}
	},
	{
		.i = "-o",
		.item = "--output",
		.argument = "[file]",
		.description = "outputs the BananaPredictor to the file location",
		.e_function = cp,
		.function = {
			.cp = args_output
		}
	},
	{
		.i = "-O",
		.item = "--output-beatmap",
		.argument = "[file]",
		.description = "outputs the BananaPredictor to the file location with the osu format",
		.e_function = cp,
		.function = {
			.cp = args_output_beatmap
		}
	},
	{
		.i = "-s",
		.item = "--shapes",
		.argument = "x:time|x:time|x:time[|...]",
		.description = "the points for the vector of the shape",
		.e_function = cp,
		.function = {
			.cp = args_shape
		}
	},
	{
		.i = "-j",
		.item = "--juice-points",
		.argument = "[[f|]x:y|x:y[|...]]",
		.description = "the points for the vector of the Juice Streams",
		.e_function = cp,
		.function = {
			.cp = args_juice_point
		}
	},
	{
		.i = "-d",
		.item = "--distance",
		.argument = "[time]",
		.description = "gives the distance for each Banana Shower as a double",
		.e_function = cp,
		.function = {
			.cp = args_distance
		}
	},
	{
		.i = "-p",
		.item = "--prefer-circles",
		.argument = "",
		.description = "outputs the Banana Shower's bananas instead of the Juice Stream and Banana Shower",
		.e_function = v,
		.function = {
			.v = args_prefer_circles
		}
	},
	{
		.i = "-r",
		.item = "--record-objects",
		.argument = "",
		.description = "records and outputs the entire map file",
		.e_function = v,
		.function = {
			.v = args_record_objects
		}
	},
	{
		.i = "-h",
		.item = "--help",
		.argument = "",
		.description = "gives this help message",
		.e_function = rv,
		.function = {
			.v = args_help
		}
	}
};

void args_help(void) {
	char *title = "BananaPredictor";
	fprintf(stdout, "%s\n\n", title);

	char *usage = "bnprdctr [arguments]";
	fprintf(stdout, "usage:\n\t%s\n\n", usage);

	// For the spaces
	int space_num = 0;
	for (int i = 0; i < args_num; i++) {
		int num = strlen((args_arg + i)->i) + 2 + strlen((args_arg + i)->item) + strlen((args_arg + i)->argument);
		if (num > space_num) {
			space_num = num;
		}
	}
	// Printing text with the spaces
	fprintf(stdout, "arguments:\n");
	for (int i = 0; i < args_num; i++) {
		int num = space_num - (strlen((args_arg + i)->i) + 2 + strlen((args_arg + i)->item) + strlen((args_arg + i)->argument));
		fprintf(stdout, "\t%s, %s %s%*c%s\n", (args_arg + i)->i, (args_arg + i)->item, (args_arg + i)->argument, num + 1, ' ', (args_arg + i)->description);
	}
}

void args_main(bool *keep_running, int argc, char **argv) {
	for (int i = 1; i < argc; i++) {
		bool not_found = true;
		for (int j = 0; j < args_num; j++) {
			if (!strcmp((args_arg + j)->i, *(argv + i)) || !strcmp((args_arg + j)->item, *(argv + i))) {
				if ((args_arg + j)->e_function == cp) {
					(args_arg + j)->function.cp(*(argv + ++i));
				} else if ((args_arg + j)->e_function == v) {
					(args_arg + j)->function.v();
				} else if ((args_arg + j)->e_function == rv) {
					(args_arg + j)->function.v();
					*keep_running = false;
					return;
				}
				not_found = false;
				break;
			}
		}
		if (not_found) {
			fprintf(stdout, "Error: Argument not found: %s\n", *(argv + i));
			*keep_running = false;
			return;
		}
	}

	if (predictor.output == NULL) {
		predictor.output = stdout;
	}

	if (predictor.beatmap == NULL) {
		fprintf(stdout, "Error: Input beatmap file not set\n");
		*keep_running = false;
		return;
	}

	if (predictor.shapes == NULL) {
		fprintf(stdout, "Error: Shapes not set\n");
		*keep_running = false;
		return;
	}

	for (int i = 0; i < predictor.shapes_len; i++) {
		if ((predictor.shapes + i)->points.len < 3) {
			fprintf(stdout, "Error: Shape has points less 3\n");
			*keep_running = false;
			return;
		}
	}

	if (predictor.jspoints == NULL) {
		args_juice_point("256:384|256:0");
	} else {
		for (int i = 0; i < predictor.jspoints_len; i++) {
			if ((predictor.jspoints + i)->points.len < 2) {
				fprintf(stdout, "Error: JuiceStream has points less than 2\n");
				*keep_running = false;
				return;
			}
		}
	}
}