using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEssentials : MonoBehaviour
{
    public int currSceneIndex;

    private void Update()
    {
        currSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    protected void Replay()
    {
        StartCoroutine(this.LoadLevel(currSceneIndex));
    }

    protected void NextLevel()
    {
        StartCoroutine(LoadLevel(currSceneIndex + 1));
    }

    protected IEnumerator LoadLevel(int levelIndex)
    {
        //transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        //transition.ResetTrigger("Start");
        SceneManager.LoadScene(levelIndex);
    }

    protected void ReturnToStart()
    {
        StartCoroutine(LoadLevel(0));
    }
}
