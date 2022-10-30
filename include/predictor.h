#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <limits.h>
#include <unistd.h>
#include <sys/ioctl.h>

#include <osu.h>

typedef struct Vector {
	float x;
	float ty;
} Vector;

typedef struct Point {
	Vector *vectors;
	unsigned int len;
} Point;

typedef struct ShapePoints {
	Point points;
	int start;
	int end;
} ShapePoints;

typedef struct JuiceStreamPoints {
	Point points;
} JuiceStreamPoints;

typedef struct Predictor {
	FILE *beatmap;
	
	FILE *output;

	ShapePoints *shapes;
	unsigned int shapes_len;

	JuiceStreamPoints *jspoints;
	unsigned int jspoints_len;

	double distance;

	bool prefer_circles;
	bool record_objects;
} Predictor;

extern Predictor predictor;

/* The BananaPredictor running and evaluating based off of the inputs given */
void predictor_main(void);

#endif
