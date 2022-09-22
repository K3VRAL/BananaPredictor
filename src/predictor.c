#include "predictor.h"

// TODO something is breaking in the code when there are either; sliders/sliders + timing point with weird decimals

Predictor predictor = {
    .output = NULL,
    .beatmap = NULL,
    .points = NULL,
    .points_len = 0,
    .distance = 1
};

void predictor_run(void) {
    // Get time where we should start and stop the loops
    int points_start;
    int points_end;
    predictor_points(&points_start, &points_end);

    // Store all `HitObjects` and create their nested objects
    CatchHitObject *object = NULL;
    unsigned int object_len = 0;

    // Evaluate Objects and RNG from the Beatmap
    if (predictor.beatmap == NULL) {
        return;
    }
    Beatmap beatmap = {0};
    predictor_beatmap(&object, &object_len, &beatmap, points_start);

    // We need the RNG of the beatmap until the point that we start
    LegacyRandom rng;
    ou_legacyrandom_init(&rng, ooc_processor_RNGSEED);
    ooc_processor_applypositionoffsetrng(object, object_len, &rng, false);

    // Now we reuse and only store the BananaPredictor to the `object` variable
    ooc_hitobject_freebulk(object, object_len);
    object = NULL;
    object_len = 0;
    
    // Evaluating BananaPredictor
    for (double i = points_start; i < points_end; i += predictor.distance) {
        // Update Progress Bar
        if (predictor.output != stdout) {
            predictor_progressbar((((i - points_start) / (points_end - points_start)) * 100) + 1);
        }

        // Recording the areas of the x-axis at the current time which can place the bananas
        unsigned int areas_len = 0;
        unsigned short *areas = calloc(++areas_len, sizeof(*areas));
        predictor_areas(&areas, &areas_len, (int) i);

        // Make a variable to store the new objects and house the Banana Shower
        unsigned int bnpd_len = 0;
        CatchHitObject *bnpd = calloc(++bnpd_len, sizeof(*bnpd));
        ooc_bananashower_init((bnpd + 0), (HitObject){ .x = 256, .y = 192, .time = (int) i, .type = spinner, .hit_sound = 0, .ho.spinner.end_time = (int) i + 1, .hit_sample = {0} });
        ooc_bananashower_createnestedbananas((bnpd + 0));

        while (true) {
            // Generate new Juice Stream or, if previous object is a Juice Stream, expand on said Juice Stream
            predictor_generatejs(&bnpd, &bnpd_len, (int) i, beatmap);

            // Apply the RNG to the new objects
            LegacyRandom test_rng = rng;
            ooc_processor_applypositionoffsetrng(bnpd, bnpd_len, &test_rng, false);

            // Check if they are within the areas we wanted them to be in; if not, we repeat the loop
            if (!predictor_breakout(areas, areas_len, bnpd, bnpd_len)) {
                continue;
            }

            // Stores the objects
            predictor_storeobjects(&object, &object_len, &rng, test_rng, bnpd, bnpd_len);
            break;
        }

        free(bnpd);
        free(areas);
    }
    fprintf(stdout, "\n");

    // Write to output
    predictor_output(object, object_len);

    // Free
    ooc_hitobject_freebulk(object, object_len);
    of_beatmap_free(beatmap);
}

void predictor_points(int *points_start, int *points_end) {
    *points_start = INT_MAX;
    *points_end = INT_MIN;
    for (int i = 0; i < predictor.points_len; i++) {
        if ((predictor.points + i)->time < *points_start) {
            *points_start = (predictor.points + i)->time;
        }
        if ((predictor.points + i)->time > *points_end) {
            *points_end = (predictor.points + i)->time;
        }
    }
}

void predictor_beatmap(CatchHitObject **object, unsigned int *object_len, Beatmap *beatmap, int points_start) {
    of_beatmap_init(beatmap);
    of_beatmap_set(beatmap, predictor.beatmap);
    // oos_hitobject_sort(beatmap.hit_objects, beatmap.num_ho); // TODO look into why this function don't end
    for (int i = 0; i < beatmap->num_ho; i++) {
        if ((beatmap->hit_objects + i)->time > points_start) {
            break;
        }
        *object = realloc(*object, ++*object_len * sizeof(**object));
        switch ((beatmap->hit_objects + i)->type) {
            case circle:
            case nc_circle:
                ooc_fruit_init((*object + *object_len - 1), *(beatmap->hit_objects + i));
                break;

            case slider:
            case nc_slider:
                ooc_juicestream_init((*object + *object_len - 1), beatmap->difficulty, beatmap->timing_points, beatmap->num_tp, *(beatmap->hit_objects + i));
                ooc_juicestream_createnestedjuice((*object + *object_len - 1));
                break;
            
            case spinner:
            case nc_spinner:
                ooc_bananashower_init((*object + *object_len - 1), *(beatmap->hit_objects + i));
                ooc_bananashower_createnestedbananas((*object + *object_len - 1));
                break;
        }
    }
}

void predictor_progressbar(unsigned int percent) {
    struct winsize w;
    ioctl(STDOUT_FILENO, TIOCGWINSZ, &w);

    int size_terminal = w.ws_col - 2 - 1 - 3 - 1;
    int width = percent * size_terminal / 100;
    fprintf(stdout, "\r[");
    for (int i = 0; i < width; i++) {
        fprintf(stdout, "#");
    }
    for (int i = 0; i < size_terminal - width; i++) {
        fprintf(stdout, " ");
    }
    fprintf(stdout, "] %d%%", percent);
    fflush(stdout);
}

void predictor_areas(unsigned short **areas, unsigned int *areas_len, int i) {
    *(*areas + 0) = 0;
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
            *areas = realloc(*areas, (++*areas_len * sizeof(**areas)));
            *(*areas + *areas_len - 1) = r->x;
        }
        free(r);
    }
    for (int j = 0; j < *areas_len - 1; j++) {
        for (int k = 0; k < *areas_len - j - 1; k++) {
            if (*(*areas + k) > *(*areas + k + 1)) {
                unsigned short temp = *(*areas + k + 1);
                *(*areas + k + 1) = *(*areas + k);
                *(*areas + k) = temp;
            }
        }
    }
    *areas = realloc(*areas, (++*areas_len * sizeof(**areas)));
    *(*areas + *areas_len - 1) = 512;

    if (*areas_len == 2) {
        exit(1);
    }
}

void predictor_line(Line *line, Point p1, Point p2) {
    line->a = p1.time - p2.time;
    line->b = p2.x - p1.x;
    line->c = -((p1.x * p2.time) - (p2.x * p1.time));
}

void predictor_intersection(Point **r, Line l1, Line l2) {
    double d = l1.a * l2.b - l1.b * l2.a;
    if (d != 0) {
        *r = calloc(1, sizeof(**r));
        (*r)->x = (l1.c * l2.b - l1.b * l2.c) / d;
        (*r)->time = (l1.a * l2.c - l1.c * l2.a) / d;
    }
}

void predictor_generatejs(CatchHitObject **bnpd, unsigned int *bnpd_len, int i, Beatmap beatmap) {
    // TODO Make the Slider/Juice Stream longer if the previous object is a juice stream and doesn't have an end time above the point's end time
    *bnpd = realloc(*bnpd, ++*bnpd_len * sizeof(**bnpd));
    for (int j = *bnpd_len - 1; j > 0; j--) {
        *(*bnpd + j) = *(*bnpd + j - 1);
    }

    HitObject slider_hit_object = { .x = 0, .y = 384, .time = i, .type = slider, .hit_sound = 0,
        .ho.slider = {
            .curve_type = slidertype_linear,
            .curves = NULL,
            .num_curve = 0,
            .slides = 1,
            // We can keep increasing the length until we get the amount of droplets/tiny droplets needed            
            .length = 1
        }, .hit_sample = {0}
    };
    slider_hit_object.ho.slider.curves = calloc(++slider_hit_object.ho.slider.num_curve, sizeof(*slider_hit_object.ho.slider.curves));
    (slider_hit_object.ho.slider.curves + 0)->x = 0;
    (slider_hit_object.ho.slider.curves + 0)->y = 0;
    ooc_juicestream_init((*bnpd + 0), beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, slider_hit_object);
    
    InheritedTimingPoint inherited;
    oos_inheritedpoint_init(&inherited, beatmap.timing_points, beatmap.num_tp);
    TimingPoint tp_inherited;
    oos_timingpoint_attime(&tp_inherited, slider_hit_object.time, inherited.tp, inherited.num);

    UninheritedTimingPoint uninherited;
    oos_uninheritedpoint_init(&uninherited, beatmap.timing_points, beatmap.num_tp);
    TimingPoint tp_uninherited;
    oos_timingpoint_attime(&tp_uninherited, slider_hit_object.time, uninherited.tp, uninherited.num);
    while (true) {
        ooc_juicestream_createnestedjuice((*bnpd + 0));
        if ((*bnpd + 0)->cho.js.num_nested > 3) {
            break;
        }
        slider_hit_object.ho.slider.length++;
        free((*bnpd + 0)->cho.js.nested);
        (*bnpd + 0)->cho.js.nested = NULL;
        (*bnpd + 0)->cho.js.num_nested = 0;
        oos_slider_calculateslider(&(*bnpd + 0)->cho.js.slider_data, beatmap.difficulty, tp_inherited, tp_uninherited, slider_hit_object);
    }
    oos_inheritedpoint_free(inherited);
    oos_uninheritedpoint_free(uninherited);
    oos_hitobject_free(slider_hit_object);
}

bool predictor_breakout(unsigned short *areas, unsigned int areas_len, CatchHitObject *bnpd, unsigned int bnpd_len) {
    for (int j = 0; j < (bnpd + bnpd_len - 1)->cho.bs.num_banana; j++) {
        bool correct_area;
        int banana_x = (int) ((bnpd + bnpd_len - 1)->cho.bs.bananas + j)->x + (int) ((bnpd + bnpd_len - 1)->cho.bs.bananas + j)->x_offset;
        for (int k = 0; k < areas_len - 1; k++) {
            unsigned int left = *(areas + k);
            unsigned int right = *(areas + k + 1);
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
            return false;
        }
    }
    return true;
}

void predictor_storeobjects(CatchHitObject **object, unsigned int *object_len, LegacyRandom *rng, LegacyRandom test_rng, CatchHitObject *bnpd, unsigned int bnpd_len) {
    *rng = test_rng;
    *object_len += bnpd_len;
    *object = realloc(*object, *object_len * sizeof(**object));
    for (int j = *object_len - bnpd_len, k = 0; j < *object_len; j++, k++) {
        *(*object + j) = *(bnpd + k);
    }
}

void predictor_output(CatchHitObject *object, unsigned int object_len) {
    for (int i = 0; i < object_len; i++) {
        if ((object + i)->type == catchhitobject_bananashower) {
            fprintf(predictor.output, "256,192,%d,8,0,%d\n", (int) (object + i)->start_time, (object + i)->cho.bs.end_time);
        } else if ((object + i)->type == catchhitobject_juicestream) {
            fprintf(predictor.output, "%d,%d,%d,6,0,%c", (int) (object + i)->cho.js.slider_data.start_position.x, (int) (object + i)->cho.js.slider_data.start_position.y, (int) (object + i)->start_time, (object + i)->cho.js.slider_data.ho_data->curve_type);
            for (int j = 0; j < (object + i)->cho.js.slider_data.ho_data->num_curve; j++) {
                fprintf(predictor.output, "|%d:%d", ((object + i)->cho.js.slider_data.ho_data->curves + j)->x, ((object + i)->cho.js.slider_data.ho_data->curves + j)->y);
            }
            fprintf(predictor.output, ",%d,%d\n", (int) (object + i)->cho.js.slider_data.span_count, (int) (object + i)->cho.js.slider_data.path.distance);
        }
    }
}