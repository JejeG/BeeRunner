using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, GAME, END }

public delegate void OnStateChangeHandler();

public class GameManager : Object {
    public static GameManager instance = null;
    private static int currentLevel = 0;

    public static int currentScore = 0;

    public GameState gameState { get; private set; }
    public event OnStateChangeHandler OnStateChange;

    public int[] levelConfig;

    protected GameManager(){
        int baseScore = 10;
        levelConfig = new int[10];
        for (int i = 0; i < 10; i++)
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

    public void SetGameState(GameState state)
    {
        this.gameState = state;
        OnStateChange();
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
        SceneManager.LoadScene("main");
    }

    public void Toto()
    {
        Debug.Log("toto");
    }
}
