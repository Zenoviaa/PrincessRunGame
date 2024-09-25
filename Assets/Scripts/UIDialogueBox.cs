using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIDialogueBox : MonoBehaviour
{
    private float _time;
    private float _typewriterTime;
    private int _typewriterIndex;
    private State _state;
    private enum State
    {
        In,
        Idle,
        Out
    }

    private AudioClip _talkSound;
    private Vector3 _startSwoopPos;
    private Vector3 _endSwoopPos;
    private Action _onFinishCallback;
    [SerializeField] private RectTransform _fullDialogueTransform;
    [SerializeField] private Image _bustImage;
    [SerializeField] private TMP_Text _dialogueTMP;

    [Header("Fly In Animation")]
    [SerializeField] private Vector3 _swoopOffset;
    [SerializeField] private float _swoopDuration;

    public float timeBetweenTypes;

    public bool isFinished;
    private void Start()
    {
        _startSwoopPos = _startSwoopPos + _swoopOffset;
        _endSwoopPos = _fullDialogueTransform.localPosition;
        _fullDialogueTransform.localPosition = _startSwoopPos;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.In:
                AI_In();
                break;
            case State.Idle:
                AI_Idle();
                break;
            case State.Out:
                AI_Out();
                break;
        }
    }

    private void AI_In()
    {
        _time += Time.deltaTime;
        float p = _time / _swoopDuration;
        float ep = Easing.InOutCubic(p);
        Vector3 swoopPos = Vector3.Lerp(_startSwoopPos, _endSwoopPos, ep);
        _fullDialogueTransform.localPosition = swoopPos;
        if(_time >= _swoopDuration)
        {
            _state = State.Idle;
            _time = 0;
        }
    }

    private void AI_Idle()
    {
        _typewriterTime += Time.deltaTime;
        if(_typewriterTime >= timeBetweenTypes)
        {
            _typewriterIndex++;
            _typewriterTime = 0;
            if(_typewriterIndex >= _dialogueTMP.text.Length)
            {
                isFinished = true;
            }
            else
            {
                FXManager fXManager = FXManager.Instance;
                fXManager.PlaySound(_talkSound);
            }
     
        }

        _dialogueTMP.maxVisibleCharacters = _typewriterIndex;
    }

    private void AI_Out()
    {
        _time += Time.deltaTime;
        float p = _time / _swoopDuration;
        float ep = Easing.InOutCubic(p);
        Vector3 swoopPos = Vector3.Lerp(_endSwoopPos, _startSwoopPos, ep);
        _fullDialogueTransform.localPosition = swoopPos;
        if (_time >= _swoopDuration)
        {
            _state = State.Idle;
            _time = 0;
            if(_onFinishCallback != null)
                _onFinishCallback.Invoke();
            Destroy(gameObject);
        }
    }

    public void Exit(Action onFinishCallback)
    {
        _state = State.Out;
        _onFinishCallback = onFinishCallback;
    }

    public void Talk(Dialogue dialogue)
    {
        timeBetweenTypes = dialogue.timeBetweenTexts;
        _talkSound = dialogue.talkSound;
        _bustImage.sprite = dialogue.bust;
        _typewriterTime = 0;
        _typewriterIndex = 0;
        _dialogueTMP.maxVisibleCharacters = 0;
        _dialogueTMP.text = dialogue.text;
    }

    public void Finish()
    {
        isFinished = true;
        _typewriterIndex = _dialogueTMP.text.Length;
    }
}