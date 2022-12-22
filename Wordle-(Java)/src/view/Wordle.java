/**
 * @author West Laos
 * 
 * This program runs lets the user play the game wordle the user can either
 * play it through a gui or a text based interface as designated by the comand line args
 * they select
 * 
 */
package view;

import java.io.FileNotFoundException;
import java.util.Scanner;

import javafx.application.Application;


public class Wordle {
	
    public static void main(String[] args) throws FileNotFoundException {
    	boolean repeat = true;
    	while (repeat) {
			if (args.length > 0 && args[0].equals("-line")) {
				// this function creates and run the WordleTextView class
				playWordleTextView();
				// this function promptes user to see if they would like to play again
				// and if not it exits the programs
				repeat = playAgainTextView();
				
			} else {
				// this function run the WordleGUIView class
				Application.launch(WordleGUIView.class, args);
				repeat = false;
			}
    	}
    	
    	
    	
    	
    }
    
    
    /**
	 * Starts a game of wordle using text base ui
	 * 
	 * Creates new instance of WordleTextView and plays it
	 * 
	 * @throws FileNotFoundException throws error if Dictionary.txt missing 
	 */
    private static void playWordleTextView() throws FileNotFoundException {
    	WordleTextView wordle = new WordleTextView();
    	wordle.play();
    }
    /**
	 * Returns a boolean representing whether the player has selected to play another 
	 * round of wordle.
	 * 
	 * Will take user input until the user has inidicated whether they would like to play
	 * another round of wordle.
	 * 
	 * @return boolean representing if the user wants to play wordle again.
	 */
    private static boolean playAgainTextView() {
    	Scanner userInput = new Scanner(System.in);  
    	String playAgain = "";
		// checks if user would like to play again and if not exits
		System.out.println("Would you like to play again? yes/no");
		while (playAgain == "") {
			playAgain = userInput.nextLine();
			
			// if they want to play again the code does nothing the 
			// loop will end on its own and the greater loop will 
			// repeat
			if (playAgain.toLowerCase().equals("yes") 
					| playAgain.toLowerCase().equals("y")) {
					return true;
			// if they do not want to play again exits the programs
			} else if (playAgain.toLowerCase().equals("no") 
					| playAgain.toLowerCase().equals("n")) {
				System.out.print("Thanks for playing!!!");
				System.exit(0);
				
			// if they give an invalid input prints a message explaining the 
			// the input is invalid then resets playAgain and repats loop
			} else {
				System.out.println("Sorry " + playAgain 
						+ " is not a valid input please enter yes or no ");
				playAgain = "";
			}
		}
		return false;
    }
}

