autocomplete: autocomplete.c
	gcc -Wall -Werror -std=c11 -o autocomplete autocomplete.c

autocompleteDebug: autocomplete.c
	gcc -Wall -Werror -std=c11 -g -c autocomplete.c
	gcc -Wall -Werror -std=c11 -g -o autocompleteDebug autocomplete.c autocomplete.o

clean: 
	rm -f *.o
	rm -f autocomplete
