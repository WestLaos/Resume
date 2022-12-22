using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathMenu : MenuEssentials
{
    public static bool hasLost = false;
    public GameObject deathMenu;
    public TextMeshProUGUI deathText;
    //public static DeathMenu instance;

    private void Awake()
    {
        hasLost = false;
        //if (DeathMenu.instance == null) instance = this;
        //else Destroy(gameObject);
    }
    public void GameOver(string playerName)
    {
        Debug.Log("Game Over");
        hasLost = true;
        deathMenu.GetComponentInChildren<TextMeshProUGUI>().text = playerName + " has Died";
        StartCoroutine(ToggleDeath());
    }

    IEnumerator ToggleDeath()
    {
        deathMenu.SetActive(!deathMenu.activeSelf);
        yield return new WaitForSeconds(5);
        deathMenu.SetActive(!deathMenu.activeSelf);
    }

}
