#include "predictor.h"

Predictor predictor = {
    .beatmap = NULL,
    .points = NULL,
    .points_len = 0
};

void predictor_run(void) {
    // Get time where we should start and stop the loops
    int points_start;
    int points_end;
    points_start = INT_MAX;
    points_end = INT_MIN;
    for (int i = 0; i < predictor.points_len; i++) {
        if ((predictor.points + i)->time < points_start) {
            points_start = (predictor.points + i)->time;
        }
        if ((predictor.points + i)->time > points_end) {
            points_end = (predictor.points + i)->time;
        }
    }
    
    // Store all `HitObjects` and create their nested objects up to what we need
    CatchHitObject *object = NULL;
    unsigned int object_len = 0;
    {
        // Evaluate Objects from the Beatmap
        Beatmap beatmap = {0};
        of_beatmap_init(&beatmap);
        of_beatmap_set(&beatmap, predictor.beatmap);
        oos_hitobject_sort(beatmap.hit_objects, beatmap.num_ho);
        for (int i = 0; i < beatmap.num_ho; i++) {
            if ((beatmap.hit_objects + i)->time > points_start) {
                break;
            }
            // Expand the size of `object` and store the newly created `CatchHitObject`
            object = realloc(object, (object_len + 1) * sizeof(*object));
            switch ((beatmap.hit_objects + i)->type) {
                case circle:
                case nc_circle:
                    ooc_fruit_init((object + object_len), *(beatmap.hit_objects + i));
                    break;

                case slider:
                case nc_slider:
                    ooc_juicestream_init((object + object_len), beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, *(beatmap.hit_objects + i));
                    ooc_juicestream_createnestedjuice((object + object_len));
                    break;
                
                case spinner:
                case nc_spinner:
                    ooc_bananashower_init((object + object_len), *(beatmap.hit_objects + i));
                    ooc_bananashower_createnestedbananas((object + object_len));
                    break;
            }
            object_len++;
        }
        of_beatmap_free(beatmap);
    }

    // Evaluation RNG up to what we need
    LegacyRandom rng;
    ou_legacyrandom_init(&rng, ooc_processor_RNGSEED);
    for (int i = 0; i < object_len; i++) {
        ooc_processor_applypositionoffsetrngstarttime(object, i, i + 1, &rng, false);
        if (points_start < (object + i)->start_time) {
            break;
        }
    }

    // Evaluating the BananaPredictor
    for (int i = points_start; i < points_end; i++ /* TODO increment based on Banana Shower's distance for each banana */) {
        // Checking if the intersection is valid so we can place our Banana Showers
        for (int j = 0; j < predictor.points_len; j++) {
            Line l1;
            Point p1_l1 = *(predictor.points + j);
            Point p2_l1 = j != predictor.points_len - 1 ? *(predictor.points + j + 1) : *(predictor.points + 0);
            predictor_line(&l1, p1_l1, p2_l1);
            
            Line l2;
            Point p1_l2 = { .x = -1, .time = i };
            Point p2_l2 = { .x = 1, .time = i };
            predictor_line(&l2, p1_l2, p2_l2);

            Point *r = NULL;
            predictor_intersection(&r, l1, l2);
            if (r == NULL) {
                continue;
            }
            if (r->x >= 0 && r->x <= 512) { 
                // TODO `r->x` `r->time`
            }
            free(r);
        }
    }

    // Free
    ooc_hitobject_freebulk(object, object_len);
}

void predictor_line(Line *line, Point p1, Point p2) {
    line->a = p1.time - p2.time;
    line->b = p2.x - p1.x;
    line->c = -((p1.x * p2.time) - (p2.x * p1.time));
}

void predictor_intersection(Point **r, Line l1, Line l2) {
    double d = l1.a * l2.b - l1.b * l2.a;
    if (d != 0) {
        *r = malloc(sizeof(**r));
        (*r)->x = (l1.c * l2.b - l1.b * l2.c) / d;
        (*r)->time = (l1.a * l2.c - l1.c * l2.a) / d;
    }
}