#include "run.h"

listInput *sp;
objects obj = { NULL, NULL, 0, 0, NULL, 0, NULL, 0 };
fileRelated fr = { 0, 0 };

void processTarget(char *target, char *output) {
    FILE *fp = fopen(target, "r");
    if (!fp) { perror(target); exit(EXIT_FAILURE); }
    
    obj.tFile = xrealloc(NULL, strlen(target) * sizeof (char) + 1);
    strcpy(obj.tFile, target);
    obj.oFile = xrealloc(NULL, strlen(output) * sizeof (char) + 1);
    strcpy(obj.oFile, output);

    size_t curLine = -1;

    obj.aho = xrealloc(NULL, sizeof (allHO));
    obj.atp = xrealloc(NULL, sizeof (allTP));

    char line[256];

    bool afterTP = false, afterHO = false;
    while (fgets(line, sizeof (line), fp) != NULL) {
        curLine++;
        int i = 0;
        while (i < strlen(line)) {
            if (line[i] == '\t' || line[i] == '\n' || line[i] == '\r') for (int j = i; j < strlen(line); j++) line[j] = line[j + 1];
            else i++;
        }
        if (strstr(line, "SliderMultiplier:") != NULL || strstr(line, "SliderTickRate:") != NULL) {
            char *delim = ":";
            char *copy = xrealloc(NULL, (strlen(line) + 1) * sizeof (char));
            strcpy(copy, line);
            char *token = strtok(copy, delim);
            token = strtok(NULL, delim);
            if (strstr(line, "SliderMultiplier:") != NULL) obj.smultiplier = atof(token);
            else if (strstr(line, "SliderTickRate:") != NULL) obj.stickrate = atof(token);
            while (token != NULL) token = strtok(NULL, delim);
        }
        if (!afterTP && strcmp(line, "[TimingPoints]") == 0) {
            afterTP = true;
            fr.tp = curLine;
            continue;
        }
        if (!afterHO && strcmp(line, "[HitObjects]") == 0) {
            afterHO = true;
            fr.ho = curLine;
            continue;
        }
        if (afterTP) {
            if (line[0] == '\0') {
                afterTP = false;
                continue;
            }
            char *delim = ",";
            size_t tlen = 0;
            char *copy = xrealloc(NULL, (strlen(line) + 1) * sizeof (char));
            strcpy(copy, line);
            char *token = strtok(copy, delim);
            if (token) {
                char **contents = xrealloc(NULL, sizeof (char *));
                while (token != NULL) {
                    contents = xrealloc(contents, (tlen + 1) * sizeof (char *));
                    *(contents + tlen) = xrealloc(NULL, (strlen(token) + 1) * sizeof (char));
                    strcpy(*(contents + tlen), token);
                    token = strtok(NULL, delim);
                    tlen++;
                }
                if (tlen == 8) {
                    obj.atp = xrealloc(obj.atp, (obj.numAtp + 1) * sizeof (allTP));

                    (obj.atp + obj.numAtp)->fileline = curLine;
                    (obj.atp + obj.numAtp)->line = xrealloc(NULL, (strlen(line) + 1) * sizeof (char));
                    strcpy((obj.atp + obj.numAtp)->line, line);
                    (obj.atp + obj.numAtp)->time = atoi(*contents);
                    (obj.atp + obj.numAtp)->beatLength = atof(*(contents + 1));
                    (obj.atp + obj.numAtp)->meter = atoi(*(contents + 2));
                    (obj.atp + obj.numAtp)->sset = atoi(*(contents + 3));
                    (obj.atp + obj.numAtp)->sindex = atoi(*(contents + 4));
                    (obj.atp + obj.numAtp)->volume = atoi(*(contents + 5));
                    (obj.atp + obj.numAtp)->uninherited = atoi(*(contents + 6)) == 1 ? true : false;
                    (obj.atp + obj.numAtp)->effects = atoi(*(contents + 7));
                }
                for (size_t i = 0; i < tlen; i++) free(*(contents + i));
                free(contents);
            }
            free(copy);

            obj.numAtp++;
        }
        if (afterHO) {
            if (line[0] == '\0') {
                afterHO = false;
                continue;
            }
            char *delim = ",";
            size_t tlen = 0;
            char *copy = xrealloc(NULL, (strlen(line) + 1) * sizeof (char));
            strcpy(copy, line);
            char *token = strtok(copy, delim);
            if (token) {
                char **contents = xrealloc(NULL, sizeof (char *));
                while (token != NULL) {
                    contents = xrealloc(contents, (tlen + 1) * sizeof (char *));
                    *(contents + tlen) = xrealloc(NULL, (strlen(token) + 1) * sizeof (char));
                    strcpy(*(contents + tlen), token);
                    token = strtok(NULL, delim);
                    tlen++;
                }
                if (tlen == 6 || tlen == 8 || tlen == 11 || tlen == 7) {
                    obj.aho = xrealloc(obj.aho, (obj.numAho + 1) * sizeof (allHO));
                    
                    int x = atoi(*contents),
                        y = atoi(*(contents + 1)),
                        time = atoi(*(contents + 2)),
                        ncombo = atoi(*(contents + 3)),
                        hsound = atoi(*(contents + 4));

                    // TODO fix then use this as it is more optimized and readable
                    // if ((tlen == 6 && (ncombo == 1 || ncombo == 5) && (rangeX >= 0 || rangeX <= 512) && (rangeY >= 0 || rangeY <= 384))  // Circle
                    // ||  (tlen == 8 && (ncombo == 2 || ncombo == 6) && (rangeX >= 0 || rangeX <= 512) && (rangeY >= 0 || rangeY <= 384))  // Slider
                    // ||  (tlen == 7 && (ncombo == 8 || ncombo == 12) && rangeX == 256 && rangeY == 192))                                  // Spinner
                    // {
                    //     (obj.aho + obj.numAho)->command = !(tlen == 7 && (ncombo == 8 || ncombo == 12) && rangeX == 256 && rangeY == 192) ? (rangeY /* do mathematics on this */) % 2 : ran;
                    // } else { perror(lines); exit(EXIT_FAILURE); }
                    if (tlen == 6                        // Circle
                        && (ncombo == 1 || ncombo == 5)
                        && (x >= 0 || x <= 512)
                        && (y >= 0 || y <= 384)) {
                        (obj.aho + obj.numAho)->type = circle;
                        (obj.aho + obj.numAho)->ncombo = ncombo == 5;
                    } else if ((tlen == 8 || tlen == 11)  // Slider
                        && (ncombo == 2 || ncombo == 6)
                        && (x >= 0 || x <= 512)
                        && (y >= 0 || y <= 384)) {
                        (obj.aho + obj.numAho)->type = slider;
                        (obj.aho + obj.numAho)->ncombo = ncombo == 6;
                    } else if (tlen == 7                 // Spinner
                        && (ncombo == 8 || ncombo == 12)
                        && x == 256 && y == 192) {
                        (obj.aho + obj.numAho)->type = spinner;
                        (obj.aho + obj.numAho)->ncombo = ncombo == 12;
                        (obj.aho + obj.numAho)->endtime = atoi(*(contents + 5));
                    } else { perror(line); exit(EXIT_FAILURE); }
                    (obj.aho + obj.numAho)->tlen = tlen;
                    (obj.aho + obj.numAho)->line = xrealloc(NULL, (strlen(line) + 1) * sizeof (char));
                    strcpy((obj.aho + obj.numAho)->line, line);
                    (obj.aho + obj.numAho)->fileline = curLine;
                    (obj.aho + obj.numAho)->x = x;
                    (obj.aho + obj.numAho)->y = y;
                    (obj.aho + obj.numAho)->time = time;
                    (obj.aho + obj.numAho)->hsound = hsound;
                }
                for (size_t i = 0; i < tlen; i++) free(*(contents + i));
                free(contents);
            }
            free(copy);

            obj.numAho++;
        }
    }
    fclose(fp);
}

void addSpinners(listMap spin) {
}

void freeObjects() {
    for (int i = 0; i < obj.numAtp; i++) free((obj.atp + i)->line);
    free(obj.atp);
    for (int i = 0; i < obj.numAho; i++) free((obj.aho + i)->line);
    free(obj.aho);
    free(obj.oFile);
    free(obj.tFile);
}

void executeBanana(char *target, char *output, listInput all) {
    sp = &all;
    processTarget(target, output);
    // for (int i = 0; i < sp->numAll; i++) {
    //     addSpinners((sp->listA + i)->listM);
    // }

    // const int RNG_SEED = 1337; //   []|< |>[-|>|>`/     thanks https://md5decrypt.net/en/Leet-translator/
    // FastRandom(RNG_SEED);



    freeObjects();
}