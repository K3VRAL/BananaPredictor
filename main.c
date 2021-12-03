#include "main.h"

int main(int argc, char **argv) {
	printf("+BananaPredictor+\n");

	bool quit = false;
	char command;

	char *target = xrealloc(NULL, sizeof (char));
	char *output = xrealloc(NULL, sizeof (char));
	listAll *all = xrealloc(NULL, sizeof (listAll));
	size_t numAll = 0;

	while (!quit) {
		printf("Command: ");
		scanf("%c", &command);
		command = tolower(command);
		char getting;
		if ((getting = getchar()) != '\n') {
			printf("Please only input a character, not an entire string.\n");
			while ((getting = getchar()) != '\n');
			continue;
		}

		switch (command) {
			case 't':
				printf("\tInput the target map: ");
				char ttemp[256];
				fgets(ttemp, 256, stdin);
				ttemp[strcspn(ttemp, "\n")] = '\0';
				target = xrealloc(target, strlen(ttemp) * sizeof (char) + 1);
				strcpy(target, ttemp);
				break;
			
			case 'o':
				printf("\tInput the output path: ");
				char otemp[256];
				fgets(otemp, 256, stdin);
				otemp[strcspn(otemp, "\n")] = '\0';
				output = xrealloc(output, strlen(otemp) * sizeof (char) + 1);
				strcpy(output, otemp);
				break;
			
			case 'a':
				all = xrealloc(all, (numAll + 1) * sizeof (listAll));
				listAll *temp = addAsk();
				if (temp == NULL) {
					break;
				}
				*(all + numAll) = *temp;
				free(temp);
				size_t r;
				bool exists = true;
				while (exists) {
					exists = false;
					r = rand() % USHRT_MAX;
					for (int i = 0; i < numAll; i++) {
						if ((all + i)->id == r) {
							exists = true;
							break;
						}
					}
				}
				(all + numAll++)->id = r;
				break;
			
			case 'e':
				printf("\tInput id of spinner to edit: ");
				char etemp[256];
				fgets(etemp, 256, stdin);
				etemp[strcspn(etemp, "\n")] = '\0';
				size_t input = atoi(etemp);
				bool found = false;
				for (int i = 0; i < numAll; i++) {
					if (input == (all + i)->id) {
						listAll *temp = editAsk((all + i));
						if (temp == NULL) {
							break;
						}
						*(all + numAll) = *temp;
						free(temp);
						found = true;
						break;
					}
				}
				if (!found) {
					printf("\tInputted id (%zu) does not exist\n", input);
				}
				break;
			
			case 'r':
				break;
			
			case 'x':
				if (strlen(target) != 0 && strlen(output) != 0) executeBanana(all);
				else printf("Target path or Output path is empty, please to them.");
				break;
			
			case 'i':
				printf("\tTarget Path: %s\n", target);
				printf("\tOutput Path: %s\n", output);
				for (size_t i = 0; i < numAll; i++) {
					printf("\tnum: %zu\tid: %zu\n", i, (all + i)->id);
					printf("\t\tlMap:\tStart-Time=%zu\tEnd-Time=%zu\tDistance=%zu\tOnly-Spin=%s\tInverted=%s\n", (all + i)->listM.startTime, (all + i)->listM.endTime, (all + i)->listM.distance, (all + i)->listM.onlySpin ? "true" : "false", (all + i)->listM.inverted ? "true" : "false");
					printf("\t\tlSpin:\tStartLeftPos=%zu\tEndLeftPos=%zu\tStartRightPos=%zu\tEndRightPos=%zu\n", (all + i)->listS.startLPos, (all + i)->listS.endLPos, (all + i)->listS.startRPos, (all + i)->listS.endRPos);
				}
				break;
			
			case 'h':
				printf(
					"t\t\tTargets the file to process data and output\n"
					"o\t\tAfter execution, outputs all processed data to file\n"
					"a\t\tAdds in new spinner\n"
					"e\t\tEdits specific spinner\n"
					"r\t\tRemoves specific spinner\n"
					"x\t\tExecutes data based on the input\n"
					"i\t\tOutputs all information including all added spinner data\n"
					"h\t\tShows help message\n"
					"q\t\tQuits application\n"
				);
				break;

			case 'q':
				printf("\tQuitting...\n");
				quit = true;
				break;
		}
	}

	free(target);
	free(output);
	free(all);

	return EXIT_SUCCESS;
}
