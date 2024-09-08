using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    private bool _input;
    public void RestartLevel()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.RestartLevel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !_input)
        {
            RestartLevel();
            _input = true;
        }
    }
}