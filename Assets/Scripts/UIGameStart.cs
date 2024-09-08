using UnityEngine;

internal class UIGameStart : MonoBehaviour
{
    private bool _input;
    public void StartLevel()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.StartLevel();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !_input)
        {
            StartLevel();
            _input = true;
            Destroy(gameObject);
        }
    }
}
