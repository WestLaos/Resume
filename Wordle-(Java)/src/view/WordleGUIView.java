/**
 * @author West Laos
 * 
 * This class runs a game of wordle using a graphic based ui
 */
package view;
import java.io.FileNotFoundException;
import java.util.Observable;
import java.util.Observer;
import java.util.Optional;

import controller.WordleController;
import controller.WordleController.InvalidGuessException;
import javafx.animation.PauseTransition;
import javafx.animation.RotateTransition;
import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.Node;
import javafx.scene.Scene;
import javafx.scene.control.Alert;
import javafx.scene.control.Button;
import javafx.scene.control.ButtonType;
import javafx.scene.control.Label;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.layout.Background;
import javafx.scene.layout.BackgroundFill;
import javafx.scene.layout.Border;
import javafx.scene.layout.BorderStroke;
import javafx.scene.layout.BorderStrokeStyle;
import javafx.scene.layout.BorderWidths;
import javafx.scene.layout.CornerRadii;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.TilePane;
import javafx.scene.layout.VBox;
import javafx.scene.paint.Color;
import javafx.scene.paint.Paint;
import javafx.scene.text.Font;
import javafx.scene.text.TextAlignment;
import javafx.stage.Popup;
import javafx.stage.PopupWindow;
import javafx.stage.Stage;
import javafx.util.Duration;
import model.WordleModel;
import utilities.INDEX_RESULT;

public class WordleGUIView extends Application implements Observer{

	/* Constants for the scene */
	private static final int SCENE_WIDTH = 800;
	private static final int SCENE_HEIGHT = 800;
	
	/* Constants for grid of letters */
	private static final int GRID_GAP = 4;

	/* Constants for letters in grid */
	private static final int LETTER_FONT_SIZE = 55;
	private static final int LETTER_SQUARE_SIZE = 65;
	
	/* Constants for letters in letter display */
	private static final int DISPLAY_FONT_SIZE = 35;
	private static final int DISPLAY_SQUARE_SIZE = 45;
	
	/* Constants for background */
	private static final Background BACKGROUND_WHITE = 
			new Background(new BackgroundFill(Color.rgb(250, 250, 250, 1), new 
					CornerRadii(5.0), new Insets(0)));
	private static final Background BACKGROUND_GREY = 
			new Background(new BackgroundFill(Color.rgb(200, 200, 200, 1), new 
					CornerRadii(5.0), new Insets(0)));
	private static final Background BACKGROUND_RED = 
			new Background(new BackgroundFill(Color.rgb(238, 75, 43, 1), new 
					CornerRadii(5.0), new Insets(0)));
	private static final Background BACKGROUND_YELLOW = 
			new Background(new BackgroundFill(Color.rgb(225, 234, 0, 1), new 
					CornerRadii(5.0), new Insets(0)));
	private static final Background BACKGROUND_GREEN = 
			new Background(new BackgroundFill(Color.rgb(34, 139, 34, 1), new 
					CornerRadii(5.0), new Insets(0)));
	
	// stores all labels used in player input area
	private Label[][] inputLabel;
	// stores all labels used in letters used display
	private Label[] alphabet; 
	// gui
	private Stage stage;
	private VBox vbox;
	// game logic 
	private WordleModel model;
	private WordleController controler;
	// stores players current guess
	private String curGuess;
	
	/**
	* Starts a game of wordle using graphic based ui
	* 
	* Takes in user input and passes it off to WordleControler until game has ended
	* 
	* @param Stage stage: is the stage on which all graphics are displayed
	* 
	* @throws FileNotFoundException throws error if Dictionary.txt missing 
	*/
	@Override
	public void start(Stage stage) throws FileNotFoundException {
	
		this.stage = stage;
		this.model = new WordleModel();
		this.model.addObserver(this);
		this.controler = new WordleController(model);
		
	    GridPane inputGrid = createGrid(); 
	    inputLabel = new Label[controler.getNumOfGuesses()][controler.getWordLength()];
		for (int y = 0; y < controler.getNumOfGuesses(); y++) {
			for (int x = 0; x < controler.getWordLength(); x++) {
				inputLabel[y][x] = createLabel(null, 
						LETTER_SQUARE_SIZE, LETTER_FONT_SIZE, BACKGROUND_WHITE, true);
				inputGrid.add(inputLabel[y][x], x, y);
			}
		}
		
		GridPane alphabetGird = createGrid();
		this.alphabet = new Label[26];
		int curLetter = 0;
		for (int y = 0; y < 4; y++) {
			for (int x = y; x < (11 - y); x++) {
				if (curLetter > 25) {
					break;
				}
				alphabet[curLetter] = createLabel((char)('A' + curLetter) + "", 
						DISPLAY_SQUARE_SIZE, DISPLAY_FONT_SIZE, BACKGROUND_GREY, false);
				alphabetGird.add(alphabet[curLetter], x, y);
				curLetter += 1;
			}
		}
		
		// sets up scene an shows stage
		this.vbox = new VBox();
		this.vbox.getChildren().add(new Label()); 		// creates space between top and first row
		this.vbox.getChildren().add(inputGrid); 		// first row set to gridpane
		this.vbox.getChildren().add(new Label()); 		// creates space between first and third row
		this.vbox.getChildren().add(alphabetGird); 		// third row set to letterGird
		this.vbox.setBackground(BACKGROUND_WHITE); 		// sets background color to white
		this.vbox.setAlignment(Pos.CENTER);
        Scene scene = new Scene(this.vbox, SCENE_WIDTH, SCENE_HEIGHT);
        stage.setScene(scene);
        stage.setTitle("Wordle!");
		stage.show();
		
		
		curGuess = "";

		scene.setOnKeyPressed(new EventHandler<KeyEvent>() {
		    public void handle(KeyEvent ke) {
		    	// To check if the user pressed delete or backspace (in order to delete a character for a guess)
		    	if (ke.getCode().equals(KeyCode.DELETE) || ke.getCode().equals(KeyCode.BACK_SPACE)) {
		    		if (curGuess.length() > 0) {
			    		inputLabel[controler.getNumGuessesMade()][curGuess.length() - 1]
				    			.setText("");
			    		curGuess = curGuess.substring(0, curGuess.length() - 1);
			    		stage.show();
		    		}
		    	// To check if the user pressed enter (in order to enter a guess)
		    	} else if (ke.getCode().equals(KeyCode.ENTER)) { 
		    		try {
						controler.makeGuess(curGuess);
						curGuess = "";
					} catch (InvalidGuessException e) {
						invalidInput(e.getLocalizedMessage());
					}
		    		
		    		
		    	// gets the character entered from the keyboard and checks its a letter 
		    	// also check that their arnt the max number of letters entered already
		    	} else if (ke.getCode().getName().matches("[a-zA-Z]+") & 
		    			(curGuess.length() < controler.getWordLength()) &
		    			ke.getCode() != KeyCode.SHIFT) {
			    	curGuess += ke.getCode().getName();
			    	inputLabel[controler.getNumGuessesMade()][curGuess.length() - 1]
			    			.setText(ke.getCode().getName());
					stage.show();
		    	} else {}
		    }
		});
	}
	
	/**
	* Creates alert telling user and error occured.
	* 
	* Animates current row turning gray and alerts users as to what was wrong with their guess.
	* 
	* @param String error: 
	*/
	private void invalidInput(String error) {
		for (int i = 0; i < controler.getWordLength(); i++) {
			inputLabel[controler.getNumGuessesMade()][i].setBackground(BACKGROUND_GREY);
		}
		Alert popup = new Alert(Alert.AlertType.INFORMATION);
		popup.setTitle("Wordle!");
		popup.setHeaderText("");
		popup.setContentText(error);
		popup.showAndWait();
		PauseTransition pause = new PauseTransition(Duration.seconds(.15));
		pause.setOnFinished(event -> {
			for (int i = 0; i < controler.getWordLength(); i++) {
				inputLabel[controler.getNumGuessesMade()][i].setBackground(BACKGROUND_WHITE);
				this.vbox.getChildren().set(0, new Label());
				stage.show();
			}
		});
		pause.play(); 
	}

	/**
	* Creates a grid pane.
	* 
	* the grid panes height, width and background color all come from constants in class
	* 
	* @return Gridpane 
	*/
	private GridPane createGrid() {
		GridPane grid = new GridPane();					// creates new GridPane
		grid.setHgap(GRID_GAP);							// sets horizontal gap in grid
		grid.setVgap(GRID_GAP);							// sets vertical gap in grid
		grid.setBackground(BACKGROUND_WHITE);			// sets back ground color
		grid.setAlignment(Pos.CENTER);					// sets position
		return grid;
	}
	
	/**
	* Creates a label.
	* 
	* creates a label based on the parameters given
	* 
	* @param String letter, int boxSize, int fontSize, Background color, boolean border
	* 
	* @return Label 
	*/
	private static Label createLabel(String letter, int boxSize, int fontSize,
			Background color,  boolean border) {
		Label label = new Label(letter); 				// creates new label
		
		label.setMinHeight(boxSize);					// sets height of label
		label.setMinWidth(boxSize);						// sets width of label
		label.setAlignment(Pos.TOP_CENTER);				// sets text position
		label.setFont(new Font(fontSize));				// sets font size
		label.setTextFill(Paint.valueOf("black"));		// sets font color
		label.setBackground(color);						// sets background color
		if (border) {
			label.setBorder(new Border(new BorderStroke(Color.rgb(0, 0, 0, 1), 
				BorderStrokeStyle.SOLID, new CornerRadii(5.0), new BorderWidths(.5))));
		}
		return label;
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
		for (int i = 0; i < 25; i++) {
			if (controler.getGuessedChars()[i] != null & this.alphabet != null) {
				if (controler.getGuessedChars()[i] == INDEX_RESULT.CORRECT) {
					alphabet[i].setBackground(BACKGROUND_GREEN);
				} else if (controler.getGuessedChars()[i] == INDEX_RESULT.CORRECT_WRONG_INDEX) {
					alphabet[i].setBackground(BACKGROUND_YELLOW);
				} else {
					alphabet[i].setBackground(BACKGROUND_RED);
				}
			}
		}
		
		for (int y = 0; y < this.model.getNumGuessesMade(); y++) {
			for (int x = 0; x < this.model.getWordLength(); x++) {
				if (model.getProgress()[y].getIndices()[x] == INDEX_RESULT.CORRECT) {
					this.inputLabel[y][x].setBackground(BACKGROUND_GREEN);
				} else if (model.getProgress()[y].getIndices()[x] == INDEX_RESULT.CORRECT_WRONG_INDEX) {
					this.inputLabel[y][x].setBackground(BACKGROUND_YELLOW);
				} else {
					this.inputLabel[y][x].setBackground(BACKGROUND_RED);
				}
			}
		}
		stage.show();	
		if (this.controler.isGameOver()) {
			Alert popup = new Alert(Alert.AlertType.INFORMATION);
			popup.setTitle("Wordle!");
			if (this.controler.getProgress()[controler.getNumGuessesMade() - 1].getIsCorrect()) {
				gameWinAnimation(this.controler.getNumGuessesMade() - 1);
				popup.setY(90 * this.controler.getNumGuessesMade() + 90);
				popup.setX(550);
				popup.setHeaderText("Congratulations You Guessed The Word Correctly!!!");
				popup.setContentText("Correct Word: " + controler.getAnswer());
				popup.showAndWait();
			} else {
				popup.setHeaderText("Sorry You Are Out Of Guesses Better Luck Next Time.");
				popup.setContentText("Correct Word: " + controler.getAnswer());
				popup.showAndWait();
			}
			
			Alert playAgain = new Alert(Alert.AlertType.CONFIRMATION);
			playAgain.setTitle("Wordle");
			playAgain.setHeaderText("Would You Like To Play Again");
			playAgain.setContentText("");
			playAgain.getButtonTypes().set(0, ButtonType.YES);
			playAgain.getButtonTypes().set(1, ButtonType.NO);
			
			Optional<ButtonType> result = playAgain.showAndWait();
			ButtonType button = result.orElse(ButtonType.CANCEL);

			if (button == ButtonType.YES) {
			    stage.close();
			    try {
					this.start(new Stage());
				} catch (FileNotFoundException e) {}
			} else {
				stage.close();
				System.exit(0);
			}
		}		
	}
	
	/**
	 * Causes all labels in given row to rotate
	 * 
	 * Create a rotation trasition and applies it to given row in this.Label
	 * 
	 * @param int row signifies which row to apply animation to
	 */
	private void gameWinAnimation(int row) {
		RotateTransition[] rotate = new RotateTransition[this.controler.getNumOfGuesses()];
		for (int i = 0; i < this.controler.getWordLength(); i++) {
			rotate[i] = new RotateTransition();
			rotate[i].setNode(this.inputLabel[row][i]);
		    rotate[i].setByAngle(360); 
			rotate[i].play();
		}
	}
}
