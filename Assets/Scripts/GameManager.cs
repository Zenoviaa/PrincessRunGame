﻿using UnityEngine;
using UnityEngine.SceneManagement;

internal class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIGameOver _uiGameOverPrefab;

    [SerializeField]
    private UILevelComplete _uiLevelCompletePrefab;

    [field: SerializeField]
    public bool GameOvered { get; private set; }

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
        GameOvered = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        GameOvered = false;
        string currentLevel = SceneManager.GetActiveScene().name;
        string nextLevel = GetNextLevel(currentLevel);
        SceneManager.LoadScene(nextLevel);  
    }
    public void GameOver()
    {
        if (GameOvered)
            return;
        GameOvered = true;
        Instantiate(_uiGameOverPrefab);
    }
    public void WinLevel()
    {
        if (GameOvered)
            return;
        GameOvered = true;
        Instantiate(_uiLevelCompletePrefab);
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