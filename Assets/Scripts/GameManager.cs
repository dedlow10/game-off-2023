using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int Level;
    [SerializeField] TextMeshProUGUI ScoreField;
    [SerializeField] TextMeshProUGUI TimeRemainingField;
    [SerializeField] TextMeshProUGUI LevelField;
    [SerializeField] GameObject FloatingTextPrefab;

    [SerializeField] GameObject EndGameScreen;
    [SerializeField] TextMeshProUGUI EndGameScoreField;

    [SerializeField] GameObject HighScoreScreen;
    [SerializeField] TextMeshProUGUI HighScoreScoreField;
    [SerializeField] TMP_InputField HighScoreScoreFieldName;

    [SerializeField] Slider SliderField;
    [SerializeField] GameObject PauseScreen;

    [SerializeField] int roundTime;

    private float playerScore;
    private float currentTimeRemaining;

    private static GameManager instance;

    private bool isGameRunning = false;

    private float currentLevelWeightNeeded = 800;

    private int comboCount = 0;

    private float cartSpeedBoost = 1;

    private string activePowerupName = null;
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        currentTimeRemaining = roundTime;
        isGameRunning = true;
    }

    private void Start()
    {
        LevelField.text = Level.ToString();
    }

    public void AddToPlayerScore(float weight)
    {
        playerScore += weight;
    }

    private void Update()
    {
        if (isGameRunning)
        {
            currentTimeRemaining -= Time.deltaTime;

            ScoreField.text = playerScore.ToString() + " oz";
            TimeRemainingField.text = Convert.ToInt32(currentTimeRemaining).ToString();
            SliderField.value = playerScore / currentLevelWeightNeeded;
            if (playerScore >= currentLevelWeightNeeded)
            {
                NextLevel();
            }

            if (currentTimeRemaining <= 0)
            {
                if (playerScore < currentLevelWeightNeeded)
                {
                    isGameRunning = false;
                    Time.timeScale = 0;
                    ShowEndGameScreen();
                }
            }
        }
    }

    public bool GetIsGameRunning()
    {
        return isGameRunning;
    }

    public void ShowEndGameScreen()
    {
        if (HighScoreManager.Instance.IsHighScore(playerScore))
        {
            HighScoreScoreField.text = playerScore + " oz";
            HighScoreScreen.SetActive(true);
        }
        else
        {
            EndGameScoreField.text = playerScore + " oz";
            EndGameScreen.SetActive(true);
        }
    }

    public void ShowFloatingText(GameObject obj, string text)
    {
        var position = obj.transform.position;
        position.y = position.y - 1;
        var ft = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
        var tmp = ft.GetComponent<TextMeshPro>();
        tmp.text = text;
    }

    public void Restart_Click()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
        AudioManager.instance.PlayMusic("Game", .3f);
    }

    public void Exit_Click()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        currentTimeRemaining = 30;
        Level++;
        LevelField.text = Level.ToString();
        currentLevelWeightNeeded = Convert.ToInt32(currentLevelWeightNeeded + 1000 + (500 * GetCurrentLevel()));
        AudioManager.instance.PlaySFX("LevelUp", GameObject.Find("Cart").transform.position, .3f);
    }

    public int GetCurrentLevel()
    {
        return Level;
    }

    public int GetComboCount()
    {
        return comboCount;
    }

    public void IncrementComboCount()
    {
        comboCount++;
    }

    public void ResetComboCount()
    {
        comboCount = 0;
    }

    public void SetCartSpeedBoost(float boost)
    {
        cartSpeedBoost = boost;
    }

    public float GetCartSpeedBoost()
    {
        return cartSpeedBoost;
    }

    public void SetPowerUpActive(string name)
    {
        activePowerupName = name;
    }

    public void AddTimeRemaining(float time)
    {
        currentTimeRemaining += time;
    }

    public bool IsPowerUpActive()
    {
        return activePowerupName != null;
    }

    public string GetActivePowerupName()
    {
        return activePowerupName;
    }

    public static double OuncesToPounds(double ounces)
    {
        double pounds = ounces / 16.0;
        return Math.Round(pounds, 2);
    }

    public void ShowPauseScreen()
    {
        if (!PauseScreen.activeSelf)
        {
            PauseScreen.SetActive(true);
            AudioManager.instance.PauseResume(true);
            Time.timeScale = 0;
        } 
        else
        {
            HidePauseScreen();
        }
    }

    public void HidePauseScreen()
    {
        PauseScreen.SetActive(false);
        Time.timeScale = 1;
        AudioManager.instance.PauseResume(false);
    }

    public void SubmitHighScores()
    {
        HighScoreManager.Instance.SubmitHighScores(HighScoreScoreFieldName.text, GetCurrentLevel(), playerScore);
        SceneManager.LoadScene("MainMenu");
    }
}
