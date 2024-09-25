using System;
using UnityEngine;

public class UIDialogueMaker : MonoBehaviour
{
    private bool _dialoging;
    private int _dialogueIndex;
    private UIDialogueBox _instance;
    private Action _onFinishCallback;
    [SerializeField] private UIDialogueBox _dialogueBox;
    [SerializeField] private Dialogue[] _dialogue;
    [SerializeField] private AudioClip _continueSound;
    private void Update()
    {
        if (_dialoging && Input.GetButtonDown("Jump"))
        {
            Continue();
        }
    }

    public void StartDialogue()
    {
        _dialoging = true;
        if(_instance != null)
            _instance.Exit(_onFinishCallback);
        _instance = Instantiate(_dialogueBox);

        Dialogue dialogue = _dialogue[_dialogueIndex];
        _instance.Talk(dialogue);
    }

    public void StartDialogue(Action onFinishCallback)
    {
        _onFinishCallback = onFinishCallback;
        _dialoging = true;
        if (_instance != null)
            _instance.Exit(_onFinishCallback);
        _instance = Instantiate(_dialogueBox);

        Dialogue dialogue = _dialogue[_dialogueIndex];
        _instance.Talk(dialogue);
    }


    private void Continue()
    {

        if (_instance.isFinished)
        {
            _dialogueIndex++;
            if (_dialogue.Length > _dialogueIndex)
            {
                Dialogue dialogue = _dialogue[_dialogueIndex];
                _instance.Talk(dialogue);
                FXManager fXManager = FXManager.Instance;
                fXManager.PlaySound(_continueSound);
            }
            else
            {

                _instance.Exit(_onFinishCallback);
            }
        }
        else
        {
            _instance.Finish();
        }

    }
}