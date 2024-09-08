using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private GameObject _collectFXPrefab;

    [SerializeField]
    private AudioClip _collectFXSound;

    public int value;

    private void Collect(Player player)
    {
        player.Collect(value);
        if(_collectFXPrefab != null)
            Instantiate(_collectFXPrefab, transform.position, _collectFXPrefab.transform.rotation);
        FXManager fXManager = FXManager.Instance;
        fXManager.PlaySound(_collectFXSound);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Player>(out var player))
        {
            Collect(player);
        }
    }
}
