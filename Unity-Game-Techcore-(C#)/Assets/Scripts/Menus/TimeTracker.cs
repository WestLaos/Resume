using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeTracker : MonoBehaviour
{
    public TextMeshProUGUI timeTracker;
    private string player;
    private float _ctMove;
    private float _ctAttack;
    private bool timerActive = false;
    public bool turnActive = false;

    void Update()
    {
        if (!timerActive) return;

        float reduction = 1 * Time.deltaTime;

        if (turnActive)
        {
            if (_ctMove <= 0)
            {
                _ctAttack -= reduction;
                if (_ctAttack <= 0)
                {
                    _ctAttack = 0;
                    turnActive = false;
                }
                if (_ctAttack <= 5) timeTracker.color = Color.red;
                else timeTracker.color = Color.white;
                timeTracker.text = "Time left for " + player + " to fire: " + _ctAttack.ToString("0");
            }
            else
            {
                _ctMove -= reduction;
                if (_ctMove <= 3) timeTracker.color = Color.red;
                else timeTracker.color = Color.white;
                timeTracker.text = "Time left for " + player + " to move: " + _ctMove.ToString("0");
            }
        }
    }

    public void endTimer()
    {
        timeTracker.text = "Game Over";
    }


    public void setTimer(float moveTime, float attackTime, string playerName)
    {
        if (!timerActive) timerActive = true;
        _ctMove = moveTime;
        _ctAttack = attackTime;
        player = playerName;
        turnActive = true;
    }
}
