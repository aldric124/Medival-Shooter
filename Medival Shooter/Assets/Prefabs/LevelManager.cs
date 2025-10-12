using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    private int currentLevel = 0;

    void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        levels[currentLevel].SetActive(true);
    }

    public void CompleteLevel()
    {
        levels[currentLevel].SetActive(false);
        currentLevel++;

        if (currentLevel < levels.Length)
        {
            StartLevel();
        }
        else
        {
            ShowVictoryScreen();
        }
    }

    void ShowVictoryScreen()
    {
        Debug.Log("You saved the princess!");
        // You can activate a Canvas or animation here
    }
}