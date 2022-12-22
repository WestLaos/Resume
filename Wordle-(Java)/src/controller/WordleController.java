/**
 * @author West Laos
 * 
 * This class works as the itermediary for the view and model taking all input from view
 * checking it and giving it to the model for use. And returning any data from the model to
 * the view that is needed
 */
package controller;

import controller.WordleController.InvalidGuessException;
import model.WordleModel;
import utilities.Guess;
import utilities.INDEX_RESULT;

public class WordleController {
	
	
	private int NUM_OF_GUESSES;
	private int WORD_LENGTH;
	
	private WordleModel model;
	
	
	public WordleController (WordleModel model) {
		// sets constants
		this.NUM_OF_GUESSES = model.getNumOfGuesses();
		this.WORD_LENGTH =  model.getWordLength();
		
		this.model = model;
	} 
	
	/**
	 * Returns true or false to indicate if game is over.
	 * 
	 * If player has either guessed the correct word or run out of guesses 
	 * this will return true else it will return false.
	 * 
	 * @return isGameOver a boolean value that is false 
	 * when the game is still running and true when the end has been reached.
	 */
	public boolean isGameOver() {
		return this.model.isGameOver;
	}
	
	/**
	 * This returns a string of the correct word.
	 * 
	 * This method gets the correct word from the model and then returns it.
	 * 
	 * @return a String value of the correct word.
	 */
	public String getAnswer() {
		return model.getAnswer();
	}
	
	/**
	 * @author West Laos
	 *
	 * This class extends Exception and is used to indicate to the view when
	 * the user has entered and Invalid guess
	 *
	 */
	public class InvalidGuessException extends Exception {
		public InvalidGuessException(String message) {
			super(message);
		}
	}
	
	/**
	 * @param guess a String value that contains the players guess at the correct word
	 * @throws InvalidGuessException this indicates the String given in the parameter guess was either
	 * to long to short or not contained in the file Dictionary.txt
	 */
	public void makeGuess(String guess) throws InvalidGuessException {
		// checks guess is a valid length
		if (guess.length() != this.WORD_LENGTH) {
			 throw new InvalidGuessException("Guesses must be 5 digits long");
		}
		
		// checks guess is a valid word
		if (this.model.isWordValid(guess) == false) {
			throw new InvalidGuessException("Guess is not valid word from game dictionary");
		}
		
		this.model.makeGuess(this.model.getNumGuessesMade(), guess);		
	}
	
	/**
	 * Retuns a int represent the num of guesses given to the player.
	 * 
	 * Gets the number of guesses the player has from the model and returns it.
	 * 
	 * @return int represent the num of guesses given to the player.
	 */
	public int getNumOfGuesses() {
		return this.NUM_OF_GUESSES;
	}
	
	/**
	 * Returns int representing the Length of words in wordle.
	 * 
	 * Gets the length of words from the model and returns it
	 * 
	 * @return int representing the Length of words in wordle.
	 */
	public int getWordLength() {
		return this.WORD_LENGTH;
	}
	
	/**
	 * Retuns a int represent the num of guesses player has made so far
	 * 
	 * Gets the number of guesses the player has made from the model and returns it.
	 * 
	 * @return int represent the num of guesses made by the player.
	 */
	public int getNumGuessesMade() {
		return model.getNumGuessesMade();
	}
	
	/**
	 * Returns a INDEX_RESULT[] containing all guessed characters.
	 * 
	 * Returns a INDEX_RESULT[] of length 26 containing INDEX_RESULTS for guessed
	 * characters at their alphabetical location and null values for characters 
	 * that have not been guessed.
	 * 
	 * @return a INDEX_RESULT[] containing all guessed characters.
	 */
	public INDEX_RESULT[] getGuessedChars() {
		return model.getGuessedCharacters();
	}
	
	/**
	 * Returns a Guess[] containing all guesses made so far.
	 * 
	 * Returns a Guess[] of length model.NUM_OF_GUESSES containing all guesses
	 * made so far and null values for remaining guesses.
	 * 
	 * @return a Guess[] containing all guesses made in the game so far.
	 */
	public Guess[] getProgress() {
		return model.getProgress();
	}
}
