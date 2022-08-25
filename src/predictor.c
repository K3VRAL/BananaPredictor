#include "../include/predictor.h"

Predictor predictor = {
    .start = 0,
    .end = 1,
    .length = 1,
    .distance = 0,
    .invert = false,
    .points = NULL,
    .points_len = 0
};

void predictor_run(void) {
    // Init
    Beatmap beatmap = of_beatmap_init();

    // Creating Spinners
    for (int i = predictor.start; i < predictor.end; i += predictor.distance) {
        HitObject ho = {
            .x = 256,
            .y = 192,
            .time = i,
            .type = spinner,
            .hit_sound = 0,
            .ho = {
                .spinner = {
                    .end_time = i + predictor.length,
                }
            },
        };
        
    }
    oos_hitobject_sort(beatmap.hit_objects, beatmap.num_ho);

    // Write to file
    FILE *fp = fopen("tmp.osu", "w");
    of_beatmap_tofile(&beatmap, fp);
    fclose(fp);
}