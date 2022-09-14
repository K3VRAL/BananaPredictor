#ifndef ARGS_H
#define ARGS_H

#include <stdbool.h>
#include <stdlib.h>
#include <string.h>

#include "predictor.h"

/*
    Handling the arguments given from the input of the terminal
    
    return
        void
    args
        int
        char **
*/
void args_handle(int, char **);

#endif