#include "predictor.h"

// TODO fix errors with generated with valgrind in libosu `slider.c:462`

Predictor predictor = {
	.output = NULL,
	.prefer_circles = false,
	.record_objects = false,

	.beatmap = NULL,

	.shapes = NULL,
	.shapes_len = 0,

	.jspoints = NULL,
	.jspoints_len = 0,

	.distance = 1,
};

/* Sets the values for the points to start and stop the main loop */
void predictor_shapes(int *shapes_start, int *shapes_end) {
	*shapes_start = INT_MAX;
	*shapes_end = INT_MIN;
	for (int i = 0; i < predictor.shapes_len; i++) {
		(predictor.shapes + i)->start = INT_MAX;
		(predictor.shapes + i)->end = INT_MIN;
		for (int j = 0; j < (predictor.shapes + i)->points.len; j++) {
			if (((predictor.shapes + i)->points.vectors + j)->ty < (predictor.shapes + i)->start) {
				(predictor.shapes + i)->start = ((predictor.shapes + i)->points.vectors + j)->ty;
			}
			if (((predictor.shapes + i)->points.vectors + j)->ty < *shapes_start) {
				*shapes_start = ((predictor.shapes + i)->points.vectors + j)->ty;
			}

			if (((predictor.shapes + i)->points.vectors + j)->ty > (predictor.shapes + i)->end) {
				(predictor.shapes + i)->end = ((predictor.shapes + i)->points.vectors + j)->ty;
			}
			if (((predictor.shapes + i)->points.vectors + j)->ty > *shapes_end) {
				*shapes_end = ((predictor.shapes + i)->points.vectors + j)->ty;
			}
		}
	}
}

/* Prints out the progress bar in the terminal. Resizing the terminal will also resize the output */
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

// Basing below functions off of https://stackoverflow.com/a/20679579 because I suck at math
typedef struct XLine {
	unsigned short *areas;
	unsigned int len;
} XLine;

typedef struct Coefficient {
	double a;
	double b;
	double c;
} Coefficient;

/* Calculating the Line using Cramer's Rule */
void predictor_line(Coefficient *coefficient, Vector p1, Vector p2) {
	coefficient->a = p1.ty - p2.ty;
	coefficient->b = p2.x - p1.x;
	coefficient->c = -((p1.x * p2.ty) - (p2.x * p1.ty));
}

/* Calculating the Line of Intersection with Determinants */
void predictor_intersection(Vector **r, Coefficient c1, Coefficient c2) {
	double d = c1.a * c2.b - c1.b * c2.a;
	if (d != 0) {
		*r = calloc(1, sizeof(**r));
		(*r)->x = (c1.c * c2.b - c1.b * c2.c) / d;
		(*r)->ty = (c1.a * c2.c - c1.c * c2.a) / d;
	}
}

/* Calculate the intersections of each line and whether they should be used for calculation */
void predictor_areas(XLine *lines, int time) {
	lines->areas = calloc(++lines->len, sizeof(*lines->areas));
	*(lines->areas + lines->len - 1) = 0;
	for (int i = 0; i < predictor.shapes_len; i++) {
		if (!(time >= (predictor.shapes + i)->start && time < (predictor.shapes + i)->end)) {
			continue;
		}
		for (int j = 0; j < (predictor.shapes + i)->points.len; j++) {
			Vector p1_l1 = *((predictor.shapes + i)->points.vectors + j);
			Vector p2_l1 = j != (predictor.shapes + i)->points.len - 1 ? *((predictor.shapes + i)->points.vectors + j + 1) : *((predictor.shapes + i)->points.vectors + 0);
			if (!((p1_l1.ty >= time && p2_l1.ty <= time) || (p1_l1.ty <= time && p2_l1.ty >= time))) {
				continue;
			}

			Coefficient c1;
			predictor_line(&c1, p1_l1, p2_l1);

			Vector p1_l2 = { .x = -1, .ty = time };
			Vector p2_l2 = { .x = 1, .ty = time };
			Coefficient c2;
			predictor_line(&c2, p1_l2, p2_l2);

			Vector *r = NULL;
			predictor_intersection(&r, c1, c2);
			if (r == NULL) {
				continue;
			}
			if (r->x >= 0 && r->x <= 512) {
				lines->areas = realloc(lines->areas, ++lines->len * sizeof(*lines->areas));
				*(lines->areas + lines->len - 1) = r->x;
			}
			free(r);
		}
	}
	for (int i = 0; i < lines->len - 1; i++) {
		for (int j = 0; j < lines->len - i - 1; j++) {
			if (*(lines->areas + j) > *(lines->areas + j + 1)) {
				unsigned short temp = *(lines->areas + j + 1);
				*(lines->areas + j + 1) = *(lines->areas + j);
				*(lines->areas + j) = temp;
			}
		}
	}
	lines->areas = realloc(lines->areas, ++lines->len * sizeof(*lines->areas));
	*(lines->areas + lines->len - 1) = 512;
	if (lines->len == 2) {
		exit(1);
	}
}
// End of stackoverflow

/* Either we create a new Juice Stream and have a minimum of 3 nested objects; or if the previously created Juice Stream is not long enough then we can keep expanding it */
void predictor_generatejs(CatchHitObject **bnpd, unsigned int *bnpd_len, int start_time, int end_time, Beatmap beatmap) {
	// TODO fix issue where Juice Stream is over the end time
	// TODO Figure out how to make optimisations/allow for the best of both worlds for the current optimisations
	// 1 - smallest being 3 nested objects  (easier load but more population)
	// 2 - biggest hitting the end time     (less population but load at end of bnprdctr)

	static unsigned int index = 0;
	HitObject *slider_hit_object = calloc(1, sizeof(*slider_hit_object));
	slider_hit_object->x = ((predictor.jspoints + index)->points.vectors + 0)->x;
	slider_hit_object->y = ((predictor.jspoints + index)->points.vectors + 0)->ty;
	slider_hit_object->time = start_time;
	slider_hit_object->type = nc_slider;
	slider_hit_object->hit_sound = 0;
	slider_hit_object->ho.slider.curve_type = slidertype_linear;
	slider_hit_object->ho.slider.curves = NULL;
	slider_hit_object->ho.slider.num_curve = 0;
	slider_hit_object->ho.slider.slides = 1;
	// We can keep increasing the length until we get the amount of droplets/tiny droplets needed            
	slider_hit_object->ho.slider.length = 1;
	slider_hit_object->hit_sample.normal_set = 0;
	slider_hit_object->hit_sample.addition_set = 0;
	slider_hit_object->hit_sample.index = 0;
	slider_hit_object->hit_sample.volume = 0;
	slider_hit_object->hit_sample.filename = NULL;
	
	InheritedTimingPoint inherited;
	oos_inheritedpoint_init(&inherited, beatmap.timing_points, beatmap.num_tp);
	TimingPoint tp_inherited;
	oos_timingpoint_attime(&tp_inherited, start_time, inherited.tp, inherited.num);

	UninheritedTimingPoint uninherited;
	oos_uninheritedpoint_init(&uninherited, beatmap.timing_points, beatmap.num_tp);
	TimingPoint tp_uninherited;
	oos_timingpoint_attime(&tp_uninherited, start_time, uninherited.tp, uninherited.num);

	bool make_new = true;

	// Extend current slider
	if ((*bnpd + 0)->type == catchhitobject_juicestream && (*bnpd + 0)->cho.js.slider_data.end_time < end_time) {
		unsigned int nested_num = (*bnpd + 0)->cho.js.num_nested;
		slider_hit_object->x = (*bnpd + 0)->cho.js.slider_data.start_position.x;
		slider_hit_object->time = (*bnpd + 0)->cho.js.slider_data.start_time;
		slider_hit_object->ho.slider.curve_type = (*bnpd + 0)->cho.js.slider_data.ho_data->curve_type;
		slider_hit_object->ho.slider.num_curve = (*bnpd + 0)->cho.js.slider_data.ho_data->num_curve;
		slider_hit_object->ho.slider.curves = calloc(slider_hit_object->ho.slider.num_curve, sizeof(*slider_hit_object->ho.slider.curves));
		for (int i = 0; i < slider_hit_object->ho.slider.num_curve; i++) {
			(slider_hit_object->ho.slider.curves + i)->x = ((*bnpd + 0)->cho.js.slider_data.ho_data->curves + i)->x;
			(slider_hit_object->ho.slider.curves + i)->y = ((*bnpd + 0)->cho.js.slider_data.ho_data->curves + i)->y;
		}
		slider_hit_object->ho.slider.slides = (*bnpd + 0)->cho.js.slider_data.span_count;
		slider_hit_object->ho.slider.length = (*bnpd + 0)->cho.js.slider_data.path.distance;

		while (true) {
			free((*bnpd + 0)->cho.js.nested);
			(*bnpd + 0)->cho.js.nested = NULL;
			(*bnpd + 0)->cho.js.num_nested = 0;
			oos_slider_calculateslider(&(*bnpd + 0)->cho.js.slider_data, *beatmap.difficulty, tp_inherited, tp_uninherited, *slider_hit_object);
			ooc_juicestream_createnestedjuice((*bnpd + 0));
			if ((*bnpd + 0)->cho.js.num_nested > nested_num) {
				make_new = false;
				oos_hitobject_freebulk(slider_hit_object, 1);
				break;
			} else if ((*bnpd + 0)->cho.js.slider_data.end_time >= end_time) {
				// If unable to extend slider anymore
				break;
			}
			slider_hit_object->ho.slider.length++;
		}
	}

	// Make a new slider
	if (make_new) {
		if (slider_hit_object->ho.slider.curves != NULL) {
			free(slider_hit_object->ho.slider.curves);
		}
		slider_hit_object->x = ((predictor.jspoints + index)->points.vectors + 0)->x;
		slider_hit_object->ho.slider.num_curve = (predictor.jspoints + index)->points.len - 1;
		slider_hit_object->ho.slider.curves = calloc(slider_hit_object->ho.slider.num_curve, sizeof(*slider_hit_object->ho.slider.curves));
		for (int i = 0; i < (predictor.jspoints + index)->points.len - 1; i++) {
			(slider_hit_object->ho.slider.curves + i)->x = ((predictor.jspoints + index)->points.vectors + i + 1)->x;
			(slider_hit_object->ho.slider.curves + i)->y = ((predictor.jspoints + index)->points.vectors + i + 1)->ty;
		}
		
		*bnpd = realloc(*bnpd, ++*bnpd_len * sizeof(**bnpd));
		for (int i = *bnpd_len - 1; i > 0; i--) {
			*(*bnpd + i) = *(*bnpd + i - 1);
		}
		ooc_juicestream_initwsliderspecific((*bnpd + 0), *beatmap.difficulty, tp_inherited, tp_uninherited, slider_hit_object);

		while (true) {
			ooc_juicestream_createnestedjuice((*bnpd + 0));
			if ((*bnpd + 0)->cho.js.num_nested > 3) {
				break;
			}
			slider_hit_object->ho.slider.length++;
			free((*bnpd + 0)->cho.js.nested);
			(*bnpd + 0)->cho.js.nested = NULL;
			(*bnpd + 0)->cho.js.num_nested = 0;
			oos_slider_calculateslider(&(*bnpd + 0)->cho.js.slider_data, *beatmap.difficulty, tp_inherited, tp_uninherited, *slider_hit_object);
		}

		index = (index + 1) % predictor.jspoints_len;
	}

	oos_inheritedpoint_free(inherited);
	oos_uninheritedpoint_free(uninherited);
}

/* Returns if we need to retry or store the current positions of the objects */
bool predictor_breakout(XLine lines, CatchHitObject *bnpd, unsigned int bnpd_len) {
	// Look at bananas currently generated
	for (int i = 0; i < (bnpd + bnpd_len - 1)->cho.bs.num_banana; i++) {
		bool correct_area = false;
		int banana_x = (int) ((bnpd + bnpd_len - 1)->cho.bs.bananas + i)->x + (int) ((bnpd + bnpd_len - 1)->cho.bs.bananas + i)->x_offset;
		// Look at the specific axis it's trying to meet
		for (int j = 0; j < lines.len - 1; j++) {
			unsigned short left = *(lines.areas + j);
			unsigned short right = *(lines.areas + j + 1);
			if (banana_x >= left && banana_x <= right) {
				// If the number is odd; we know it's in the right position
				// If the number is even; we know it's in the wrong position
				correct_area = j % 2 != 0;
				break;
			}
		}
		if (!correct_area) {
			return false;
		}
	}
	return true;
}

/* Outputs data */
void predictor_output(CatchHitObject object) {
	if (!predictor.prefer_circles) {
		char *output = NULL;
		ofb_hitobject_catchtostring(&output, object);
		fprintf(predictor.output, "%s", output);
		free(output);
	} else {
		if (object.type == catchhitobject_bananashower) {
			for (int j = 0; j < object.cho.bs.num_banana; j++) {
				int banana_x = (int) ((object.cho.bs.bananas + j)->x + (object.cho.bs.bananas + j)->x_offset);
				fprintf(predictor.output, "%d,192,%d,5,0\n", banana_x, (int) (object.cho.bs.bananas + j)->start_time);
			}
		}
	}
}

/* Output the object generated and saves the rng */
void predictor_saverng(LegacyRandom *rng, CatchHitObject *bnpd, unsigned int bnpd_len, LegacyRandom test_rng) {
	*rng = test_rng;
	for (int i = 0; i < bnpd_len; i++) {
		predictor_output(*(bnpd + i));
	}
}

/* Gets the values of the beatmap and svaes rng based on the converted catch object */
void predictor_beatmap(LegacyRandom *rng, Beatmap *beatmap, int index) {
	CatchHitObject object = {0};
	switch ((beatmap->hit_objects + index)->type) {
		case circle:
		case nc_circle:
			ooc_fruit_init(&object, (beatmap->hit_objects + index));
			break;

		case slider:
		case nc_slider:
			ooc_juicestream_initwslidertp(&object, *beatmap->difficulty, beatmap->timing_points, beatmap->num_tp, (beatmap->hit_objects + index));
			ooc_juicestream_createnestedjuice(&object);
			break;
		
		case spinner:
		case nc_spinner:
			ooc_bananashower_init(&object, (beatmap->hit_objects + index));
			ooc_bananashower_createnestedbananas(&object);
			break;
	}
	ooc_processor_applypositionoffsetrngstarttime(&object, index, index + 1, rng, false);
	if (predictor.record_objects) {
		predictor_output(object);
	}

	if (object.type == catchhitobject_juicestream) {
		ooc_juicestream_free(object.cho.js);
	} else if (object.type == catchhitobject_bananashower) {
		ooc_bananashower_free(object.cho.bs);
	}
}

void predictor_main(void) {
	// Evaluate Objects and RNG from the Beatmap
	if (predictor.beatmap == NULL) {
		return;
	}
	Beatmap beatmap = {0};
	of_beatmap_init(&beatmap);
	of_beatmap_set(&beatmap, predictor.beatmap);

	// We need the RNG of the beatmap until the point that we start
	LegacyRandom rng;
	ou_legacyrandom_init(&rng, ooc_processor_RNGSEED);

	// Get time where we should start and stop the loops
	int shapes_start, shapes_end;
	predictor_shapes(&shapes_start, &shapes_end);
	
	int i = 0;
	double j = shapes_start;
	while (i < beatmap.num_ho || j < shapes_end) {
		// Update Progress Bar
		if (predictor.output != stdout) {
			double beatmap_objects = beatmap.num_ho > 0 ? ((double) i / beatmap.num_ho) : 1;
			double shapes_objects = j < shapes_end ? (j - shapes_start) / (shapes_end - shapes_start) : 1;
			predictor_progressbar((beatmap_objects * shapes_objects) * 100);
		}
		
		// Getting current time
		bool is_shape = false;
		int time = INT_MAX;
		if (beatmap.num_ho > i && beatmap.hit_objects != NULL) {
			time = (beatmap.hit_objects + i)->time;
			is_shape = false;
		}
		if (time > (int) j) {
			time = (int) j;
			is_shape = true;
		}
		
		// Evaluating BananaPredictor
		if (is_shape && time >= shapes_start && time < shapes_end) {
			// Recording the areas of the x-axis at the current time which can place the bananas
			XLine lines = {0};
			predictor_areas(&lines, time);

			// Make a variable to store the new objects and house the Banana Shower
			unsigned int bnpd_len = 0;
			CatchHitObject *bnpd = calloc(++bnpd_len, sizeof(*bnpd));
			HitObject *bnpd_shower = calloc(1, sizeof(*bnpd_shower));
			bnpd_shower->x = 256;
			bnpd_shower->y = 192;
			bnpd_shower->time = (int) j;
			bnpd_shower->type = spinner;
			bnpd_shower->hit_sound = 0;
			bnpd_shower->ho.spinner.end_time = (int) j + 1;
			bnpd_shower->hit_sample.normal_set = 0;
			bnpd_shower->hit_sample.addition_set = 0;
			bnpd_shower->hit_sample.index = 0;
			bnpd_shower->hit_sample.volume = 0;
			bnpd_shower->hit_sample.filename = NULL;
			ooc_bananashower_init((bnpd + 0), bnpd_shower);
			ooc_bananashower_createnestedbananas((bnpd + 0));

			while (true) {
				// Apply the RNG to the new objects
				LegacyRandom test_rng = rng;
				ooc_processor_applypositionoffsetrng(bnpd, bnpd_len, &test_rng, false);

				// Check if they are within the areas we wanted them to be in; if not, we generate the Juice Stream and repeat the loop
				if (!predictor_breakout(lines, bnpd, bnpd_len)) {
					// Create new Juice Stream or, if previous object is a Juice Stream, expand on said Juice Stream
					predictor_generatejs(&bnpd, &bnpd_len, (int) j, shapes_end, beatmap);
					continue;
				}

				// Saves the rng and outputs the current saved data
				predictor_saverng(&rng, bnpd, bnpd_len, test_rng);
				break;
			}

			if (bnpd != NULL) {
				ooc_hitobject_freebulk(bnpd, bnpd_len);
			}
			if (lines.areas != NULL) {
				free(lines.areas);
			}
			j += predictor.distance;
			continue;
		}

		// Processes next object in beatmap
		predictor_beatmap(&rng, &beatmap, i);
		i++;
	}
	if (predictor.output != stdout) {
		predictor_progressbar(100);
		fprintf(stdout, "\n");
	}


	// Free
	of_beatmap_free(beatmap);
}