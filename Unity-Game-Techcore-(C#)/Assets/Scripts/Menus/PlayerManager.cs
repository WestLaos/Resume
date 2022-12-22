using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject[] playerList;
    private GameObject playerA;
    private GameObject playerB;
    private PlayerController paController;
    private PlayerController pbController;
    public HealthBar hpA;
    public HealthBar hpB;
    public Camera overviewCamera;
    public Camera playerCamera;
    public TimeTracker timeTracker;
    private int numTurns;
    private GameObject opponent;
    private GameObject currPlayer;
    private bool gameOver = false;
    //private bool isShown = false;
    //public DeathMenu deathMenu;
    public ResultsScreen resultsScreen;

    private void Awake()
    {
        int _paIndex;
        int _pbIndex;
        do
        {
            _paIndex = Random.Range(0, 3);
            _pbIndex = Random.Range(0, 3);
        } while (_paIndex == _pbIndex);

        playerA = Instantiate(playerList[_paIndex]) as GameObject;
        playerB = Instantiate(playerList[_pbIndex]) as GameObject;


        paController = playerA.GetComponent<PlayerController>();
        pbController = playerB.GetComponent<PlayerController>();
        paController.playerCamera = playerCamera;
        paController.overviewCamera = overviewCamera;
        pbController.playerCamera = playerCamera;
        pbController.overviewCamera = overviewCamera;
        //paController.hpBar = hpA;
        //pbController.hpBar = hpB;
        playerA.name = "Player A";
        playerB.name = "Player B";
        paController.enabled = true;
        pbController.enabled = false;
        playerCamera.GetComponent<FollowPlayer>().setPlayer(playerA);
        numTurns = 1;
        opponent = playerB;
        currPlayer = playerA;
    }

    private void Update()
    {
        if (opponent.GetComponent<PlayerHealth>().isAlive == false  && !gameOver)
        {
            gameOver = true;
            currPlayer.GetComponent<PlayerController>().enabled = false;
            resultsScreen.GameOverbyDeath(currPlayer.name, opponent.name);
            
        }
        else if (currPlayer.GetComponent<PlayerHealth>().isAlive == false && !gameOver)
        {
            gameOver = true;
            currPlayer.GetComponent<PlayerController>().enabled = false;
            resultsScreen.GameOverbyDeath(opponent.name, currPlayer.name);
        }
        else if (!timeTracker.turnActive && !gameOver)            //Input.GetKeyDown(KeyCode.S) && overviewCamera.enabled)
        {
            if(numTurns < 6)
            {
                numTurns++;
                SwapPlayer();
            }
            else
            {
                gameOver = true;
                int pAhealth = playerA.GetComponent<PlayerHealth>().playerHealth;
                int pBhealth = playerB.GetComponent<PlayerHealth>().playerHealth;
                resultsScreen.GameOverbyTurns(pAhealth, pBhealth, playerA.name, playerB.name);
                timeTracker.timeTracker.text = "Game Over";
            }
        }
    }

    
    //IEnumerator GameOverRoutine()
    //{
        //yield return new WaitForSeconds(2);
        //gameOver = true;
        //deathMenu.GameOver(opponent.name);
        //resultsScreen.GameOver(currPlayer.name, opponent.name);
    //}
    

    private void SwapPlayer()
    {
        if (paController.enabled == true)
        {
            pbController.enabled = true;
            paController.enabled = false;
            opponent = playerA;
            currPlayer = playerB;
            playerCamera.GetComponent<FollowPlayer>().setPlayer(playerB);
            pbController.resetTimer();
        }
        else if (pbController.enabled == true)
        {
            paController.enabled = true;
            pbController.enabled = false;
            opponent = playerB;
            currPlayer = playerA;
            playerCamera.GetComponent<FollowPlayer>().setPlayer(playerA);
            paController.resetTimer();
        }
    }
}
