using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    private float _time;
    private Vector3 _startLocal;
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private Vector3 _endOffset;
    [SerializeField] private float _speed;
    private void Start()
    {
        _startLocal = _transform.localPosition;
    }
    private void Update()
    {
        _time += Time.deltaTime * _speed;
        float p = Mathf.PingPong(_time, 1);
        float ep = Easing.InOutCubic(p);
        Vector3 offset = Vector3.Lerp(_startOffset, _endOffset, ep);
        _transform.localPosition = _startLocal + offset;
    }
}
