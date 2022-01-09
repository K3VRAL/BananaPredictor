#include "main.h"

int main(int argc, char **argv) {
	fprintf(stdout, "+BananaPredictor+\n");

	bool quit = false;
	char command;

	char *target = malloc(sizeof (char));
	char *output = malloc(sizeof (char));

	Node *llINP = NULL;

	char temp[4096];
	while (!quit) {
		fprintf(stdout, "Command: ");
		scanf("%c", &command);
		command = tolower(command);
		if (command != '\n' && getchar() != '\n') { // TODO fix issue where if there are only newlines
			fprintf(stdout, "\tPlease only input a character, not an entire string.\n");
			while (getchar() != '\n');
			continue;
		}

		switch (command) {
			case 't':
				fprintf(stdout, "\tInput the target map: ");
				memset(temp, 0, sizeof (temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				if (access(temp, F_OK) != 0) {
					fprintf(stdout, "\tError: Target file (%s) not found\n", temp);
					break;
				}
				free(target);
				target = strdup(temp);
				break;
			
			case 'o':
				fprintf(stdout, "\tInput the output path: ");
				memset(temp, 0, sizeof (temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				free(output);
				output = strdup(temp);
				break;
			
			case 'a':
				fprintf(stdout, "\tAdding New Spinner\n");
				listAll *asktemp = addAsk();
				if (asktemp == NULL) {
					break;
				}
				Tag tag;
				tag.inp.listM = asktemp->listM;
				tag.inp.listS = asktemp->listS;
				free(asktemp);
				int r;
				bool exists = true;
				while (exists) {
					exists = false;
					r = rand() % USHRT_MAX;
					for (int i = 0; i < ll_length(llINP); i++) {
						if (ll_get(llINP, i)->tag.inp.id == r) {
							exists = true;
							break;
						}
					}
				}
				tag.inp.id = r;
				ll_add(&llINP, inp, tag);
				ll_sort(&llINP);
				break;
				
			case 's': // TODO more dynamic/quadratics/mathematical
				break;
			
			case 'e':
				// fprintf(stdout, "\tInput id of spinner to edit: ");
				// memset(temp, 0, sizeof (temp));
				// fgets(temp, sizeof (temp), stdin);
				// temp[strcspn(temp, "\n")] = '\0';
				// int input = atoi(temp);
				// bool found = false;
				// for (int i = 0; i < ll_length(llINP); i++) {
				// 	if (input == ll_get(llINP, i)->tag.inp.id) {
				// 		listAll *temp = editAsk((input.listA + i));
				// 		if (temp == NULL) {
				// 			break;
				// 		}
				// 		*(input.listA + input.numAll) = *temp;
				// 		free(temp);
				// 		found = true;
				// 		break;
				// 	}
				// }
				// if (!found) {
				// 	fprintf(stdout, "\tInputted id (%d) does not exist\n", input);
				// }
				break;
			
			case 'r':
				break;
			
			case 'x':
				if (target && output && strlen(target) != 0 && strlen(output) != 0) {
					executeBanana(target, output, llINP);
				} else {
					fprintf(stdout, "Target path or Output path is empty, please to them.");
				}
				break;
			
			case 'i':
				fprintf(stdout, "\tTarget Path: %s\n", target == NULL ? target : "(NO INPUT GIVEN)");
				fprintf(stdout, "\tOutput Path: %s\n", output == NULL ? output : "(NO INPUT GIVEN)");
				for (int i = 0; i < ll_length(llINP); i++) {
					fprintf(stdout, "\tnum: %d\tid: %d\n"
								  "\t\tlMap:\tStart-Time=%d\tEnd-Time=%d\tDistance=%d\tOnly-Spin=%s\tInverted=%s\n"
								  "\t\tlSpin:\tStartLeftPos=%d\tEndLeftPos=%d\tStartRightPos=%d\tEndRightPos=%d\n",
								  i, ll_get(llINP, i)->tag.inp.id,
								  ll_get(llINP, i)->tag.inp.listM.startTime, ll_get(llINP, i)->tag.inp.listM.endTime, ll_get(llINP, i)->tag.inp.listM.distance, ll_get(llINP, i)->tag.inp.listM.onlySpin ? "true" : "false", ll_get(llINP, i)->tag.inp.listM.inverted ? "true" : "false",
								  ll_get(llINP, i)->tag.inp.listS.startLPos, ll_get(llINP, i)->tag.inp.listS.endLPos, ll_get(llINP, i)->tag.inp.listS.startRPos, ll_get(llINP, i)->tag.inp.listS.endRPos);
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
			default:
				if (command != '\n') {
					fprintf(stdout, "\tERROR: Command not found.\n");
				}
				break;
		}
	}

	free(target);
	free(output);
	ll_free(llINP);

	return EXIT_SUCCESS;
}