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
	if (word == NULL || word[0] == 0x0) {
		return 0;
	}
	// Checks if their if first letter in word is already in letters
	if (tree->set == 0) {
		tree->set = 1;
		tree->letters = calloc(27, sizeof(wordTree));
	}
	// Marks 27 spot to indicate this location is the end of a word
	if (word[0] == '\n') {
		tree->letters[27].set = 1;
		addToTree( &(tree->letters[27]), NULL );
	}
	
	// Creates substring of word without the first letter
	char* substr = malloc(strlen(word));
	strncpy(substr, word+1, strlen(word));
	
	// Used in testing printf("%c", word[0]);
	
	addToTree( &(tree->letters[ (tolower(word[0]) - 'a') ]), substr);
	
	free(substr);

	return 0;
}

int freeMemory(wordTree *tree) {
	// Checks if tree has had memory allocated to its array
	if (tree->set == 1) {
		// goes through each tree in the array and calls this function on them
		for (int i = 0; i < 27; i++) {
			freeMemory(&(tree->letters[i]));	
		}
		free(tree->letters);
	}
	return 0;
}

wordTree * containsWord(wordTree *tree, char *word) {
	// Loops through each letter in word checking if subsequent layers of the tree contain them
	for (int i = 0; i < strlen(word); i++) {
		// Checks if this tree contains all letters in its array
		if (tree->set == 0) {
			return NULL;
		} 
		// Checks if this tree contains the current letter in word
		if (tree->letters[word[i] - 'a'].set == 1) {
			tree = &(tree->letters[word[i] - 'a']);
		} else {
			return NULL;
		}
	}
	return tree;
}

int printWords(wordTree *tree, char *wordStart, char *wordEnd) {
	// Print word once end is hit on branch
	if (tree->letters[27].set == 1) {
		printf("%s%s\n", wordStart, wordEnd);
	}

	// Loops through all possible letters seeing if any ar conainted in tree	
	for (int i = 0; i < 26; i++) {
		if (tree->letters[i].set == 1) {
			
			// Current letter
			char temp = 'a' + i;
			// Put x to the next spot in wordEnd and adds new letter
			int x;
			for (x = 0; isalpha(wordEnd[x]); x++) {}
			wordEnd[x] = temp;
			
			printWords( &(tree->letters[i]), wordStart, wordEnd);

			wordEnd[x] = 0x0;
		}
	}
	return 0;
}

int main(int argc, char **argv) {
	
	// Checks the correct number of arguments were given in the command line. 
	if (argc == 2) {
		
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
		tree->letters = calloc(27, sizeof(wordTree));
		
			
		// Goes line by line through given file adding words to wordBank
		while (fgets(line, sizeof(line), wordBank)) {
			addToTree(tree, line);
		}
		
		// Checks if tree contains given word
		char word[100];
		printf("Type the word you would like to have completed here: ");
		scanf("%s", word);
		wordTree *start = containsWord(tree, word);



		char *temp = calloc(100, sizeof(char));
		if ( start != NULL) {
			printWords(start, word, temp);	
		} else {
			printf("\"%s\" is not contained in the dictionary given.\n", word);
		}

		// Free Memory Used
		fclose(wordBank);	
		freeMemory(tree);
	        free(tree);	
		free(temp);	

	// Checks if no command line argument were given if so prints error
	} else if (argc < 2) {
		fprintf(stderr, "ERROR: %d argument(s) given, this function requires two.\n", (argc - 1));
		return 1;
	// Checks if to many command line arguments were given if so prints error
	} else {
		fprintf(stderr, "ERROR: To many arguments given, this function can only handle two.\n");
		return 1;
	}
}
