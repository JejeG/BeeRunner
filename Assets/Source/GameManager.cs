using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Object {
    public static GameManager instance = null;
    private static int currentLevel = 0;

    public static bool isEnd = false;
    public static int currentScore = 0;
    public static int distance = 0;

    public int[] levelConfig;

    private int _maxLevel = 20;

    protected GameManager(){
        int baseScore = 10;
        levelConfig = new int[_maxLevel];
        for (int i = 0; i < _maxLevel; i++)
        {
            levelConfig[i] = baseScore;
            baseScore += baseScore;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if(GameManager.instance == null)
            {
                DontDestroyOnLoad(GameManager.instance);
                GameManager.instance = new GameManager();
            }

            return GameManager.instance;
        }
    }

    public void OnApplicationQuit()
    {
        GameManager.instance = null;
    }

    public int getScoreToReach()
    {
        return levelConfig[currentLevel];
    }

    //Initializes the game for each level.
    public void RestartGame()
    {
        ResetLevel();
        StartLevel();

    }

    public void ResetLevel()
    {
        currentLevel = 0;
        currentScore = 0;
        distance = 0;
        isEnd = false;
    }

    public void IncreaseLevel()
    {
        if (currentLevel < levelConfig.Length)
        {
            currentLevel++;
        }
        StartLevel();
    }

    public void StartLevel()
    {
        currentScore = 0;
        distance = 0;
        SceneManager.LoadScene("main");
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public bool hasCompleteAllLevel()
    {
        return currentLevel == (levelConfig.Length - 1);
    }
}
