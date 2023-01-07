/*
 * File: autocomplete.c
 * Author: Frederic W Laos
 * Description: This file takes in incomplete and offers words potential completions. 
 */

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>

// This class is used to store all known words.
typedef struct wordTree {
	int set;
	struct wordTree *letters; 
} wordTree;

int addToTree(wordTree *tree, char *word) {
	// Check if word is null
	if (word == NULL || word[0] == '\n') {
		return 0;
	}
	// Checks if their if first letter in word is already in letters
	if (tree->set == 0) {
		tree->set = 1;
		tree->letters = calloc(26, sizeof(wordTree));
	}
	
	// Creates substring of word without the first letter
	char* substr = malloc(strlen(word));
	strncpy(substr, word+1, strlen(word));
	
	printf("%c", word[0]);
	
	addToTree( &(tree->letters[ (tolower(word[0]) - 'a') ]), substr);
	
	free(substr);
	
	printf("\n");

	return 0;
}

int freeMemory(wordTree *tree) {
	if (tree->set == 1) {
		for (int i = 0; i < 26; i++) {
			freeMemory(&(tree->letters[i]));	
		}
		free(tree->letters);
	}
	return 0;
}

int main(int argc, char **argv) {
	
	// Checks the correct number of arguments were given in the command line. 
	if (argc == 3) {
		
		// First program adds all words in in the given file to a wordTree 
		FILE *wordBank = fopen(argv[1], "r");
		char line[256];
		
		// Checks that the file given is valid
		if (wordBank == NULL) {
			fprintf(stderr, "ERROR: %s does not exist.\n", argv[1]);
			return 1;
		}
		
		// Create wordTree	
		wordTree *tree = calloc(1, sizeof(wordTree));
		tree->set = 1;
		tree->letters = calloc(26, sizeof(wordTree));
		
			
		// Goes line by line through given file adding words to wordBank
		while (fgets(line, sizeof(line), wordBank)) {
			addToTree(tree, line);
		}
		
		fclose(wordBank);
		
		freeMemory(tree);
	        free(tree);		

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
