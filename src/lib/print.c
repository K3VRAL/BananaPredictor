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
        fprintf(stdout, "\tStart Time is greater than End Time.\n");
        isthis = true;
    }
    if (*(function + 2)->set <= 0) {
        fprintf(stdout, "\tDistance is smaller or equal to 0.\n");
        isthis = true;
    }
    if (*(function + 5)->set > *(function + 7)->set) {
        fprintf(stdout, "\tStartLeftPos is greater than StartRightPos.\n");
        isthis = true;
    }
    if (*(function + 6)->set > *(function + 8)->set) {
        fprintf(stdout, "\tEndLeftPos is greater than EndRightPos.\n");
        isthis = true;
    }
    return isthis;
}

FunCallback *setData(FunCallback *function, size_t size) {
    bool isthis;
    size_t numtemp;
    for (int i = 0; i < size; i++) {
        while (true) {
            char temp[256];
            isthis = false;
            fprintf(stdout, "\t");
            (function + i)->function();
            fgets(temp, 256, stdin);
            temp[strcspn(temp, "\n")] = '\0';
            if (strlen(temp) == 0) {
                continue;
            }
            if (!(function + i)->isbool) {
                for (size_t j = 0; j < strlen(temp); j++) {
                    if (!isdigit(temp[j])) {
                        isthis = true;
                        break;
                    }
                }
                if (isthis) continue;
                numtemp = atoi(temp);
                if (numtemp >= 0 && numtemp <= 512) {
                    *(function + i)->set = numtemp;
                    break;
                }
            } else {
                for (size_t j = 0; j < strlen(temp); j++) {
                    if (isdigit(temp[j])) {
                        isthis = true;
                        break;
                    }
                }
                if (isthis) continue;
                for (size_t j = 0; j < strlen(temp); j++) temp[j] = tolower(temp[j]);
                if (strcmp(temp, "yes") == 0 || temp[0] == 'y'
                 || strcmp(temp, "no") == 0 || temp[0] == 'n') {
                    *(function + i)->bset = temp[0] == 'y' ? true : false;
                    break;
                }
            }
        }
    }

    return function;
}

listAll *addAsk() {
    listMap lMap = { 0, 0, 0, 0, 0 };
    listSpin lSpin = { 0, 0, 0, 0 };

    int size = 9;
    FunCallback *function = xrealloc(NULL, size * sizeof (FunCallback));
    
    (function + 0)->function = addStartTime;
    (function + 0)->set = &lMap.startTime;
    (function + 0)->isbool = false;
    (function + 1)->function = addEndTime;
    (function + 1)->set = &lMap.endTime;
    (function + 1)->isbool = false;
    (function + 2)->function = addDistance;
    (function + 2)->set = &lMap.distance;
    (function + 2)->isbool = false;
    (function + 3)->function = addOnlySpin;
    (function + 3)->bset = &lMap.onlySpin;
    (function + 3)->isbool = true;
    (function + 4)->function = addInverted;
    (function + 4)->bset = &lMap.inverted;
    (function + 4)->isbool = true;

    (function + 5)->function = addStartLPos;
    (function + 5)->set = &lSpin.startLPos;
    (function + 5)->isbool = false;
    (function + 6)->function = addEndLPos;
    (function + 6)->set = &lSpin.endLPos;
    (function + 6)->isbool = false;
    (function + 7)->function = addStartRPos;
    (function + 7)->set = &lSpin.startRPos;
    (function + 7)->isbool = false;
    (function + 8)->function = addEndRPos;
    (function + 8)->set = &lSpin.endRPos;
    (function + 8)->isbool = false;

    function = setData(function, size);

    if (checkingErorrs(function)) {
        fprintf(stdout, "\tNothing has changed.\n");
        free(function);
        return NULL;
    }

    free(function);

    listAll *lAll = xrealloc(NULL, sizeof (listAll));
    lAll->id = 0;
    lAll->listM = lMap;
    lAll->listS = lSpin;
    return lAll;
}

listAll *editAsk(listAll *all) {
    int size = 9;
    FunCallback *function = xrealloc(NULL, size * sizeof (FunCallback));

    (function + 0)->function = addStartTime;
    (function + 0)->set = &all->listM.startTime;
    (function + 0)->isbool = false;
    (function + 1)->function = addEndTime;
    (function + 1)->set = &all->listM.endTime;
    (function + 1)->isbool = false;
    (function + 2)->function = addDistance;
    (function + 2)->set = &all->listM.distance;
    (function + 2)->isbool = false;
    (function + 3)->function = addOnlySpin;
    (function + 3)->bset = &all->listM.onlySpin;
    (function + 3)->isbool = true;
    (function + 4)->function = addInverted;
    (function + 4)->bset = &all->listM.inverted;
    (function + 4)->isbool = true;

    (function + 5)->function = addStartLPos;
    (function + 5)->set = &all->listS.startLPos;
    (function + 5)->isbool = false;
    (function + 6)->function = addEndLPos;
    (function + 6)->set = &all->listS.endLPos;
    (function + 6)->isbool = false;
    (function + 7)->function = addStartRPos;
    (function + 7)->set = &all->listS.startRPos;
    (function + 7)->isbool = false;
    (function + 8)->function = addEndRPos;
    (function + 8)->set = &all->listS.endRPos;
    (function + 8)->isbool = false;

    function = setData(function, size);

    if (checkingErorrs(function)) {
        fprintf(stdout, "\tNothing has changed.\n");
        free(function);
        return NULL;
    }
    free(function);

    return all;
}