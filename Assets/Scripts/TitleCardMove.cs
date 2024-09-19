using UnityEngine;

public class TitleCardMove : MonoBehaviour
{
    private float _time;
    private Vector3 _startLocal;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private Vector3 _endOffset;
    [SerializeField] private float _speed;
    private void Start()
    {
        _startLocal = _rectTransform.localPosition;
    }
    private void Update()
    {
        _time += Time.deltaTime * _speed;
        float p = Mathf.PingPong(_time, 1);
        float ep = Easing.InOutCubic(p);
        Vector3 offset = Vector3.Lerp(_startOffset, _endOffset, ep);
        _rectTransform.localPosition = _startLocal + offset;
    }
}
