using UnityEngine;

public class Collectible : MonoBehaviour
{
    private bool _hasCollected;
    [SerializeField]
    private GameObject _collectFXPrefab;

    [SerializeField]
    private AudioClip _collectFXSound;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private Material _spriteWhite;

    public int value;

    private void Update()
    {
        if (_hasCollected)
        {
            _spriteRenderer.transform.localScale = Vector3.Lerp(_spriteRenderer.transform.localScale, Vector3.zero, Time.deltaTime * 5f);
        }
    }

    private void Collect(Player player)
    {
        player.Collect(value);
        if(_collectFXPrefab != null)
            Instantiate(_collectFXPrefab, transform.position, _collectFXPrefab.transform.rotation);
        FXManager fXManager = FXManager.Instance;
        fXManager.PlaySound(_collectFXSound);
        _hasCollected = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_hasCollected)
            return;

        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            _spriteRenderer.material = _spriteWhite;
            Collect(player);
        }
    }
}
