using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIHandler : MonoBehaviour
{
    private Scene CurrLevel;

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void RetryCurrentLevel()
    {
        CurrLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrLevel.buildIndex);
    }

    public void LoadNextLevel()
    {
        CurrLevel = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CurrLevel.buildIndex + 1);
    }
}