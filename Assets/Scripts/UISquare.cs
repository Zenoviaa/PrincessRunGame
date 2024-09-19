using UnityEngine;
using UnityEngine.UI;


public class UISquare : MonoBehaviour
    {
        private float _timer;
        private bool _moveOut;
        [SerializeField] private float _easeDuration;
        [SerializeField] private Image _square;

        public bool outIn;
        public float delay;

        private void Start()
        {
            if (outIn)
            {
                _square.transform.localScale = Vector3.one;
            }
            else
            {
                _square.transform.localScale = Vector3.zero;
            }
        }

        private void Update()
        {
            if (outIn)
            {
                if (!_moveOut)
                {
                    _square.transform.localScale = Vector3.one;
                    _timer += Time.deltaTime;
                    if(_timer >= delay)
                    {
                        _timer = 0;
                        _moveOut = true;
                    }
                }
                else
                {
                    _timer += Time.deltaTime;
                    float progress = _timer / _easeDuration;
                    float easedProgress = Easing.InOutCubic(progress);
                    _square.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, easedProgress);
                }
            }
            else
            {
                if (!_moveOut)
                {
                    _square.transform.localScale = Vector3.zero;
                    _timer += Time.deltaTime;
                    if (_timer >= delay)
                    {
                        _timer = 0;
                        _moveOut = true;
                    }
                }
                else
                {
                    _timer += Time.deltaTime;
                    float progress = _timer / _easeDuration;
                    float easedProgress = Easing.InOutCubic(progress);
                    _square.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, easedProgress);
                }
            }
        }

        public void Out()
        {
            outIn = true;
            _timer = 0;
            _moveOut = false;
        }
    }
