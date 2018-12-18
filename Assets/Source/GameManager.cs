using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { MENU, GAME, END }

public delegate void OnStateChangeHandler();

public class GameManager : Object {
    public static GameManager instance = null;
    private static int currentLevel = 0;
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

    //Awake is always called before any Start functions
    //void Awake()
    //{
    //    //Check if instance already exists
    //    if (instance == null)

    //        //if not, set instance to this
    //        instance = this;

    //    //If instance already exists and it's not this:
    //    else if (instance != this)

    //        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
    //        Destroy(gameObject);

    //    //Sets this to not be destroyed when reloading scene
    //    DontDestroyOnLoad(gameObject);

    //    //Call the InitGame function to initialize the first level 
    //    InitGame();
    //}

    //Initializes the game for each level.
    public void RestartGame()
    {
        ResetLevel();
        StartLevel();
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        // boardScript.SetupScene(level);

    }

    public void ResetLevel()
    {
        currentLevel = 0;
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
        SceneManager.LoadScene("main");
    }
}
