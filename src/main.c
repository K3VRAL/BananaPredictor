#include "main.h"

int main(int argc, char **argv) {
	fprintf(stdout, "+BananaPredictor+\n");

	bool quit = false;
	char command;

	char *target = NULL;
	char *output = NULL;
	Node *llINP = NULL;

	char temp[4096];
	listSpin *asktemp;
	Tag tagtemp;
	srand(time(NULL));
	unsigned int tempid;
	bool tempexists;
	Node *lltemp;
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
				fprintf(stdout, "\tTarget File");
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
				fprintf(stdout, "\tOutput File");
				fprintf(stdout, "\tInput the output path: ");
				memset(temp, 0, sizeof (temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				free(output);
				output = strdup(temp);
				break;
			
			case 'a':
				fprintf(stdout, "\tAdding New LR Spinner\n");
				asktemp = addAsk(als);
				if (asktemp == NULL) {
					break;
				}
				tagtemp.inp.ls = *asktemp;
				tagtemp.inp.lid = als;
				free(asktemp);
				tempid = 0;
				tempexists = true;
				while (tempexists) {
					tempexists = false;
					tempid = rand() % USHRT_MAX;
					if (ll_get_byid(llINP, tempid) != NULL) {
						tempexists = true;
					}
				}
				ll_add(&llINP, inp, tagtemp, tempid);
				ll_sort(&llINP);
				tagtemp = (const Tag){ 0 };
				fprintf(stdout, "\tSuccessfully added LR spinner - id: %d\n", tempid);
				break;
				
			case 's':
				fprintf(stdout, "\tAdding New Single Spinner\n");
				asktemp = addAsk(sls);
				if (asktemp == NULL) {
					break;
				}
				tagtemp.inp.ls = *asktemp;
				tagtemp.inp.lid = sls;
				free(asktemp);
				tempid = 0;
				tempexists = true;
				while (tempexists) {
					tempexists = false;
					tempid = rand() % USHRT_MAX;
					if (ll_get_byid(llINP, tempid) != NULL) {
						tempexists = true;
					}
				}
				ll_add(&llINP, inp, tagtemp, tempid);
				ll_sort(&llINP);
				tagtemp = (const Tag){ 0 };
				fprintf(stdout, "\tSuccessfully added single spinner - id: %d\n", tempid);
				break;

			case 'd': // TODO more dynamic
				break;
			
			case 'e':
				fprintf(stdout, "\tEdit Specific Spinner\n");
				fprintf(stdout, "\tEdit spinner by id: ");
				memset(temp, 0, sizeof (temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				tempexists = false;
				for (int i = 0; i < strlen(temp); i++) {
                    if (!isdigit(temp[i])) {
                        tempexists = true;
                        break;
                    }
				}
				if (tempexists) {
					fprintf(stdout, "\t\tERROR: non digit found\n");
					break;
				}
				lltemp = ll_get_byid(llINP, atoi(temp));
				// TODO
				break;
			
			case 'r':
				fprintf(stdout, "\tRemove Specific Spinner\n");
				fprintf(stdout, "\t\tRemove spinner by id: ");
				memset(temp, 0, sizeof (temp));
				fgets(temp, sizeof (temp), stdin);
				temp[strcspn(temp, "\n")] = '\0';
				tempexists = false;
				for (int i = 0; i < strlen(temp); i++) {
                    if (!isdigit(temp[i])) {
                        tempexists = true;
                        break;
                    }
				}
				if (tempexists) {
					fprintf(stdout, "\t\tERROR: non digit found\n");
					break;
				}
				ll_remove_byid(&llINP, atoi(temp));
				fprintf(stdout, "\t\tSuccessfully removed spinner\n");
				break;
			
			case 'x':
				if (target && output && strlen(target) != 0 && strlen(output) != 0) {
					executeBanana(target, output, llINP);
				} else {
					fprintf(stdout, "\tTarget path or Output path is empty, please to them.\n");
				}
				break;
			
			case 'i':
				fprintf(stdout, "\tTarget Path: %s\n", target != NULL ? target : "(NO INPUT GIVEN)");
				fprintf(stdout, "\tOutput Path: %s\n", output != NULL ? output : "(NO INPUT GIVEN)");
				for (int i = 0; i < ll_length(llINP); i++) {
					lltemp = ll_get(llINP, i);
					listSpinners lstemp = lltemp->tag.inp;
					fprintf(stdout, "\tnum: %d\tid: %u\n", i, lltemp->id);
					switch (lstemp.lid) {
						case als:
							fprintf(stdout, "\t\tStart-Time=%d\tEnd-Time=%d\tDistance=%d\tOnly-Spin=%s\tInverted=%s\n"
											"\t\tStartLeftPos=%d\tEndLeftPos=%d\tStartRightPos=%d\tEndRightPos=%d\n",
											lstemp.ls.als.startTime, lstemp.ls.als.endTime, lstemp.ls.als.distance, lstemp.ls.als.onlySpin ? "true" : "false", lstemp.ls.als.inverted ? "true" : "false",
											lstemp.ls.als.startLPos, lstemp.ls.als.endLPos, lstemp.ls.als.startRPos, lstemp.ls.als.endRPos);
							break;
						case sls:
							fprintf(stdout, "\t\tTime=%d\tPosition=%d\n",
											lstemp.ls.sls.time, lstemp.ls.sls.pos);
							break;
					}
					fprintf(stdout, "\n");
				}
				break;
			
			case 'h':
				fprintf(stdout,
					"\tModification Of Spinner\n"
					"\t\ta\tAdds new LR spinner\n"
					"\t\ts\tAdds new specific spinner\n"
					"\t\te\tEdits specific spinner\n"
					"\t\tr\tRemoves specific spinner\n"
					
					"\tInput Of Data\n"
					"\t\tt\tTargets the file to process data and output\n"
					"\t\to\tOutputs new file absed on processed data\n"
					"\t\tx\tExecutes data based on the input\n"

					"\tMiscellaneous\n"
					"\t\ti\tOutputs all inputted information\n"
					"\t\th\tShows help message\n"
					"\t\tq\tQuits application\n"
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