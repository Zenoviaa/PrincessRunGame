using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EffectSpriteFlash : MonoBehaviour
{
    private SpriteRenderer _sr;
    public SpriteRenderer Target { get; set; }
    private float _elapsedTime;
    [SerializeField] private float _duration;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        _elapsedTime += Time.deltaTime;
        if (Target != null)
        {
            _sr.sprite = Target.sprite;
            _sr.flipX = Target.flipX;
            _sr.flipY = Target.flipY;
            transform.localScale = Target.transform.localScale;
            transform.rotation = Target.transform.rotation;
            transform.position = Target.transform.position;
        }

        float progress = _elapsedTime / _duration;
        float easedProgress = Easing.InOutCubic(progress);
        _sr.color = Color.Lerp(Color.white, Color.clear, easedProgress);
        if (_elapsedTime >= _duration)
        {
            Destroy(gameObject);
        }
    }
}