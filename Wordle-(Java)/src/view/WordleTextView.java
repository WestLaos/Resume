/**
 * @author West Laos
 * 
 * This class runs a game of wordle using a text based ui
 */
package view;

import java.io.FileNotFoundException;
import java.util.Observable;
import java.util.Observer;
import java.util.Scanner;

import controller.WordleController;
import controller.WordleController.InvalidGuessException;
import model.WordleModel;
import utilities.INDEX_RESULT;

public class WordleTextView implements Observer{
	
	// game logic 
	private WordleModel model;
	private WordleController controler;
	private String playerGuess;
	
	 /**
	 * Starts a game of wordle using text base ui
	 * 
	 * Takes in user input and passes it off to WordleControler untill game has ended
	 * 
	 * @throws FileNotFoundException throws error if Dictionary.txt missing 
	 */
	public void play() throws FileNotFoundException {
		this.model = new WordleModel();
		this.controler = new WordleController(model);
		this.model.addObserver(this);
		
		// used for getting and storing user input
		Scanner playerInput = new Scanner(System.in);  
		this.playerGuess = "";
		
		// used to check if user has entered a valid guess
		boolean waitingForGuess = true;
		
		// repeats until game ends
		while (this.controler.isGameOver() == false) {
			
			// resets
			waitingForGuess = true;
					
			// repeats until player intputs a valid guess
			while (waitingForGuess) {
				
				System.out.print("Enter a guess: ");
				playerGuess = playerInput.nextLine();
				try {
					this.controler.makeGuess(playerGuess.toUpperCase());
					waitingForGuess = false;
				} catch (InvalidGuessException e) {
					System.out.println(e.getLocalizedMessage());
				}
			}
		}
	}
	
	/**
	 * Updates ui when a called
	 * 
	 * Makes updates based on changes in WordleModel
	 * 
	 * @param Observable o: is what this class is observing Object arg: contains command line args
	 */
	@Override
	public void update(Observable o, Object arg) {
		// iterates through all guesses
		for (int i = 0; i < this.controler.getNumOfGuesses(); i++) {
			if (this.controler.getProgress()[i] != null) {
				// iterates through current guess
				for (int x = 0; x < this.controler.getWordLength(); x++) {
					if (this.controler.getProgress()[i].getIndices()[x] == 
							INDEX_RESULT.CORRECT) {
						System.out.print(
								Character.toUpperCase(
										this.controler.getProgress()[i].getGuess().charAt(x)) + " ");
					} else if (this.controler.getProgress()[i].getIndices()[x] == 
							INDEX_RESULT.CORRECT_WRONG_INDEX) {
						System.out.print(
								Character.toLowerCase(
										this.controler.getProgress()[i].getGuess().charAt(x)) + " ");
					} else {
						System.out.print("_ ");
					}
				}
				System.out.println();
			} else {
				// iterates for length of word
				for (int x = 0; x < this.controler.getWordLength(); x++) {
					System.out.print("_ ");
				}
				System.out.println();
			}
		}
		
		boolean incorrect = false;
		boolean almostCorrect = false;
		boolean correct = false;
		
		// prints unguessed characters
		System.out.print("Unguessed: ");
		for (int i = 0; i < 26 ; i++) {
			// if cur value is null means the letter has not been guessed
			if (this.controler.getGuessedChars()[i] == null) {
				// i offsets the ascii value of 'A' to current char
				System.out.print((char)('A' + i) + " ");
			// if cur value is not null check whether any correct incorrect or almost letters
			// have been guessed
			} else {
				if (this.controler.getGuessedChars()[i] == INDEX_RESULT.CORRECT) {
					correct = true;
				} else if (this.controler.getGuessedChars()[i] == INDEX_RESULT.CORRECT_WRONG_INDEX) {
					almostCorrect = true;
				} else {
					incorrect = true;
				}
			}
		}
		System.out.println();
		
		// if any Incorrect letters have been guessed prints them
		if (incorrect) {
			System.out.print("Incorrect: ");
			printIndexType(this.controler.getGuessedChars(),
					INDEX_RESULT.INCORRECT);
		}
		
		// if any correct letters have been guessed prints them
		if (correct) {
			System.out.print("Correct: ");
			printIndexType(this.controler.getGuessedChars(),
					INDEX_RESULT.CORRECT);
		}
		
		// if any correct letters have been guessed prints them
		if (almostCorrect) {
			System.out.print("Correct letter, wrong index: ");
			printIndexType(this.controler.getGuessedChars(),
					INDEX_RESULT.CORRECT_WRONG_INDEX);
		}
		System.out.println("\n");
		
		if (this.controler.isGameOver()) {
			// checks why game ended
			if (this.controler.getAnswer().equals(playerGuess.toUpperCase())) {
				System.out.print("You guessed the correct word CONGRATULATIONS.\n");
			} else {
				System.out.print("Sorry but you are out of guesses better luck next time.\n");
			}
		}
	}
	
	/**
	 * prints all INDEX_RESULTS from an array of a specific type
	 * 
	 * Prints the letter val of indices of the desiredType.
	 * 
	 * @param INDEX_RESULT[] indices , INDEX_RESULT desiredType
	 */
	public static void printIndexType(INDEX_RESULT[] indices, INDEX_RESULT desiredType) {
		for (int i = 0; i < 26; i++) {
			// if cur value is not null and almost correct
			if (indices[i] != null & 
					indices[i] == desiredType) {
				// i offsets the ascii value of 'A' to current char
				System.out.print((char)('A' + i) + " ");
			}
		}
		System.out.println();
	}
}
