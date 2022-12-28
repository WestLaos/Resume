/*
 * File: autocomplete.c
 * Author: Frederic W Laos
 * Description: This file takes in incomplete or incorrectly spelled words and
 * 		offers words that they could be meant to be. 
 */
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// This class is used to store all known words.
typedef struct wordTree {
	struct wordTree *letters; 
} wordTree;



int main(int argc, char **argv) {
	
	// Checks the correct number of arguments were given in the command line. 
	if (argc == 3) {
		
		// First program adds all words in in the given file to a wordTree 
		
		FILE *wordBank = fopen(argv[1], "r");
		char line[256];

		if (wordBank == NULL) {
			fprintf(stderr, "ERROR: %s does not exist.\n", argv[1]);
			return 1;
		}

		while (fgets(line, sizeof(line), wordBank)) {
			printf("%s", line);
		}

		fclose(wordBank);

	// Checks if no command line argument were given if so prints error
	} else if (argc < 3) {
		fprintf(stderr, "ERROR: %d argument(s) given, this function requires two.\n", (argc - 1));
		return 1;
	// Checks if to many command line arguments were given if so prints error
	} else {
		fprintf(stderr, "ERROR: To many arguments given, this function can only handle two.\n");
		return 1;
	}
}
