#include "print.h"

void addStartTime() {
    fprintf(stdout, "Start Time: ");
}
void addEndTime() {
    fprintf(stdout, "End Time: ");
}
void addDistance() {
    fprintf(stdout, "Distance: ");
}
void addOnlySpin() {
    fprintf(stdout, "Only Spinner (y/n): ");
}
void addInverted() {
    fprintf(stdout, "Inverted (y/n): ");
}

void addStartLPos() {
    fprintf(stdout, "Start Left Pos: ");
}
void addEndLPos() {
    fprintf(stdout, "End Left Pos: ");
}
void addStartRPos() {
    fprintf(stdout, "Start Right Pos: ");
}
void addEndRPos() {
    fprintf(stdout, "End Right Pos: ");
}

bool checkingErorrs(FunCallback *function) {
    bool isthis = false;
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
                if (!(*function + i)->ismap && numtemp >= 0 && numtemp <= 512) {
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

listAll *addAsk() {
    listMap lMap = { 0, 0, 0, 0, 0 };
    listSpin lSpin = { 0, 0, 0, 0 };

    int size = 9;
    FunCallback *function = malloc(size * sizeof (FunCallback));
    
    (function + 0)->function = addStartTime;
    (function + 0)->set = (int *)&lMap.startTime;
    (function + 0)->isbool = false;
    (function + 0)->ismap = true;
    (function + 1)->function = addEndTime;
    (function + 1)->set = (int *)&lMap.endTime;
    (function + 1)->isbool = false;
    (function + 1)->ismap = true;
    (function + 2)->function = addDistance;
    (function + 2)->set = &lMap.distance;
    (function + 2)->isbool = false;
    (function + 2)->ismap = true;
    (function + 3)->function = addOnlySpin;
    (function + 3)->bset = &lMap.onlySpin;
    (function + 3)->isbool = true;
    (function + 4)->function = addInverted;
    (function + 4)->bset = &lMap.inverted;
    (function + 4)->isbool = true;

    (function + 5)->function = addStartLPos;
    (function + 5)->set = &lSpin.startLPos;
    (function + 5)->isbool = false;
    (function + 5)->ismap = false;
    (function + 6)->function = addEndLPos;
    (function + 6)->set = &lSpin.endLPos;
    (function + 6)->isbool = false;
    (function + 6)->ismap = false;
    (function + 7)->function = addStartRPos;
    (function + 7)->set = &lSpin.startRPos;
    (function + 7)->isbool = false;
    (function + 7)->ismap = false;
    (function + 8)->function = addEndRPos;
    (function + 8)->set = &lSpin.endRPos;
    (function + 8)->isbool = false;
    (function + 8)->ismap = false;

    setData(&function, size);

    if (checkingErorrs(function)) {
        fprintf(stdout, "\tNothing has changed.\n");
        free(function);
        return NULL;
    }

    free(function);

    listAll *lAll = malloc(sizeof (listAll));
    lAll->id = 0;
    lAll->listM = lMap;
    lAll->listS = lSpin;
    return lAll;
}