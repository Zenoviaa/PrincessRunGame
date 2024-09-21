using System.Collections;
using UnityEngine;

public class UILevelCompleteDialogue : MonoBehaviour
{
    public UIDialogueMaker dialogueBoxMaker;
    public static UILevelCompleteDialogue Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}