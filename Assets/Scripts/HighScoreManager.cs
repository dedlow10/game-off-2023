using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HighScoreManager : MonoBehaviour
{
    private static HighScoreManager instance;

    public static HighScoreManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;

    }
    public bool IsHighScore(float playerScore)
    {
        var scores = GetHighScores();
        return scores.Any(s => playerScore > s.Weight);
    }

    public void SubmitHighScores(string name, int level, float playerScore)
    {
        var scores = GetHighScores();
        scores.Add(new Score { Level = level, Weight = playerScore, Name = name });

        var newScores = scores.OrderByDescending(s => s.Weight).ToList();
        var strHighScores = JsonConvert.SerializeObject(newScores.Take(5));
        PlayerPrefs.SetString("HighScores", strHighScores);
        PlayerPrefs.Save();
    }

    public List<Score> GetHighScores()
    {
        var jsonScores = PlayerPrefs.GetString("HighScores");
        if (jsonScores == null || jsonScores == String.Empty || jsonScores == "{}") return new List<Score> { new Score { Name = "Johnny Groceries", Level = 5, Weight = 5500 }, new Score { Name = "Suzy The Cereal Catcher", Level = 4, Weight = 4100 }, new Score { Name = "Joey Cart-man", Level = 3, Weight = 3250 }, new Score { Name = "Slick Rick Beerman", Level = 3, Weight = 3200 }, new Score { Name = "Paper Bag Man", Level = 1, Weight = 900 } };
        return JsonConvert.DeserializeObject<List<Score>>(jsonScores);
    }

    public class Score
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public float Weight { get; set; }
    }
}
