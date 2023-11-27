using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static HighScoreManager;

public class HighScore : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI NameField;
    [SerializeField] TextMeshProUGUI LevelField;
    [SerializeField] TextMeshProUGUI ScoreField;

    public void SetScore(Score score)
    {
        NameField.text = score.Name;
        LevelField.text = score.Level.ToString();
        ScoreField.text = score.Weight.ToString() + " oz";
    }
}
