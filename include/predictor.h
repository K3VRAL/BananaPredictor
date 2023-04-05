#ifndef PREDICTOR_H
#define PREDICTOR_H

#include <stdio.h>
#include <stdbool.h>
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

typedef struct HitObjectPoints {
	int x;
	int y;
} HitObjectPoints;

typedef struct JuiceStreamPoints {
	Point points;
	bool follow;
	int length;
	SliderType type;
} JuiceStreamPoints;

typedef struct BananaShowerPoints {
	unsigned int length;
} BananaShowerPoints;

typedef enum TypePointsEnum {
	hit_object = 'c',
	juice_stream = 'j',
	banana_shower = 'w'
} TypePointsEnum;

typedef union TypePoints {
	HitObjectPoints *ho;
	JuiceStreamPoints *js;
	BananaShowerPoints *bs;
} TypePoints;

typedef struct Predictor {
	FILE *beatmap;
	
	FILE *output;
	bool output_beatmap;

	ShapePoints *shapes;
	unsigned int shapes_len;

	TypePointsEnum points_type;
	TypePoints points;
	unsigned int points_len;

	double distance;

	bool prefer_circles;
	bool record_objects;
} Predictor;

extern Predictor predictor;

/* The BananaPredictor running and evaluating based off of the inputs given */
void predictor_main(void);

#endif
