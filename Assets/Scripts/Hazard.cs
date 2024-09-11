using UnityEngine;

public class Hazard : MonoBehaviour
{
    private Vector3 _freezePos;
    private bool _hasFrozen;
    public float damage = 1;

    private void Update()
    {
        GameManager gameManager = GameManager.Instance;
        if (gameManager.EndedRun && !_hasFrozen)
        {
            _freezePos = transform.position;
            _hasFrozen = true;
        }else if (gameManager.EndedRun)
        {
            transform.position = _freezePos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(damage);
        }
    }
}