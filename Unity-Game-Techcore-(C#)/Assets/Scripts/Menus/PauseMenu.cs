using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class PauseMenu : MenuEssentials
{
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public AudioMixer audioMixer;

    private void Awake()
    {
        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) // && DeathMenu.hasLost != true && WinMenu.hasWon != true)
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void ExitLevel()
    {
        Time.timeScale = 1f;
        isPaused = false;
        ReturnToStart();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(volume) * 20);
    }
}
