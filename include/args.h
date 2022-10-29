#ifndef ARGS_H
#define ARGS_H

#include <stdbool.h>
#include <stdio.h>
#include <string.h>

#include "predictor.h"

/*
	Handling the arguments given from the input of the terminal
	
	return
		void
	argsreturn
		bool *
	args
		int
		char **
*/
bool args_main(int, char **);

#endif