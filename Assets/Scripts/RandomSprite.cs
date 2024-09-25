using System.Collections;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _sprites;
    private void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}