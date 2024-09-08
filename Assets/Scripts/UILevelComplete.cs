using UnityEngine;

internal class UILevelComplete : MonoBehaviour
{
    private bool _input;
    public void NextLevel()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.NextLevel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !_input)
        {
            NextLevel();
            _input = true;
        }
    }
}
