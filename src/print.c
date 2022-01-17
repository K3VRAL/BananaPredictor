#include "print.h"

void addAStartTime() {
    fprintf(stdout, "\tStart Time: ");
}
void addAEndTime() {
    fprintf(stdout, "\tEnd Time: ");
}
void addADistance() {
    fprintf(stdout, "\tDistance: ");
}
void addAOnlySpin() {
    fprintf(stdout, "\tOnly Spinner (y/n): ");
}
void addAInverted() {
    fprintf(stdout, "\tInverted (y/n): ");
}
void addAStartLPos() {
    fprintf(stdout, "\tStart Left Pos: ");
}
void addAEndLPos() {
    fprintf(stdout, "\tEnd Left Pos: ");
}
void addAStartRPos() {
    fprintf(stdout, "\tStart Right Pos: ");
}
void addAEndRPos() {
    fprintf(stdout, "\tEnd Right Pos: ");
}

void addSTime() {
    fprintf(stdout, "\tTime: ");
}
void addSPos() {
    fprintf(stdout, "\tPosition: ");
}

bool checkingErorrs(FunCallback *function, listEnum lid) {
    bool isthis = false;
    switch (lid) {
        case als:
            if (*(function + 0)->set > *(function + 1)->set) {
                fprintf(stdout, "\t*Start Time is greater than End Time.*\n");
                isthis = true;
            }
            if (*(function + 2)->set <= 0) {
                fprintf(stdout, "\t*Distance is smaller or equal to 0.*\n");
                isthis = true;
            }
            if (*(function + 5)->set > *(function + 7)->set) {
                fprintf(stdout, "\t*StartLeftPos is greater than StartRightPos.*\n");
                isthis = true;
            }
            if (*(function + 6)->set > *(function + 8)->set) {
                fprintf(stdout, "\t*EndLeftPos is greater than EndRightPos.*\n");
                isthis = true;
            }
            break;
        case sls:
            break;
    }
    return isthis;
}

void setData(FunCallback **function, int size) {
    bool isthis;
    for (int i = 0; i < size; i++) {
        while (true) {
            char temp[4096]; // TODO fix this issue
            isthis = false;
            fprintf(stdout, "\t");
            (*function + i)->function();
            fgets(temp, sizeof (temp), stdin);
            temp[strcspn(temp, "\n")] = '\0';
            if (strlen(temp) == 0) {
                continue;
            }
            if (!(*function + i)->isbool) {
                for (int j = 0; j < strlen(temp); j++) {
                    if (!isdigit(temp[j])) {
                        isthis = true;
                        break;
                    }
                }
                if (isthis) {
                    continue;
                }
                int numtemp = atoi(temp);
                if ((*function + i)->maplimits && numtemp >= 0 && numtemp <= 512) {
                    *(*function + i)->set = numtemp;
                    break;
                } else if (numtemp != -1) {
                    *(*function + i)->set = numtemp;
                    break;
                }
            } else {
                for (int j = 0; j < strlen(temp); j++) {
                    if (isdigit(temp[j])) {
                        isthis = true;
                        break;
                    }
                }
                if (isthis) continue;
                for (int j = 0; j < strlen(temp); j++) temp[j] = tolower(temp[j]);
                if (strcmp(temp, "yes") == 0 || temp[0] == 'y'
                 || strcmp(temp, "no") == 0 || temp[0] == 'n') {
                    *(*function + i)->bset = temp[0] == 'y' ? true : false;
                    break;
                }
            }
        }
    }
}

listSpin *addAsk(listEnum lid) {
    int size;
    FunCallback *function = NULL;

    listSpin *ls = malloc(sizeof (listSpin));
    switch (lid) {
        case als:
            size = 9;
            function = malloc(size * sizeof (FunCallback));
            (function + 0)->function = addAStartTime;
            (function + 0)->set = (int *)&ls->als.startTime;
            (function + 0)->isbool = false;
            (function + 0)->maplimits = false;
            (function + 1)->function = addAEndTime;
            (function + 1)->set = (int *)&ls->als.endTime;
            (function + 1)->isbool = false;
            (function + 1)->maplimits = false;
            (function + 2)->function = addADistance;
            (function + 2)->set = &ls->als.distance;
            (function + 2)->isbool = false;
            (function + 2)->maplimits = false;
            (function + 3)->function = addAOnlySpin;
            (function + 3)->bset = &ls->als.onlySpin;
            (function + 3)->isbool = true;
            (function + 4)->function = addAInverted;
            (function + 4)->bset = &ls->als.inverted;
            (function + 4)->isbool = true;

            (function + 5)->function = addAStartLPos;
            (function + 5)->set = &ls->als.startLPos;
            (function + 5)->isbool = false;
            (function + 5)->maplimits = true;
            (function + 6)->function = addAEndLPos;
            (function + 6)->set = &ls->als.endLPos;
            (function + 6)->isbool = false;
            (function + 6)->maplimits = true;
            (function + 7)->function = addAStartRPos;
            (function + 7)->set = &ls->als.startRPos;
            (function + 7)->isbool = false;
            (function + 7)->maplimits = true;
            (function + 8)->function = addAEndRPos;
            (function + 8)->set = &ls->als.endRPos;
            (function + 8)->isbool = false;
            (function + 8)->maplimits = true;
            break;
        case sls:
            size = 2;
            function = malloc(size * sizeof (FunCallback));
            (function + 0)->function = addSTime;
            (function + 0)->set = (int *)&ls->sls.time;
            (function + 0)->isbool = false;
            (function + 0)->maplimits = false;
            (function + 1)->function = addSPos;
            (function + 1)->set = &ls->sls.pos;
            (function + 1)->isbool = false;
            (function + 1)->maplimits = true;
            break;
    }
    
    setData(&function, size);

    if (checkingErorrs(function, lid)) {
        fprintf(stdout, "\tNothing has changed.\n");
        free(function);
        return NULL;
    }

    free(function);

    return ls;
}