#include "../include/predictor.h"

Predictor predictor = {
    .start = 0,
    .length = 0,
    .shower_length = 1,
    .shower_distance = 0,
    .invert = false,
    .points = NULL,
    .points_len = 0
};