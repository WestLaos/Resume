using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MenuEssentials
{
    public void StartGame()
    {
        this.NextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
