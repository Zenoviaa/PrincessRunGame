using System.Collections;
using UnityEngine;

public class ExpandingSquare : MonoBehaviour
{
    private static int _lastLayer;
    private float _time;
    [SerializeField] private float _duration;
    [SerializeField] private float _lifeTime;
    [SerializeField] private Vector3 _startScale;
    [SerializeField] private Vector3 _endScale;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer.transform.localScale = Vector3.zero;
        spriteRenderer.sortingOrder = _lastLayer;
        _lastLayer++;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        float p = _time / _duration;
        float ep = Easing.OutCubic(p);
        spriteRenderer.transform.localScale = Vector3.Lerp(_startScale, _endScale, ep);
        if(_time >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}