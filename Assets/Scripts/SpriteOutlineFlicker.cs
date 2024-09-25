using UnityEngine;

public class SpriteOutlineFlicker : MonoBehaviour
{
    private float _time;
    private bool _otherColor;
    private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private SpriteRenderer _targetSpriteRenderer;
    [SerializeField] private SpriteRenderer _spritePrefab;
    [SerializeField] private float _distance;
    [SerializeField] private float _flickerTime;
    [SerializeField] private Color _flickerColor;
    [SerializeField] private Color _flickerColor2;

    private void Start()
    {
        _targetSpriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderers = new SpriteRenderer[4];
        _spriteRenderers[0] = CreateOutline(new Vector2(_distance, 0));
        _spriteRenderers[1] = CreateOutline(new Vector2(-_distance, 0));
        _spriteRenderers[2] = CreateOutline(new Vector2(0,_distance));
        _spriteRenderers[3] = CreateOutline(new Vector2(0,-_distance));
    }

    private SpriteRenderer CreateOutline(Vector3 offset)
    {
        SpriteRenderer spriteRenderer = Instantiate(_spritePrefab);
        spriteRenderer.transform.parent = transform.parent;
        spriteRenderer.transform.localPosition = transform.localPosition + offset;
        return spriteRenderer;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time >= _flickerTime)
        {
            _otherColor = !_otherColor;
            _time = 0;
        }

        if (_otherColor)
        {
            SetColor(_flickerColor);
        }
        else
        {
            SetColor(_flickerColor2);
        }
    }

    private void SetColor(Color color)
    {
        for(int i = 0; i < _spriteRenderers.Length; i++)
        {
            SpriteRenderer spriteRenderer = _spriteRenderers[i];
            spriteRenderer.material.SetColor("_Color", color);
            spriteRenderer.sprite = _targetSpriteRenderer.sprite;
            spriteRenderer.transform.rotation = _targetSpriteRenderer.transform.rotation;
            spriteRenderer.flipX = _targetSpriteRenderer.flipX;
            spriteRenderer.flipY = _targetSpriteRenderer.flipY;
        }
    }
}