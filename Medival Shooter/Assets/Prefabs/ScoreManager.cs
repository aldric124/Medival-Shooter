using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    public Text scoreText;

    void Start()
    {
        score = 0;
        UpdateScoreText();
    }

    public static void AddPoint()
    {
        score++;
        FindObjectOfType<ScoreManager>().UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}