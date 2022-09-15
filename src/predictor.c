#include "predictor.h"

Predictor predictor = {
    .beatmap = NULL,
    .points = NULL,
    .points_len = 0
};

void predictor_run(void) {
    // Store all `HitObjects` and create their nested objects
    CatchHitObject *object = NULL;
    unsigned int object_len = 0;

    // Evaluate Objects from the Beatmap
    Beatmap beatmap = {0};
    of_beatmap_init(&beatmap);
    of_beatmap_set(&beatmap, predictor.beatmap);
    // oos_hitobject_sort(beatmap.hit_objects, beatmap.num_ho); // TODO look into why this function don't end
    for (int i = 0; i < beatmap.num_ho; i++) {
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

    // Evaluating RNG and the BananaPredictor
    LegacyRandom rng;
    ou_legacyrandom_init(&rng, ooc_processor_RNGSEED);
    for (int i = 0; i < object_len; i++) { // TODO we can make the assumption that the beatmap can be empty
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
        *(areas + areas_len++) = 512;

        if (areas_len == 2) {
            ooc_processor_applypositionoffsetrngstarttime(object, i, i + 1, &rng, false);
            continue;
        }

        HitObject hit_object = { .x = 256, .y = 192, .time = i, .type = spinner, .hit_sound = 0, .ho.spinner.end_time = i + 1, .hit_sample = {0} };
        CatchHitObject object_bs;
        ooc_bananashower_init(&object_bs, hit_object);
        ooc_bananashower_createnestedbananas(&object_bs);

        unsigned int object_js_len = 0;
        CatchHitObject *object_js = malloc(++object_js_len * sizeof(*object_js));
        *(object_js + 0) = object_bs;

        // TODO check if this works
        while (true) {
            LegacyRandom test_rng = rng;
            
            // TODO change this such that we can easily update the length of the slider/amount of droplets or tiny droplets
            HitObject slider_hit_object = { .x = 256, .y = 192, .time = i, .type = slider, .hit_sound = 0,
                .ho.slider = {
                    .curve_type = slidertype_linear,
                    .curves = NULL,
                    .num_curve = 0,
                    .slides = 1,
                    .length = 20
                }, .hit_sample = {0}
            };
            slider_hit_object.ho.slider.curves = realloc(slider_hit_object.ho.slider.curves, ++slider_hit_object.ho.slider.num_curve * sizeof(*slider_hit_object.ho.slider.curves));
            (slider_hit_object.ho.slider.curves + 0)->x = 256;
            (slider_hit_object.ho.slider.curves + 0)->y = 192;
            CatchHitObject juice_stream;
            ooc_juicestream_init(&juice_stream, beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, slider_hit_object);
            object_js = realloc(object_js, ++object_js_len * sizeof(*object_js));
            for (int j = object_js_len; j > 0; j--) {
                *(object_js + j) = *(object_js + j - 1);
            }
            *(object_js + 0) = juice_stream;
            oos_hitobject_free(slider_hit_object);

            ooc_processor_applypositionoffsetrng(object_js, object_js_len, &test_rng, false);

            bool break_out = true;
            for (int j = 0; j < object_bs.cho.bs.num_banana; j++) {
                bool correct_area;
                for (int k = 0; k < areas_len - 1; k++) {
                    unsigned short left = *(areas + k);
                    unsigned short right = *(areas + k + 1);
                    int banana_x = (object->cho.bs.bananas + j)->x + (object->cho.bs.bananas + j)->x_offset;
                    if (banana_x <= left && banana_x >= right) {
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

            // TODO add objects
            rng = test_rng;
            break;
        }

        free(object_js);
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