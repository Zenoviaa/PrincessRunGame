using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class UILevelComplete : MonoBehaviour
{
    [Serializable]
    private class UIImageTween
    {
        public Image image;

        [HideInInspector]
        public Vector3 startLocal;

        [HideInInspector]
        public Vector3 endLocal;

        public Vector3 offset;
        public float duration;
    }

    private enum State
    {
        In,
        Idle,
        Out,
        Dialogue
    }

    private float _time;
    private State _state;
    private bool _continueToDialogue;
    private float _duration;
    private Player _player;
    [SerializeField] private UIImageTween[] _tweens;
    [SerializeField] private UIDialogueMaker _dialogueBoxMaker;
    [SerializeField] private TMP_Text _crystalBerryTMP;

    public float GetLongestTween()
    {
        float m = 0;
        for(int i = 0;i < _tweens.Length; i++)
        {
            var tween = _tweens[i];
            m = MathF.Max(tween.duration, m);
        }
        return m;
    }

    public void NextLevel()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.NextLevel();
    }

    private void Start()
    {
        for(int i = 0; i < _tweens.Length; i++)
        {
            var tween = _tweens[i];
            tween.startLocal = tween.image.transform.localPosition + tween.offset;
            tween.endLocal = tween.image.transform.localPosition;
        }
        _duration = GetLongestTween();
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
            case State.Dialogue:
                AI_Dialogue();
                break;
        }

        if (Input.GetButtonDown("Jump") && !_continueToDialogue)
        {
            _continueToDialogue = true;
        }
    }

    private void AI_In()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>();
        _crystalBerryTMP.text = _player.coins.ToString();
        _time += Time.deltaTime;
        for(int i = 0; i < _tweens.Length; i++)
        {
            var tween = _tweens[i];
            float p = _time / tween.duration;
            float ep = Easing.InOutCubic(p);
            tween.image.transform.localPosition = Vector3.Lerp(tween.startLocal, tween.endLocal, ep);
        }

        if(_time >= _duration)
        {
            _time = 0;
            _state = State.Idle;
        }
    }

    private void AI_Idle()
    {
        if (_continueToDialogue)
        {
            _time = 0;
            _state = State.Out;
        }
    }

    private void AI_Out()
    {
        _time += Time.deltaTime;
        for (int i = 0; i < _tweens.Length; i++)
        {
            var tween = _tweens[i];
            float p = _time / tween.duration;
            float ep = Easing.InOutCubic(p);
            tween.image.transform.localPosition = Vector3.Lerp(tween.endLocal, tween.startLocal, ep);
        }

        if(_time >= _duration)
        {
            _time = 0;
            _state = State.Dialogue;
            _dialogueBoxMaker.StartDialogue(NextLevel);
        }
    }

    private void AI_Dialogue()
    {

    }
}
