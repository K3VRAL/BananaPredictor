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

    // Store all `HitObjects` and create their nested objects
    CatchHitObject *object = NULL;
    unsigned int object_len = 0;

    // Evaluate Objects and RNG from the Beatmap
    Beatmap beatmap = {0};
    of_beatmap_init(&beatmap);
    of_beatmap_set(&beatmap, predictor.beatmap);
    // oos_hitobject_sort(beatmap.hit_objects, beatmap.num_ho); // TODO look into why this function don't end
    for (int i = 0; i < beatmap.num_ho; i++) {
        // Expand the size of `object` and store the newly created `CatchHitObject`
        if ((beatmap.hit_objects + i)->time > points_start) {
            break;
        }
        object = realloc(object, ++object_len * sizeof(*object));
        switch ((beatmap.hit_objects + i)->type) {
            case circle:
            case nc_circle:
                ooc_fruit_init((object + object_len - 1), *(beatmap.hit_objects + i));
                break;

            case slider:
            case nc_slider:
                ooc_juicestream_init((object + object_len - 1), beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, *(beatmap.hit_objects + i));
                ooc_juicestream_createnestedjuice((object + object_len - 1));
                break;
            
            case spinner:
            case nc_spinner:
                ooc_bananashower_init((object + object_len - 1), *(beatmap.hit_objects + i));
                ooc_bananashower_createnestedbananas((object + object_len - 1));
                break;
        }
    }
    LegacyRandom rng;
    ou_legacyrandom_init(&rng, ooc_processor_RNGSEED);
    ooc_processor_applypositionoffsetrng(object, object_len, &rng, false);   

    // Evaluating BananaPredictor
    for (int i = points_start; i < points_end; i++) {
        // Recording the areas of the x-axis at the current time which can place the bananas
        unsigned int areas_len = 2;
        unsigned short *areas = malloc(areas_len-- * sizeof(*areas));
        *(areas + 0) = 0;
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
                areas = realloc(areas, (++areas_len * sizeof(*areas)));
                *(areas + areas_len - 1) = r->x;
            }
            free(r);
        }
        for (int j = 0; j < areas_len - 1; j++) {
            for (int k = 0; k < areas_len - j - 1; k++) {
                if (*(areas + k) > *(areas + k + 1)) {
                    unsigned short temp = *(areas + k + 1);
                    *(areas + k + 1) = *(areas + k);
                    *(areas + k) = temp;
                }
            }
        }
        *(areas + areas_len++) = 512;

        if (areas_len == 2) {
            return;
        }

        unsigned int bnpd_len = 0;
        CatchHitObject *bnpd = malloc(++bnpd_len * sizeof(*bnpd));
        ooc_bananashower_init((bnpd + 0), (HitObject){ .x = 256, .y = 192, .time = i, .type = spinner, .hit_sound = 0, .ho.spinner.end_time = i + 1, .hit_sample = {0} });
        ooc_bananashower_createnestedbananas((bnpd + 0));

        // TODO something is going wrong
        // TODO reinitialising the juicestream just so we can reset the slider variables is inefficient; fix `libosu` on this
        while (true) {
            // If previous Juice Stream isn't full; keep increasing it. If previous Juice Stream is full; make a new one
            if ((bnpd + 0)->type == catchhitobject_juicestream && (bnpd + 0)->cho.js.slider_data.ho_data->length < 384) { // TODO this doesn't work
                HitObject slider_hit_object = { .x = (bnpd + 0)->x, .y = 384, .time = (bnpd + 0)->start_time, .type = slider, .hit_sound = 0,
                    .ho.slider = {
                        .curve_type = slidertype_linear,
                        .curves = NULL,
                        .num_curve = 0,
                        .slides = 1,
                        // We can keep increasing the length until we get the amount of droplets/tiny droplets needed            
                        .length = (bnpd + 0)->cho.js.slider_data.path.distance
                    }, .hit_sample = {0}
                };
                slider_hit_object.ho.slider.curves = realloc(slider_hit_object.ho.slider.curves, ++slider_hit_object.ho.slider.num_curve * sizeof(*slider_hit_object.ho.slider.curves));
                (slider_hit_object.ho.slider.curves + 0)->x = 256;
                (slider_hit_object.ho.slider.curves + 0)->y = 0;
                unsigned int nested = (bnpd + 0)->cho.js.num_nested;
                while (true) {
                    slider_hit_object.ho.slider.length++;
                    ooc_hitobject_free(*(bnpd + 0));
                    ooc_juicestream_init((bnpd + 0), beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, slider_hit_object);
                    ooc_juicestream_createnestedjuice((bnpd + 0));
                    if ((bnpd + 0)->cho.js.num_nested > nested) {
                        break;
                    }
                }
                oos_hitobject_free(slider_hit_object);
            } else {
                bnpd = realloc(bnpd, ++bnpd_len * sizeof(*bnpd));
                for (int j = bnpd_len; j > 0; j--) {
                    *(bnpd + j - 1) = *(bnpd + j - 2); // TODO Valgrind is complaining
                }
                HitObject slider_hit_object = { .x = 256, .y = 384, .time = i, .type = slider, .hit_sound = 0,
                    .ho.slider = {
                        .curve_type = slidertype_linear,
                        .curves = NULL,
                        .num_curve = 0,
                        .slides = 1,
                        // We can keep increasing the length until we get the amount of droplets/tiny droplets needed            
                        .length = 1
                    }, .hit_sample = {0}
                };
                slider_hit_object.ho.slider.curves = realloc(slider_hit_object.ho.slider.curves, ++slider_hit_object.ho.slider.num_curve * sizeof(*slider_hit_object.ho.slider.curves));
                (slider_hit_object.ho.slider.curves + 0)->x = 256;
                (slider_hit_object.ho.slider.curves + 0)->y = 0;
                while (true) {
                    ooc_juicestream_init((bnpd + 0), beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, slider_hit_object);
                    ooc_juicestream_createnestedjuice((bnpd + 0));
                    if ((bnpd + 0)->cho.js.num_nested > 3) {
                        break;
                    }
                    slider_hit_object.ho.slider.length++;
                    ooc_hitobject_free(*(bnpd + 0));
                }
                oos_hitobject_free(slider_hit_object);
            }

            LegacyRandom test_rng = rng;
            ooc_processor_applypositionoffsetrng(bnpd, bnpd_len, &test_rng, false);

            bool break_out = true;
            for (int j = 0; j < (bnpd + bnpd_len - 1)->cho.bs.num_banana; j++) {
                bool correct_area;
                int banana_x = (int) ((bnpd + bnpd_len - 1)->cho.bs.bananas + j)->x + (int) ((bnpd + bnpd_len - 1)->cho.bs.bananas + j)->x_offset;
                for (int k = 0; k < areas_len - 1; k++) {
                    unsigned short left = *(areas + k);
                    unsigned short right = *(areas + k + 1);
                    if (banana_x >= left && banana_x <= right) {
                        // If the number is odd; we know it's in the right position
                        // If the number is even; we know it's in the wrong position
                        if (k % 2 != 0) {
                            correct_area = true;
                        } else if (k % 2 == 0) {
                            correct_area = false;
                        }
                        break;
                    }
                }
                if (!correct_area) {
                    break_out = false;
                    break;
                }
            }
            if (!break_out) {
                continue;
            }

            // TODO add to objects
            rng = test_rng;
            // for (int j = 0; j < bnpd_len; j++) {
            //     if ((bnpd + j)->type == catchhitobject_bananashower) {
            //         printf("256,192,%d,8,0,%d\n", (int) (bnpd + j)->start_time, (bnpd + j)->cho.bs.end_time);
            //     } else if ((bnpd + j)->type == catchhitobject_juicestream) {
            //         printf("0,384,%d,2,0,L|0:0,1,%d\n", (int) (bnpd + j)->start_time, (int) (bnpd + j)->cho.js.slider_data.path.distance);
            //     }
            // }
            break;
        }

        ooc_hitobject_freebulk(bnpd, bnpd_len);
        free(areas);
    }

    // Write to file
    // TODO

    // Free
    ooc_hitobject_freebulk(object, object_len);
    of_beatmap_free(beatmap);
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