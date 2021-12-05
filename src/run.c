#include "run.h"
#include "lib/files.h"
#include <stddef.h>
#include <stdio.h>
#include <string.h>

objects obj = { NULL, NULL, NULL, 0 };

void processTarget(char *target, char *output) {
    FILE *fp = fopen(target, "r");
    if (!fp) { perror(target); exit(EXIT_FAILURE); }
    
    obj.tFile = xrealloc(NULL, strlen(target) * sizeof (char) + 1);
    strcpy(obj.tFile, target);
    obj.oFile = xrealloc(NULL, strlen(output) * sizeof (char) + 1);
    strcpy(obj.oFile, output);

    size_t curLine = 0;

    obj.aho = xrealloc(NULL, sizeof (allHO));

    char line[256];

    bool afterHO = false;
    while (fgets(line, sizeof (line), fp) != NULL) {
        int i = 0;
        while (i < strlen(line)) {
            if (line[i] == '\t' || line[i] == '\n' || line[i] == '\r') for (int j = i; j < strlen(line); j++) line[j] = line[j + 1];
            else i++;
        }
        if (!afterHO && strcmp(line, "[HitObjects]") == 0) {
            afterHO = true;
            continue;
        }
        if (afterHO) {
            if (line[0] == '\n') break;
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
                    
                    size_t x = atoi(*contents);
                    size_t y = atoi(*(contents + 1));
                    size_t time = atoi(*(contents + 2));
                    size_t ncombo = atoi(*(contents + 3));
                    size_t hsound = atoi(*(contents + 4));

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
        curLine++;
    }
    fclose(fp);
}

void executeBanana(char *target, char *output, listAll *all) {
    processTarget(target, output);
    const int RNG_SEED = 1337; //   []|< |>[-|>|>`/     thanks https://md5decrypt.net/en/Leet-translator/
    FastRandom(RNG_SEED);
    for (int i = 0; i < obj.numAho; i++) {
        fprintf(stdout, "%d - %s - %s\n", i, etsType((obj.aho + i)->type), (obj.aho + i)->line);
    }
    freeRun();
}

void freeRun() {
    for (int i = 0; i < obj.numAho; i++) {
        free((obj.aho + obj.numAho)->line);
    }
    free(obj.aho);
    free(obj.oFile);
    free(obj.tFile);
}