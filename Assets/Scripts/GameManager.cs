﻿using UnityEngine;
using UnityEngine.SceneManagement;

internal class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIGameOver _uiGameOverPrefab;

    [SerializeField] 
    private UISquareTransition _transitionPrefab;

    [SerializeField]
    private UILevelComplete _uiLevelCompletePrefab;

    [field: SerializeField]
    public bool GameOvered { get; private set; }

    [field: SerializeField]
    public bool EndedRun { get; private set; }

    [field:SerializeField]
    public bool StartedLevel { get; private set; } 

    [field: SerializeField]
    public string[] Levels { get; private set; }

    [field: SerializeField]
    public string WinScreen { get; private set; }

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }



    public void RestartLevel()
    {
        EndedRun = false;
        GameOvered = false;
        StartedLevel = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetDefaults()
    {
        EndedRun = false;
        GameOvered = false;
        StartedLevel = false;
    }

    public void NextLevel()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        string nextLevel = GetNextLevel(currentLevel);
        LoadSceneWithTransition(nextLevel);  
    }

    public void StartLevel()
    {
        StartedLevel = true;
    }

    public void EndRun()
    {
        EndedRun = true;
    }
    public void GameOver()
    {
        if (GameOvered)
            return;
        EndedRun = true;
        GameOvered = true;
        Instantiate(_uiGameOverPrefab);
    }
    public void WinLevel()
    {
        if (GameOvered)
            return;
        EndedRun = true;
        GameOvered = true;
        Instantiate(_uiLevelCompletePrefab);
    }
    public void LoadSceneWithTransition(string scene)
    {
       
        UISquareTransition transition = Instantiate(_transitionPrefab);
        transition.LoadScene(scene);
    }

    private string GetNextLevel(string currentLevel)
    {
        for(int i = 0; i < Levels.Length; i++)
        {
            string level = Levels[i];
            if(level == currentLevel && i + 1 < Levels.Length)
            {
                return Levels[i + 1];
            }
        }

        return WinScreen;
    }
}
