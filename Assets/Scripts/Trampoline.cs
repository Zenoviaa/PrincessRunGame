using System.Collections;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float _jumpSpeedModifier = 1.5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out var player))
        {
            player.ExecuteSuperJump(_jumpSpeedModifier);
        }
    }
}