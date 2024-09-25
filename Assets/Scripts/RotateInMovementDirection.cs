using UnityEngine;

internal class RotateInMovementDirection : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb2D;
    private void Update()
    {
        Vector2 direction = _rb2D.velocity;
        direction = direction.normalized;
        _spriteRenderer.transform.rotation = Helpers.GetAngle(-direction);
    }
}