#ifndef PROJECT_H
#define PROJECT_H

#include <stdbool.h>

typedef struct {
    char *audio_file;
    int audio_lead_in;
    char *audio_hash;
    int preview_time;
    int count_down;
    char *sameple_set;
    double stack_leniency;
    int mode;
    bool

} General;

typedef struct {
    int *bookmarks;
    double beat_divisor;
    int grid_size;
    double timeline_zoom;
} Editor;

typedef struct {
    char *title;
    char *title_unicode;
    char *artist;
    char *artist_unicode;
    char *creator;
    char *version;
    char **tags;
    int beatmap_id;
    int beatmap_set_id;
} MetaData;

typedef struct {
    
} Project;

#endif