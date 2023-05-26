using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string mainLevelName;
    [SerializeField] private string mainMenuName;
    [SerializeField] private string creditsName;

    public void LaunchGame()
    {
        SceneManager.LoadScene(mainLevelName);
        Time.timeScale = 1;
    }

    public void LaunchMainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }

    public void LaunchCredits()
    {
        SceneManager.LoadScene(creditsName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
