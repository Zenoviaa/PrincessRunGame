using System.Collections;
using UnityEngine;

public class ExpandingSquareManager : MonoBehaviour
{
    private enum State
    {
        Part_One,
        Part_Two,
        Part_Three
    }

    private State _state;
    private float _squareTimer;
    private int _xIndex;
    private int _yIndex;
    [SerializeField] private float _timeBetween;
    [SerializeField] private float _maxXIndex;
    [SerializeField] private float _maxYIndex;
    [SerializeField] private float _offset;
    [SerializeField] private ExpandingSquare _expandingSquarePrefab;

    [SerializeField] private Color[] _colors;

    private void Update()
    {
        _squareTimer += Time.deltaTime;
   
        Color color = _colors[(int)_state];
        if (_squareTimer >= _timeBetween)
        {
            Vector3 spawnPos = new Vector3(_xIndex * _offset, _yIndex * _offset,0);
            spawnPos += transform.position;
            var instance = Instantiate(_expandingSquarePrefab, spawnPos, _expandingSquarePrefab.transform.rotation);
            instance.spriteRenderer.color = color;
            _xIndex++;
            if(_xIndex >= _maxXIndex)
            {
                _xIndex = 0;
                _yIndex++;
                if(_yIndex >= _maxYIndex)
                {
                    switch (_state)
                    {
                        default:
                        case State.Part_One:
                            SwitchState(State.Part_Two);
                            break;
                        case State.Part_Two:
                            SwitchState(State.Part_Three);
                            break;
                        case State.Part_Three:
                            SwitchState(State.Part_One);
                            break;
                    }

                    _yIndex = 0;
                }
            }
            _squareTimer = 0;
        }
    }


    private void SwitchState(State state)
    {
        _state = state;
    }
}