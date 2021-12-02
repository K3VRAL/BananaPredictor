#include "print.h"

void addStartTime() {
    printf("Start Time: ");
}
void addEndTime() {
    printf("End Time: ");
}
void addDistance() {
    printf("Distance: ");
}
void addOnlySpin() {
    printf("Only Spinner (y/n): ");
}
void addInverted() {
    printf("Inverted (y/n): ");
}

void addStartLPos() {
    printf("Start Left Pos: ");
}
void addEndLPos() {
    printf("End Left Pos: ");
}
void addStartRPos() {
    printf("Start Right Pos: ");
}
void addEndRPos() {
    printf("End Right Pos: ");
}

listAll addAsk() {
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

    bool isthis;
    char temp[256];
    size_t numtemp;
    for (int i = 0; i < size; i++) {
        while (true) {
            isthis = false;
            memset(temp, 0, strlen(temp));
            printf("\t");
            (function + i)->function();
            fgets(temp, 256, stdin);
            temp[strcspn(temp, "\n")] = '\0';
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

    isthis = false;
    if (*(function + 0)->set > *(function + 1)->set) {
        printf("\tStart Time is greater than End Time.\n");
        isthis = true;
    }
    if (*(function + 2)->set <= 0) {
        printf("\tDistance is smaller or equal to 0.\n");
        isthis = true;
    }
    if (*(function + 5)->set > *(function + 7)->set) {
        printf("\tStartLeftPos is greater than StartRightPos.\n");
        isthis = true;
    }
    if (*(function + 6)->set > *(function + 8)->set) {
        printf("\tEndLeftPos is greater than EndRightPos.\n");
        isthis = true;
    }

    listAll lAll;

    if (isthis) {
        for (size_t i = 0; i < size; i++) {
            (function + i)->set = 0;
        }
        printf("\tNothing has changed.\n");

        return lAll;
    }

    free(function);

    lAll.listM = lMap;
    lAll.listS = lSpin;
    return lAll;
}