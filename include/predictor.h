#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <limits.h>
#include <unistd.h>
#include <sys/ioctl.h>

#include <osu.h>

typedef struct XLine {
	unsigned short *areas;
	unsigned int len;
} XLine;

typedef struct Coefficient {
	double a;
	double b;
	double c;
} Coefficient;

typedef struct Vector {
	float x;
	union {
		float time;
		float y;
	} ty;
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
	// char *type; // TODO allow for the user to change types of the slider
} JuiceStreamPoints;

typedef struct Predictor {
	FILE *output;
	bool prefer_circles;
	bool record_objects;

	FILE *beatmap;

	ShapePoints *shapes;
	unsigned int shapes_len;

	JuiceStreamPoints *jspoints;
	unsigned int jspoints_len;

	double distance;
} Predictor;

extern Predictor predictor;

/*
	The BananaPredictor running and evaluating based off of the inputs given
	
	return
		void
*/
void predictor_main(void);

/*
	Sets the values for the points to start and stop the main loop

	return
		void
	argsreturn
		int *
		int *
*/
void predictor_shapes(int *, int *);

/*
	Prints out the progress bar in the terminal. Resizing the terminal will also resize the output

	return
		void
	args
		unsigned int
*/
void predictor_progressbar(unsigned int);

// Basing below functions off of https://stackoverflow.com/a/20679579 because I suck at math
/*
	Calculate the intersections of each line and whether they should be used for calculation

	return
		void
	argsreturn
		unsigned short **
		unsigned int *
	args
		int
*/
void predictor_areas(XLine **, unsigned int *, int);

/*
	Calculating the Line using Cramer's Rule

	return
		void
	argsreturn
		Coefficient *
	args
		Vector
		Vector
*/
void predictor_line(Coefficient *, Vector, Vector);

/*
	Calculating the Line of Intersection with Determinants

	return
		void
	argsreturn
		Vector **
	args
		Line
		Line
*/
void predictor_intersection(Vector **, Coefficient, Coefficient);
// End of stackoverflow

/*
	Either we create a new Juice Stream and have a minimum of 3 nested objects; or if the previously created Juice Stream is not long enough then we can keep expanding it

	return
		void
	argsreturn
		CatchHitObject **
		unsigned int *
	args
		int
		int
		Beatmap
*/
void predictor_generatejs(CatchHitObject **, unsigned int *, int, int, Beatmap);

/*
	Returns if we need to retry or store the current positions of the objects

	return
		bool
	args
		XLine *
		unsigned int
		CatchHitObject *
		unsigned int
*/
bool predictor_breakout(XLine *, unsigned int, CatchHitObject *, unsigned int);

/*
	Gets the values of the beatmap and svaes rng based on the converted catch object

	return
		void
	argsreturn
		LegacyRandom *
	args
		Beatmap *
		int
*/
void predictor_beatmap(LegacyRandom *, Beatmap *, int);

/*
	Output the object generated and saves the rng

	return
		void
	argsreturn
		LegacyRandom *
	args
		LegacyRandom
		CatchHitObject *
		unsigned int
*/
void predictor_saverng(LegacyRandom *, CatchHitObject *, unsigned int, LegacyRandom);

/*
	Outputs data

	return
		void
	args
		CatchHitObject
*/
void predictor_output(CatchHitObject);

#endif
