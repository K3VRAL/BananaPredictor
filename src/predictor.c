#include "predictor.h"

#include <osu.h>
#include <stdio.h>
#include <stdbool.h>
#include <limits.h>
#ifdef __unix__
#include <sys/ioctl.h>
#endif
#include <unistd.h>
#include <math.h>

Predictor predictor = {
	.beatmap = NULL,

	.output = NULL,
	.output_beatmap = false,

	.shapes = NULL,
	.shapes_len = 0,

	.points_type = '\0',
	.points.ho = NULL,	// set to ho because then compiler starts complaining
	.points_len = 0,
	.nonvisual = false,

	.distance = 1,

	.prefer_circles = false,
	.record_objects = false,
	.read_until = 0,
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
	if (predictor.points_type == juice_stream) {
		for (int i = 0; i < predictor.points_len; i++) {
			if ((predictor.points.js + i)->length == 0) {
				(predictor.points.js + i)->length = *shapes_end;
			}
		}
	}
}

/* Prints out the progress bar in the terminal. Resizing the terminal will also resize the output */
void predictor_progressbar(unsigned int percent) {
#ifdef __unix__
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
	fprintf(stdout, "] ");
#endif
	fprintf(stdout, "%d%%", percent);
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
Coefficient predictor_line(Vector p1, Vector p2) {
	Coefficient coefficient = {
		.a = p1.ty - p2.ty,
		.b = p2.x - p1.x,
		.c = -((p1.x * p2.ty) - (p2.x * p1.ty))
	};
	return coefficient;
}

/* Calculating the Line of Intersection with Determinants */
Vector *predictor_intersection(Coefficient c1, Coefficient c2) {
	Vector *r = NULL;
	double d = c1.a * c2.b - c1.b * c2.a;
	if (d != 0) {
		r = calloc(1, sizeof(*r));
		r->x = round((c1.c * c2.b - c1.b * c2.c) / d);
		r->ty = (c1.a * c2.c - c1.c * c2.a) / d;
	}
	return r;
}

/* Calculate the intersections of each line and whether they should be used for calculation */
XLine predictor_areas(int time) {
	XLine *lines = NULL;
	for (int i = 0; i < predictor.shapes_len; i++) {
		lines = realloc(lines, (i + 1) * sizeof(*lines));
		(lines + i)->areas = NULL;
		(lines + i)->len = 0;

		if (!(time >= (predictor.shapes + i)->start && time < (predictor.shapes + i)->end)) {
			continue;
		}
		for (int j = 0; j < (predictor.shapes + i)->points.len; j++) {
			Vector p1_l1 = *((predictor.shapes + i)->points.vectors + j);
			Vector p2_l1 = j != (predictor.shapes + i)->points.len - 1 ? *((predictor.shapes + i)->points.vectors + j + 1) : *((predictor.shapes + i)->points.vectors + 0);
			if (!((p1_l1.ty >= time && p2_l1.ty <= time) || (p1_l1.ty <= time && p2_l1.ty >= time))) {
				continue;
			}

			Coefficient c1 = predictor_line(p1_l1, p2_l1);

			Vector p1_l2 = { .x = -1, .ty = time };
			Vector p2_l2 = { .x = 1, .ty = time };
			Coefficient c2 = predictor_line(p1_l2, p2_l2);

			Vector *r = predictor_intersection(c1, c2);
			if (r == NULL) {
				continue;
			}
			if (r->x >= 0 && r->x <= 512) {
				bool found = false;
				for (int k = 0; k < (lines + i)->len; k++) {
					if (r->x == *((lines + i)->areas + k)) {
						found = true;
						break;
					}
				}
				if (found) {
					free(r);
					continue;
				}

				(lines + i)->areas = realloc((lines + i)->areas, ++(lines + i)->len * sizeof(*(lines + i)->areas));
				*((lines + i)->areas + (lines + i)->len - 1) = r->x;
			}
			free(r);
		}

		for (int j = 0; j < (lines + i)->len - 1; j++) {
			for (int k = 0; k < (lines + i)->len - j - 1; k++) {
				if (*((lines + i)->areas + k) > *((lines + i)->areas + k + 1)) {
					unsigned short temp = *((lines + i)->areas + k);
					*((lines + i)->areas + k) = *((lines + i)->areas + k + 1);
					*((lines + i)->areas + k + 1) = temp;
				}
			}
		}
	}

	XLine final = {0};
	final.len = 0;
	final.areas = realloc(final.areas, ++final.len * sizeof(*final.areas));
	*(final.areas + final.len - 1) = 0;
	final.areas = realloc(final.areas, ++final.len * sizeof(*final.areas));
	*(final.areas + final.len - 1) = 512;
	for (int i = 0; i < predictor.shapes_len; i++) {
		if ((lines + i)->len <= 1) {
			continue;
		}

		bool found = false;
		for (int j = 0; j < (lines + i)->len - 1; j += 2) {
			unsigned short lines_s = *((lines + i)->areas + j);
			unsigned short lines_e = *((lines + i)->areas + j + 1);

			for (int k = 1; k < final.len - 1; k += 2) {
				unsigned short *final_s = (final.areas + k);
				unsigned short *final_e = (final.areas + k + 1);

				if (((lines_s >= *final_s && lines_s >= *final_e) || (lines_e <= *final_s && lines_e <= *final_e))) {
					if (lines_s == *final_s || lines_e == *final_e) {
						found = true;
					}
					continue;
				}

				if (lines_s < *final_s) {
					*final_s = lines_s;
				}

				if (lines_e > *final_e) {
					*final_e = lines_e;
				}

				found = true;
			}

			if (!found) {
				final.areas = realloc(final.areas, ++final.len * sizeof(*final.areas));
				*(final.areas + final.len - 1) = lines_s;
				final.areas = realloc(final.areas, ++final.len * sizeof(*final.areas));
				*(final.areas + final.len - 1) = lines_e;

				for (int j = 0; j < final.len - 1; j++) {
					for (int k = 0; k < final.len - j - 1; k++) {
						if (*(final.areas + k) > *(final.areas + k + 1)) {
							unsigned short temp = *(final.areas + k);
							*(final.areas + k) = *(final.areas + k + 1);
							*(final.areas + k + 1) = temp;
						}
					}
				}
			}
		}
	}

	for (int i = 0; i < predictor.shapes_len; i++) {
		free((lines + i)->areas);
	}
	free(lines);

	if (final.len == 2) {
		fprintf(stdout, "ERROR: Not enough lines were provided for processing.\n");
		exit(1);
	}
	return final;
}
// End of stackoverflow

void predictor_generateho(CatchHitObject **bnpd, unsigned int *bnpd_len, int start_time, unsigned int *index) {
	HitObject *circle_hit_object = calloc(1, sizeof(*circle_hit_object));
	circle_hit_object->x = (predictor.points.ho + *index)->x;
	circle_hit_object->y = (predictor.points.ho + *index)->y;
	circle_hit_object->time = !predictor.nonvisual ? start_time : INT_MIN;
	circle_hit_object->type = nc_circle;

	*bnpd = realloc(*bnpd, ++*bnpd_len * sizeof(**bnpd));
	for (int i = *bnpd_len - 1; i > 0; i--) {
		*(*bnpd + i) = *(*bnpd + i - 1);
	}

	ooc_fruit_init((*bnpd + 0), circle_hit_object);

	*index = (*index + 1) % predictor.points_len;
}

/* Either we create a new Juice Stream and have a minimum of 3 nested objects; or if the previously created Juice Stream is not long enough then we can keep expanding it */
void predictor_generatejs(CatchHitObject **bnpd, unsigned int *bnpd_len, int start_time, Beatmap beatmap, unsigned int *index) {
	HitObject *slider_hit_object = calloc(1, sizeof(*slider_hit_object));
	slider_hit_object->x = ((predictor.points.js + *index)->points.vectors + 0)->x;
	slider_hit_object->y = ((predictor.points.js + *index)->points.vectors + 0)->ty;
	slider_hit_object->time = !predictor.nonvisual ? start_time : INT_MIN;
	slider_hit_object->type = nc_slider;
	slider_hit_object->ho.slider.curve_type = (predictor.points.js + *index)->type;
	slider_hit_object->ho.slider.curves = NULL;
	slider_hit_object->ho.slider.num_curve = 0;
	slider_hit_object->ho.slider.slides = 1;
	slider_hit_object->ho.slider.length = (predictor.nonvisual * INT_MIN) + 1;	// We can keep increasing the length until we get the amount of droplets/tiny droplets needed
	
	InheritedTimingPoint inherited;
	oos_inheritedpoint_init(&inherited, beatmap.timing_points, beatmap.num_tp);
	TimingPoint tp_inherited;
	oos_timingpoint_attime(&tp_inherited, start_time, inherited.tp, inherited.num);

	UninheritedTimingPoint uninherited;
	oos_uninheritedpoint_init(&uninherited, beatmap.timing_points, beatmap.num_tp);
	TimingPoint tp_uninherited;
	oos_timingpoint_attime(&tp_uninherited, start_time, uninherited.tp, uninherited.num);

	bool make_new = true;

	// Extend current slider to optimise slider amount placed
	if ((*bnpd + 0)->type == catchhitobject_juicestream && (*bnpd + 0)->cho.js->slider_data->end_time < (predictor.points.js + *index)->length) {
		unsigned int nested_num = (*bnpd + 0)->cho.js->num_nested;
		slider_hit_object->x = (*bnpd + 0)->cho.js->slider_data->start_position.x;
		slider_hit_object->time = (*bnpd + 0)->cho.js->slider_data->start_time;
		slider_hit_object->ho.slider.curve_type = (*bnpd + 0)->cho.js->slider_data->ho_data->curve_type;
		slider_hit_object->ho.slider.num_curve = (*bnpd + 0)->cho.js->slider_data->ho_data->num_curve;
		slider_hit_object->ho.slider.curves = calloc(slider_hit_object->ho.slider.num_curve, sizeof(*slider_hit_object->ho.slider.curves));
		for (int i = 0; i < slider_hit_object->ho.slider.num_curve; i++) {
			(slider_hit_object->ho.slider.curves + i)->x = ((*bnpd + 0)->cho.js->slider_data->ho_data->curves + i)->x;
			(slider_hit_object->ho.slider.curves + i)->y = ((*bnpd + 0)->cho.js->slider_data->ho_data->curves + i)->y;
		}
		slider_hit_object->ho.slider.slides = (*bnpd + 0)->cho.js->slider_data->span_count;
		slider_hit_object->ho.slider.length = (*bnpd + 0)->cho.js->slider_data->path.distance;

		while (true) {
			free((*bnpd + 0)->cho.js->nested);
			(*bnpd + 0)->cho.js->nested = NULL;
			(*bnpd + 0)->cho.js->num_nested = 0;
			oos_slider_calculateslider((*bnpd + 0)->cho.js->slider_data, *beatmap.difficulty, tp_inherited, tp_uninherited, *slider_hit_object);
			ooc_juicestream_createnestedjuice((*bnpd + 0));
			if ((*bnpd + 0)->cho.js->num_nested > nested_num) {
				make_new = false;
				oos_hitobject_freebulk(slider_hit_object, 1);
				break;
			} else if ((*bnpd + 0)->cho.js->slider_data->end_time >= (predictor.points.js + *index)->length) {
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
		slider_hit_object->x = ((predictor.points.js + *index)->points.vectors + 0)->x;
		slider_hit_object->ho.slider.num_curve = (predictor.points.js + *index)->points.len - 1;
		slider_hit_object->ho.slider.curves = calloc(slider_hit_object->ho.slider.num_curve, sizeof(*slider_hit_object->ho.slider.curves));
		for (int i = 0; i < (predictor.points.js + *index)->points.len - 1; i++) {
			(slider_hit_object->ho.slider.curves + i)->x = ((predictor.points.js + *index)->points.vectors + i + 1)->x;
			(slider_hit_object->ho.slider.curves + i)->y = ((predictor.points.js + *index)->points.vectors + i + 1)->ty;
		}
		
		*bnpd = realloc(*bnpd, ++*bnpd_len * sizeof(**bnpd));
		for (int i = *bnpd_len - 1; i > 0; i--) {
			*(*bnpd + i) = *(*bnpd + i - 1);
		}
		ooc_juicestream_initwsliderspecific((*bnpd + 0), *beatmap.difficulty, tp_inherited, tp_uninherited, slider_hit_object);
		static int length = 0;
		if (length != 0) {
			slider_hit_object->ho.slider.length = length;
		}
		while (true) {
			ooc_juicestream_createnestedjuice((*bnpd + 0));
			if ((*bnpd + 0)->cho.js->num_nested > 3) {
				break;
			}
			slider_hit_object->ho.slider.length++;
			free((*bnpd + 0)->cho.js->nested);
			(*bnpd + 0)->cho.js->nested = NULL;
			(*bnpd + 0)->cho.js->num_nested = 0;
			oos_slider_calculateslider((*bnpd + 0)->cho.js->slider_data, *beatmap.difficulty, tp_inherited, tp_uninherited, *slider_hit_object);
		}
		if (length == 0) {
			length = slider_hit_object->ho.slider.length;
		}

		*index = (*index + 1) % predictor.points_len;
	}

	oos_inheritedpoint_free(inherited);
	oos_uninheritedpoint_free(uninherited);
}

void predictor_generatebs(CatchHitObject **bnpd, unsigned int *bnpd_len, int start_time, unsigned int *index) {
	HitObject *spinner_hit_object = calloc(1, sizeof(*spinner_hit_object));
	spinner_hit_object->x = 256;
	spinner_hit_object->y = 192;
	spinner_hit_object->time = !predictor.nonvisual ? (predictor.points.bs + *index)->time : INT_MIN;
	spinner_hit_object->type = nc_spinner;
	spinner_hit_object->ho.spinner.end_time = (!predictor.nonvisual ? (predictor.points.bs + *index)->time : INT_MIN) + 1;

	bool make_new = false;

	// Extend current slider to optimise slider amount placed
	if ((*bnpd + 0)->type == catchhitobject_bananashower && (*bnpd + 0)->cho.bs->end_time < (predictor.points.bs + *index)->time + (predictor.points.bs + *index)->length) {
		unsigned int banana_num = (*bnpd + 0)->cho.bs->num_banana;
		spinner_hit_object->ho.spinner.end_time = (*bnpd + 0)->cho.bs->end_time;

		while (true) {
			free((*bnpd + 0)->cho.bs->bananas);
			(*bnpd + 0)->cho.bs->bananas = NULL;
			(*bnpd + 0)->cho.bs->num_banana = 0;
			ooc_bananashower_init((*bnpd + 0), spinner_hit_object);
			ooc_bananashower_createnestedbananas((*bnpd + 0));
			if ((*bnpd + 0)->cho.bs->num_banana > banana_num) {
				make_new = false;
				oos_hitobject_freebulk(spinner_hit_object, 1);
				break;
			} else if ((*bnpd + 0)->cho.bs->end_time >= (predictor.points.bs + *index)->time + (predictor.points.bs + *index)->length) {
				// If unable to extend slider anymore
				break;
			}
			spinner_hit_object->ho.spinner.end_time++;
		}
	}

	if (make_new) {
		*bnpd = realloc(*bnpd, ++*bnpd_len * sizeof(**bnpd));
		for (int i = *bnpd_len - 1; i > 0; i--) {
			*(*bnpd + i) = *(*bnpd + i - 1);
		}

		ooc_bananashower_init((*bnpd + 0), spinner_hit_object);
		ooc_bananashower_createnestedbananas((*bnpd + 0));

		*index = (*index + 1) % predictor.points_len;
	}
}

/* Returns if we need to retry or store the current positions of the objects */
bool predictor_breakout(XLine lines, CatchHitObject *bnpd, unsigned int bnpd_len) {
	// Look at bananas currently generated
	for (int i = 0; i < (bnpd + bnpd_len - 1)->cho.bs->num_banana; i++) {
		bool correct_area = false;
		int banana_x = (int) ((bnpd + bnpd_len - 1)->cho.bs->bananas + i)->x + (int) ((bnpd + bnpd_len - 1)->cho.bs->bananas + i)->x_offset;
		// Look at the specific axis it's trying to meet
		for (int j = 0; j < lines.len - 1; j++) {
			unsigned short left = *(lines.areas + j);
			unsigned short right = *(lines.areas + j + 1);
			if (banana_x >= left && banana_x < right) {
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
			for (int j = 0; j < object.cho.bs->num_banana; j++) {
				int banana_x = (int) ((object.cho.bs->bananas + j)->x + (object.cho.bs->bananas + j)->x_offset);
				fprintf(predictor.output, "%d,192,%d,5,0\n", banana_x, (int) (object.cho.bs->bananas + j)->start_time);
			}
		}
	}
}

/* Gets the values of the beatmap and svaes rng based on the converted catch object */
void predictor_beatmap(LegacyRandom *rng, Beatmap beatmap, int index, float **last_position, double *last_start_time) {
	CatchHitObject object = {0};
	switch ((beatmap.hit_objects + index)->type) {
		case circle:
		case nc_circle:
			ooc_fruit_init(&object, (beatmap.hit_objects + index));
			break;

		case slider:
		case nc_slider:
			ooc_juicestream_initwslidertp(&object, *beatmap.difficulty, beatmap.timing_points, beatmap.num_tp, (beatmap.hit_objects + index));
			ooc_juicestream_createnestedjuice(&object);
			break;
		
		case spinner:
		case nc_spinner:
			ooc_bananashower_init(&object, (beatmap.hit_objects + index));
			ooc_bananashower_createnestedbananas(&object);
			break;
	}
	ooc_processor_applypositionoffsetrng(&object, 1, rng, predictor.points_type == hit_object, last_position, last_start_time);
	if (predictor.record_objects) {
		predictor_output(object);
	}

	ooc_hitobject_free(object);
}

void predictor_main(void) {
	// Evaluate Objects and RNG from the Beatmap
	if (predictor.beatmap == NULL) {
		return;
	}
	Beatmap beatmap = {0};
	of_beatmap_init(&beatmap);
	of_beatmap_set(&beatmap, predictor.beatmap);

	// If output with beatmap osu format; removing then putting back HitObjects so we can save beatmap settings without saving the HitObjects (we do that while processing)
	if (predictor.output_beatmap) {
		HitObject *objects = beatmap.hit_objects;
		unsigned int objects_num = beatmap.num_ho;
		beatmap.hit_objects = NULL;
		beatmap.num_ho = 0;
		of_beatmap_tofile(predictor.output, beatmap);
		fputs("[HitObjects]\n", predictor.output);
		beatmap.hit_objects = objects;
		beatmap.num_ho = objects_num;
	}

	// We need the RNG of the beatmap until the point that we start
	LegacyRandom rng;
	float *last_position = NULL;
	double last_start_time = 0;
	ou_legacyrandom_init(&rng, ooc_processor_RNGSEED);

	// Get time where we should start and stop the loops
	int shapes_start, shapes_end;
	predictor_shapes(&shapes_start, &shapes_end);
	
	unsigned int i = 0;
	double j = shapes_start;
	unsigned int obj_generation_index = 0;

	if (predictor.read_until > beatmap.num_ho) {
		fprintf(stdout, "ERROR: 'read_until' flag was passed with a higher number then can be processed; %u %u\n", beatmap.num_ho, predictor.read_until);
		return;
	}
	while (i < predictor.read_until) {
		// Processes next object in beatmap
		predictor_beatmap(&rng, beatmap, i, &last_position, &last_start_time);
		i++;
	}

	while (i < beatmap.num_ho || j < shapes_end) {

		// Update Progress Bar
		if (predictor.output != stdout) {
			double beatmap_objects = beatmap.num_ho > 0 ? ((double) i / beatmap.num_ho) : 1;
			double shapes_objects = j < shapes_end ? (j - shapes_start) / (shapes_end - shapes_start) : 1;
			predictor_progressbar((beatmap_objects * shapes_objects) * 100);
		}
		
		// Getting current time
		int time = INT_MAX;
		if (beatmap.num_ho > i && beatmap.hit_objects != NULL) {
			time = (beatmap.hit_objects + i)->time;
		}
		bool is_shape = false;
		if (time > (int) j) {
			time = (int) j;
			is_shape = true;
		}
		
		// Evaluating BananaPredictor
		if (is_shape && time >= shapes_start && time < shapes_end) {
			// Recording the areas of the x-axis at the current time which can place the bananas
			XLine lines = predictor_areas(time);

			// Make a variable to store the new objects and house the Banana Shower
			unsigned int bnpd_len = 0;
			CatchHitObject *bnpd = calloc(++bnpd_len, sizeof(*bnpd));
			HitObject *bnpd_shower = calloc(1, sizeof(*bnpd_shower));
			bnpd_shower->x = 256;
			bnpd_shower->y = 192;
			bnpd_shower->time = (int) j;
			bnpd_shower->type = nc_spinner;
			bnpd_shower->hit_sound = 0;
			bnpd_shower->ho.spinner.end_time = (int) j + 1;
			ooc_bananashower_init((bnpd + 0), bnpd_shower);
			ooc_bananashower_createnestedbananas((bnpd + 0));

			while (true) {
				// Apply the RNG to the new object
				LegacyRandom test_rng = rng;
				float *test_last_position = NULL;
				if (last_position != NULL) {
					test_last_position = malloc(sizeof(*test_last_position));
					*test_last_position = *last_position;
				}
				double test_last_start_time = last_start_time;
				ooc_processor_applypositionoffsetrng(bnpd, bnpd_len, &test_rng, predictor.points_type == hit_object, &test_last_position, &test_last_start_time);

				// Check if they are within the areas we wanted them to be in; if not, we generate an Object and repeat the loop
				if (!predictor_breakout(lines, bnpd, bnpd_len)) {
					switch (predictor.points_type) {
						case hit_object:
							predictor_generateho(&bnpd, &bnpd_len, (int) j, &obj_generation_index);
							break;

						case juice_stream:
							// If previous object is a Juice Stream, expand on said Juice Stream, else create new Juice Stream
							predictor_generatejs(&bnpd, &bnpd_len, (int) j, beatmap, &obj_generation_index);
							break;

						case banana_shower:
							predictor_generatebs(&bnpd, &bnpd_len, (int) j, &obj_generation_index);
							break;
					}
					if (test_last_position != NULL) {
						free(test_last_position);
					}
					continue;
				}

				// Saves the rng and outputs the current saved data
				rng = test_rng;
				if (test_last_position != NULL) {
					if (last_position != NULL) {
						free(last_position);
					}
					last_position = test_last_position;
				}
				last_start_time = test_last_start_time;
				for (int i = 0; i < bnpd_len; i++) {
					predictor_output(*(bnpd + i));
				}
				break;
			}

			if (bnpd != NULL) {
				ooc_hitobject_freebulkrefer(bnpd, bnpd_len);
				ooc_hitobject_freebulk(bnpd, bnpd_len);
			}
			if (lines.areas != NULL) {
				free(lines.areas);
			}
			j += predictor.distance;
			continue;
		}

		// Processes next object in beatmap
		predictor_beatmap(&rng, beatmap, i, &last_position, &last_start_time);
		i++;
	}
	if (predictor.output != stdout) {
		predictor_progressbar(100);
		fprintf(stdout, "\n");
	}
	if (last_position != NULL) {
		free(last_position);
	}

	// Free
	of_beatmap_free(beatmap);
}
