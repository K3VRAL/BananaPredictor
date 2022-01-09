#include "run.h"

Objects obj = { NULL, NULL, 0, 0, NULL, NULL, 0, 0 };

void processTarget(char *target, char *output) {
    FILE *fp = fopen(target, "r");
    if (!fp) { perror(target); exit(EXIT_FAILURE); }
    
    obj.tFile = strdup(target);
    obj.oFile = strdup(output);

    size_t curLine = -1;

    char line[4096];

    bool afterTP = false, afterHO = false;
    while (fgets(line, sizeof (line), fp) != NULL) {
        curLine++;

        for (int i = 0; i < strlen(line); i++) {
            if (line[i] == '\t' || line[i] == '\n' || line[i] == '\r') {
                for (int j = i; j < strlen(line); j++) {
                    line[j] = line[j + 1];
                }
            }
        }

        if ((obj.sliderMultiplier == 0 && strstr(line, "SliderMultiplier:") != NULL) || (obj.sliderTickrate == 0 && strstr(line, "SliderTickRate:") != NULL)) {
            char *copy = strdup(line);
            strtok(copy, ":");
            if (strstr(line, "SliderMultiplier:") != NULL) {
                obj.sliderMultiplier = atof(strtok(NULL, ":"));
            } else if (strstr(line, "SliderTickRate:") != NULL) {
                obj.sliderTickrate = atof(strtok(NULL, ":"));
            }
            free(copy);
        }

        if (!afterTP && !strcmp(line, "[TimingPoints]")) {
            afterTP = true;
            obj.lineTimingPoints = curLine;
            continue;
        } else if (!afterHO && !strcmp(line, "[HitObjects]")) {
            afterHO = true;
            obj.lineHitObjects = curLine;
            continue;
        }
        if (afterTP || afterHO) {
            if (line[0] == '\n' || line[0] == '\0') {
                afterTP = false;
                afterHO = false;
                continue;
            }
            int tlen = 0;
            char *copy = strdup(line);
            for (char *token = strtok(copy, ","); token != NULL; token = strtok(NULL, ",")) {
                tlen++;
            }
            free(copy);
            copy = strdup(line);
            Tag tag;
            TagID tagid;
            if (afterTP && tlen == 8) {
                tag.atp.time = atoi(strtok(copy, ","));
                tag.atp.beatlength = atof(strtok(NULL, ","));
                tag.atp.meter = atoi(strtok(NULL, ","));
                tag.atp.sset = atoi(strtok(NULL, ","));
                tag.atp.sindex = atoi(strtok(NULL, ","));
                tag.atp.volume = atoi(strtok(NULL, ","));
                tag.atp.uninherited = (bool)atoi(strtok(NULL, ","));
                tag.atp.effects = atoi(strtok(NULL, ","));
                obj.llTP = ll_add(obj.llTP, atp, tag);
            } else if (afterHO && (tlen == 6 || tlen == 8 || tlen == 11 || tlen == 7)) {
                tag.aho.x = atoi(strtok(copy, ","));
                tag.aho.y = atoi(strtok(NULL, ","));
                tag.aho.time = atoi(strtok(NULL, ","));
                // TODO May need to do more testing: `If it is not written, it defaults to 0:0:0:0:`
                if (tlen == 6) { // Circle
                    tag.aho.type = circle;
                    tag.aho.ncombo = atoi(strtok(NULL, ",")) == 5; // NCombo = 5, not = 1
                    tag.aho.hsound = atoi(strtok(NULL, ","));
                    // tag.aho.hobject.circle; // This is useless but is there just because
                } else if (tlen == 8 || tlen == 11) { // Slider
                    tag.aho.type = slider;
                    tag.aho.ncombo = atoi(strtok(NULL, ",")) == 6; // NCombo = 6, not = 2
                    tag.aho.hsound = atoi(strtok(NULL, ","));
                    // TODO need to do more testing with sliders and see what I can get away with
                } else if (tlen == 7) { // Spinner
                    tag.aho.type = spinner;
                    tag.aho.ncombo = atoi(strtok(NULL, ",")) == 12; // NCombo = 12, not = 8
                    tag.aho.hsound = atoi(strtok(NULL, ","));
                    tag.aho.hobject.spinner.endtime = atoi(strtok(NULL, ","));
                }
                tag.aho.hsample = malloc(4 * sizeof(int));
                *(tag.aho.hsample + 0) = atoi(strtok(NULL, ":"));
                *(tag.aho.hsample + 1) = atoi(strtok(NULL, ":"));
                *(tag.aho.hsample + 2) = atoi(strtok(NULL, ":"));
                *(tag.aho.hsample + 3) = atoi(strtok(NULL, ":"));
                obj.llHO = ll_add(obj.llHO, aho, tag);
            }
            free(copy);
        }
    }
    fclose(fp);
}

void addSpinners(listMap spin) {
}

void freeObjects() {
    ll_free(obj.llTP);
    ll_free(obj.llHO);
    free(obj.oFile);
    free(obj.tFile);
}

void executeBanana(char *target, char *output, listInput all) {
    // listInput *sp = &all;
    processTarget(target, output);
    for (int i = 0, size = ll_length(obj.llTP); i < size; i++) {
        fprintf(stdout, "TP: %d\n", ll_get(obj.llTP, i)->tag.atp.time);
    }
    for (int i = 0, size = ll_length(obj.llHO); i < size; i++) {
        fprintf(stdout, "HO: %d\n", ll_get(obj.llHO, i)->tag.aho.time);
    }
    // for (int i = 0; i < sp->numAll; i++) {
    //     addSpinners((sp->listA + i)->listM);
    // }

    // const int RNG_SEED = 1337; //   []|< |>[-|>|>`/     thanks https://md5decrypt.net/en/Leet-translator/
    // FastRandom(RNG_SEED);

    freeObjects();
}