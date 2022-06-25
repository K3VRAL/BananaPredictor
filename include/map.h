#ifndef OBJECTS_H
#define OBJECTS_H

#include <stdbool.h>

// Hit Object
typedef struct {
    // Unnecessary but here for consistency
} Circle;

typedef struct {
    int x;
    int y;
} SliderCurveTypePoints;

typedef struct {
    char curve_type;
    SliderCurveTypePoints *curve_type_points;
    int slides;
    double length;
    int *edge_sounds;
    char **edge_sets;
} Slider;

typedef struct {
    int end_time;
} Spinner;

// Comma Data
typedef union {
    Circle circle_object;
    Slider slider_object;
    Spinner spinner_object;
} Comma;

typedef enum {
    circle,
    slider,
    spinner
} CommaType;

// Hit Object
typedef struct {
    int x;
    int y;
    int time;
    int type;
    int hit_sound;
    CommaType comma_type;
    Comma comma;
    int *hit_sample;
} HitObject;

// Timing Point
typedef struct {
    int time;
    double beat_length;
    int meter;
    int sample_set;
    int sample_index;
    int volume;
    bool uninherited;
    int effects;
} TimePoint;

// Object
typedef union {
    HitObject hit_obj;
    TimePoint time_point;
} Object;

typedef enum {
    hit_obj,
    time_point
} ObjectType;

// Node
typedef struct Node {
    struct Node *next;
    struct Node *previous;
} Node;

// Objects
typedef struct {
    Node node;
    Object obj;
    ObjectType obj_type;
} Objects;

// Map Data
typedef struct {
    Objects objects;
    double slider_multiplier;
    double slider_tick_rate;
} Map;

#endif