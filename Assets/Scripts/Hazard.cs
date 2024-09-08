using System.Collections;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public float damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(damage);
        }
    }
}