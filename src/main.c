#include "main.h"

int main(int argc, char **argv) {
	fprintf(stdout, "+BananaPredictor+\n");

	bool quit = false;
	char command;

	char *target = malloc(sizeof (char));
	char *output = malloc(sizeof (char));
	listInput input = { NULL, 0 };
	input.listA = malloc((input.numAll + 1) * sizeof (listAll));

	char getting;
	char temp[4096];
	while (!quit) {
		fprintf(stdout, "Command: ");
		scanf("%c", &command);
		command = tolower(command);
		if ((getting = getchar()) != '\n') {
			fprintf(stdout, "Please only input a character, not an entire string.\n");
			while ((getting = getchar()) != '\n');
			continue;
		}

		switch (command) {
			case 't':
				fprintf(stdout, "\tInput the target map: ");
				memset(temp, 0, strlen(temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				if (access(temp, F_OK) != 0) {
					fprintf(stdout, "\tError: Target file (%s) not found\n", temp);
					break;
				}
				target = strdup(temp);
				break;
			
			case 'o':
				fprintf(stdout, "\tInput the output path: ");
				memset(temp, 0, strlen(temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				output = strdup(temp);
				break;
			
			case 'a':
				input.listA = realloc(input.listA, (input.numAll + 1) * sizeof (listAll));
				listAll *asktemp = addAsk();
				if (asktemp == NULL) {
					break;
				}
				*(input.listA + input.numAll) = *asktemp;
				free(asktemp);
				size_t r;
				bool exists = true;
				while (exists) {
					exists = false;
					r = rand() % USHRT_MAX;
					for (int i = 0; i < input.numAll; i++) {
						if ((input.listA + i)->id == r) {
							exists = true;
							break;
						}
					}
				}
				(input.listA + input.numAll++)->id = r;
				break;
				
			case 's': // TODO more dynamic/quadratics/mathematical
				break;
			
			case 'e':
				fprintf(stdout, "\tInput id of spinner to edit: ");
				memset(temp, 0, strlen(temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				int inp = atoi(temp);
				bool found = false;
				for (int i = 0; i < input.numAll; i++) {
					if (inp == (input.listA + i)->id) {
						listAll *temp = editAsk((input.listA + i));
						if (temp == NULL) {
							break;
						}
						*(input.listA + input.numAll) = *temp;
						free(temp);
						found = true;
						break;
					}
				}
				if (!found) {
					fprintf(stdout, "\tInputted id (%d) does not exist\n", inp);
				}
				break;
			
			case 'r':
				break;
			
			case 'x':
				if (target && output && strlen(target) != 0 && strlen(output) != 0) {
					executeBanana(target, output, input);
				} else {
					fprintf(stdout, "Target path or Output path is empty, please to them.");
				}
				break;
			
			case 'i':
				fprintf(stdout, "\tTarget Path: %s\n", target);
				fprintf(stdout, "\tOutput Path: %s\n", output);
				for (int i = 0; i < input.numAll; i++) {
					fprintf(stdout, "\tnum: %d\tid: %zu\n", i, (input.listA + i)->id);
					fprintf(stdout, "\t\tlMap:\tStart-Time=%zu\tEnd-Time=%zu\tDistance=%zu\tOnly-Spin=%s\tInverted=%s\n", (input.listA + i)->listM.startTime, (input.listA + i)->listM.endTime, (input.listA + i)->listM.distance, (input.listA + i)->listM.onlySpin ? "true" : "false", (input.listA + i)->listM.inverted ? "true" : "false");
					fprintf(stdout, "\t\tlSpin:\tStartLeftPos=%zu\tEndLeftPos=%zu\tStartRightPos=%zu\tEndRightPos=%zu\n", (input.listA + i)->listS.startLPos, (input.listA + i)->listS.endLPos, (input.listA + i)->listS.startRPos, (input.listA + i)->listS.endRPos);
				}
				break;
			
			case 'h':
				fprintf(stdout,
					"t\t\tTargets the file to process data and output\n"
					"o\t\tAfter execution, outputs all processed data to file\n"
					"a\t\tAdds new spinner\n"
					"e\t\tEdits specific spinner\n"
					"r\t\tRemoves specific spinner\n"
					"x\t\tExecutes data based on the input\n"
					"i\t\tOutputs all information including all added spinner data\n"
					"h\t\tShows help message\n"
					"q\t\tQuits application\n"
				);
				break;

			case 'q':
				fprintf(stdout, "\tQuitting...\n");
				quit = true;
				break;
		}
	}

	free(target);
	free(output);
	free(input.listA);

	return EXIT_SUCCESS;
}