using UnityEngine;

public class UIYouWin : MonoBehaviour
{
    private float _timer;
    private Vector3 _startLocalPos;
    private Vector3 _endLocalPos;

    private Vector3 _startTextLocalPos;
    private Vector3 _endTextLocalPos;

    [SerializeField] private Transform _tweenTransform;
    [SerializeField] private float _tweenDuration = 2f;
    [SerializeField] private Vector3 _tweenLocalOffset;


    [SerializeField] private Transform _tweenTextTransform;
    [SerializeField] private float _tweenTextDuration = 2f;
    [SerializeField] private Vector3 _tweenTextLocalOffset;

    private void Start()
    {
        _endLocalPos = _tweenTransform.localPosition;
        _startLocalPos = _endLocalPos + _tweenLocalOffset;
        _tweenTransform.localPosition = _startLocalPos;


        _endTextLocalPos = _tweenTextTransform.localPosition;
        _startTextLocalPos = _endTextLocalPos + _tweenTextLocalOffset;
        _tweenTextTransform.localPosition = _startTextLocalPos;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float p = _timer / _tweenDuration;
        float ep = Easing.InOutCubic(p);
        _tweenTransform.localPosition = Vector3.Lerp(_startLocalPos, _endLocalPos, ep);


        p = _timer / _tweenTextDuration;
        ep = Easing.InOutCubic(p);
        _tweenTextTransform.localPosition = Vector3.Lerp(_startTextLocalPos, _endTextLocalPos, ep);
    }
}