/*
 * File: autocomplete.c
 * Author: Frederic W Laos
 * Description: This file takes in incomplete or incorrectly spelled words and
 * 		offers words that they could be meant to be. 
 */
#include <stdio.h>







int main(int argc, char **argv) {
	
	// Checks the correct number of arguments were given in the command line. 
	if (argc == 2) {
	

	// Checks if no command line argument were given if so prints error
	} else if (argc < 2) {
		fprintf(stderr, "ERROR: No arguments given, this function requires one.\n");
		return 1;
	// Checks if to many command line arguments were given if so prints error
	} else {
		fprintf(stderr, "ERROR: Multiple arguements, this function can only handle one.\n");
		return 1;
	}
}
