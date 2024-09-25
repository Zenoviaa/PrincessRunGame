using UnityEngine;

internal class LevelCompleter : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out var player))
        {
            FXManager fXManager = FXManager.Instance;
            fXManager.PlayMusic(_musicClip);
            GameManager gameManager = GameManager.Instance;
            gameManager.WinLevel();
        }
    }
}