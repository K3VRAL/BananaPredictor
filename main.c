#include "main.h"
#include "src/lib/print.h"

int main(int argc, char **argv) {
	printf("+BananaPredictor+\n");

	bool quit = false;
	char command;

	char temp[256];

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
				printf("\tInput the target map - ");
				memset(temp, 0, strlen(temp));
				fgets(temp, 256, stdin);
				*(temp + strcspn(temp, "\n")) = '\0';
				targetPath(temp);
				break;
			
			case 'o':
				printf("\tInput the output path - ");
				memset(temp, 0, strlen(temp));
				fgets(temp, 256, stdin);
				*(temp + strcspn(temp, "\n")) = '\0';
				outputPath(temp);
				break;
			
			case 'a':
 				// addAsk();
				break;
			
			case 'e':
				break;
			
			case 'r':
				break;
			
			case 'x':
				break;
			
			case 'i':
				printf("\tTarget Path: %s\n", target);
				printf("\tOutput Path: %s\n", output);
				printf("\tlMap:\tStart Time=%zu\tEnd Time=%zu\tDistance=%zu\tOnly Spin=%s\tInverted=%s\n", lMap.startTime, lMap.endTime, lMap.distance, lMap.onlySpin ? "true" : "false", lMap.inverted ? "true" : "false");
				printf("\tlSpin:\tStartLeftPos=%zu\tEndLeftPos=%zu\tStartRightPos=%zu\tEndRightPos=%zu\n", lSpin.startLPos, lSpin.endLPos, lSpin.startRPos, lSpin.endRPos);
				break;
			
			case 'h':
				break;

			case 'q':
				printf("\tQuitting...\n");
				quit = true;
				break;
		}
	}

	return EXIT_SUCCESS;
}
