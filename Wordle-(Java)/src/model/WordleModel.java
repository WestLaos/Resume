/**
 * @author West Laos
 * 
 * This class stores all game data and handles most of the game logic 
 */

package model;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Observable;
import java.util.Observer;
import java.util.Scanner;

import controller.WordleController.InvalidGuessException;
import utilities.Guess;
import utilities.INDEX_RESULT;

public class WordleModel extends java.util.Observable{
	
	private static final String FILENAME = "dictionary.txt";
	
	// these store constants for game characteristics 
	private final int NUM_OF_GUESSES = 6;
	private final int WORD_LENGTH = 5;
	
	private INDEX_RESULT[] guessedCharacters;
	private Guess[] progress;
	private String[] validWords;
	private String answer;
	private int numGuessesMade;
	public boolean isGameOver;

	
	public WordleModel() throws FileNotFoundException { 
		// gets answers and all valid words
		this.answer = getWordFromDict();
		// tracks which characters have been guessed and the results
		this.guessedCharacters = new INDEX_RESULT[26];
		// tracks all valid guesses made
		this.progress = new Guess[NUM_OF_GUESSES];
		this.numGuessesMade = 0;
		this.isGameOver = false;
	}
	
	/**
	 * Returns a word to be used as the correct answer for the game. 
	 * 
	 * A word is selected from Dictionary.txt and returned and used as answer for game.
	 * Also initiallized the validWords field filling it with all words from Dictionary.txt.
	 * 
	 * @return String of a word
	 * @throws FileNotFoundException throws error if Dictionary.txt missing 
	 */
	private String getWordFromDict() throws FileNotFoundException {
		// creates scanner of all the lines in Dictionary (FILENAME constant)
		Scanner dictionary = new Scanner(new File(FILENAME));
		
		// stores all the words from Dictionary 
		// with each word separated by a space
		String allWords = "";
		
		// adds words from Dictionary to allWords
		while (dictionary.hasNextLine()) {
			allWords += dictionary.nextLine() + " ";
		}
		allWords = allWords.trim();
		dictionary.close();
		
		// sets validWords
		this.validWords = allWords.split(" ");
		
		// splits allWords into an array then selects a random words from the array
		return 
			allWords.split(" ")[(int) (Math.random() * 
					(allWords.split(" ").length - 1))].toUpperCase();
	}
	
	/**
	 * Takes in a string value and turns it into a guess and adds it to progress
	 * 
	 * Takes in a string value sees which letters in it if any match the answers and creates
	 * and Guess to represent how close it was to the correct answer and if it was correct.
	 * 
	 * @param guess String value representing player guess at correct the answer
	 */
	public void makeGuess(int guessNumber, String guess) {
		// adds letters to guessed Characters and to guessIndecis
		INDEX_RESULT[] guessIndices = new INDEX_RESULT[this.WORD_LENGTH];
		INDEX_RESULT curIndex;
		// iterates through chars in guess
		for (int i = 0; i < this.WORD_LENGTH; i++) {
			// if cur char is correct
			if (guess.charAt(i) == this.answer.charAt(i)) {
					this.guessedCharacters[guess.charAt(i) - 'A'] = INDEX_RESULT.CORRECT;
					guessIndices[i] = INDEX_RESULT.CORRECT;
			// if cur char is in word but in wrong place 
			// * + "" converts char to str
			} else if (this.answer.contains(guess.charAt(i) + "")) {
				// checks if char has been guess correctly before
				if (this.guessedCharacters[guess.charAt(i) - 'A'] != INDEX_RESULT.CORRECT) {
					this.guessedCharacters[guess.charAt(i) - 'A'] = INDEX_RESULT.CORRECT_WRONG_INDEX;
					guessIndices[i] = INDEX_RESULT.CORRECT_WRONG_INDEX;
				}
			// if cur char is not in the word
			} else {
				this.guessedCharacters[guess.charAt(i) - 'A'] = INDEX_RESULT.INCORRECT;
				guessIndices[i] = INDEX_RESULT.INCORRECT;
			}
		}
		// makes a new "Guess" and adds it to progress
		this.progress[this.numGuessesMade] = 
				new Guess(guess, guessIndices, guess.toUpperCase().equals(this.answer));
		this.numGuessesMade += 1;
		
		// if player has guessed correct word game is over
		if (this.progress[this.numGuessesMade- 1].getIsCorrect()) {
			this.isGameOver = true;
		// if player has made maximum number of guesses game is over
		} else if (this.NUM_OF_GUESSES == this.numGuessesMade){
			this.isGameOver = true;
		}
		
		this.setChanged();
		this.notifyObservers();
		this.clearChanged();
	}

	/**
	 * Returns the answer.
	 * 
	 * Returns private parameter String answer.
	 * 
	 * @return String representing answer.
	 */
	public String getAnswer() {
		return this.answer;
	}

	/**
	 * Returns an INDEX_RESULT[] containing all guessed characters 
	 * 
	 * Returns a INDEX_RESULT[] length 26 containing all guessed characters 
	 * at their positions in the alphabet with the positions of unguessed characters
	 * storing null values
	 * 
	 * @return INDEX_RESULT[] storing all guessed characters.
	 */
	public INDEX_RESULT[] getGuessedCharacters() {
		return this.guessedCharacters;
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
		return this.progress;
		
	}
	
	/**
	 * Checks if word given is valid for the game
	 * 
	 * Returns true if the word given is contained in Dictionary.txt else returns false
	 * 
	 * @param word String of word to be checked against word in dictionary
	 * @return Boolean representing if word is valid
	 */
	public boolean isWordValid(String word) {
		for (int i = 0; i < this.validWords.length; i++) {
			if (validWords[i].toUpperCase().equals(word.toUpperCase())) {
				return true;
			}
		}
		return false;
	}
	
	/**
	 * Retuns a int represent the num of guesses player has made so far
	 * 
	 * Gets the number of guesses the player has made from the model and returns it.
	 * 
	 * @return int represent the num of guesses made by the player.
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
	 * Retuns a int represent the num of guesses given to the player.
	 * 
	 * Gets the number of guesses the player has from the model and returns it.
	 * 
	 * @return int represent the num of guesses given to the player.
	 */
	public int getNumGuessesMade() {
		return this.numGuessesMade;
	}
	
	/**
	 * Retuns a boolean representing if game is over.
	 * 
	 * Returns true if game is over returns false if game has not ended
	 * 
	 * @return boolean 
	 */
	public boolean isGameOver() {
		return this.isGameOver;
	}
}
