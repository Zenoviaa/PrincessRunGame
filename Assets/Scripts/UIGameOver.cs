using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public void RestartLevel()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.RestartLevel();
    }
}