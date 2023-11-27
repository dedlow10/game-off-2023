using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start_Click()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Quit_Click()
    {
        Application.Quit();
    }

    public void Credits_Click()
    {

    }

    public void HighScores_Click()
    {
        SceneManager.LoadScene("HighScores");
    }
}
