using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseGO;
    private bool isPaused = false;
    private bool canPause = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseGO.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseGO.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void DesactivatePause()
    {
        canPause = false;
    }
    public void ReactivatePause()
    {
        canPause = true;
    }
}
