using System.Collections.Generic;
using UnityEngine;

public class UISquareTransition : MonoBehaviour
    {
        private List<UISquare> _squares = new();
        private float _time;
        private float _delay;
        private bool _transitioning;
        private bool _killing;
        private string _sceneToLoad;
        [SerializeField] private UISquare _uiSquarePrefab;
        [SerializeField] private RectTransform _squareContent;
        [SerializeField] private float _transitionWidth;
        [SerializeField] private float _transitionHeight;
        [SerializeField] private float _squareSize;
        [SerializeField] private float _delayBetween;
        [SerializeField] private float _duration;
        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void Update()
        {

            if (_killing)
            {
                _time += Time.deltaTime;
                if (_time >= _duration)
                {
                    Destroy(gameObject);
                }
            }
            else if (_transitioning)
            {
                _time += Time.deltaTime;
                if (_time >= _duration)
                {
                    _transitioning = false;
                    SceneLoader.Main.LoadScene(_sceneToLoad, useTransition: false, onFinish: Out);
                }
            }
        }

        private void Out()
        {
            _time = 0;
            _killing = true;
            foreach (UISquare square in _squares)
            {
                square.Out();
            }
        }

        public void LoadScene(string scene)
        {
            _sceneToLoad = scene;
            _transitioning = true;
            for (float w = 0; w < _transitionWidth; w += _squareSize)
            {
                for (float h = 0; h < _transitionHeight; h += _squareSize)
                {
                    UISquare uiSquare = Instantiate(_uiSquarePrefab, _squareContent, false);
                    uiSquare.delay = _delay;
                    _delay += _delayBetween;
                    _squares.Add(uiSquare);
                }
            }
        }
    }
