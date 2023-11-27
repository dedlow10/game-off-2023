using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreScreen : MonoBehaviour
{
    [SerializeField] GameObject ScorePlaceholder;
    [SerializeField] HighScore ScorePrefab;

    private void Start()
    {
        var scores = HighScoreManager.Instance.GetHighScores();
        foreach(var score in scores)
        {
            var newScore = Instantiate(ScorePrefab, ScorePlaceholder.transform);
            newScore.SetScore(score);
        }
    }

    public void Back_Click() 
    {
        SceneManager.LoadScene("MainMenu");
    }

}
