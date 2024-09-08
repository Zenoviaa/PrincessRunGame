using UnityEngine;

internal class LevelCompleter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out var player))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.WinLevel();
        }
    }
}