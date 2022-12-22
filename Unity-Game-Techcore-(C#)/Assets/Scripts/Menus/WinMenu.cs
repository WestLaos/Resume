using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject player;
    public static bool hasWon = false;

    private void Awake()
    {
        hasWon = false;
    }

    private void Update()
    {

    }
}
