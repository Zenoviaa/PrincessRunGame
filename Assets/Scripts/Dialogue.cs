using System;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string text;
    public Sprite bust;
    public AudioClip talkSound;
    public float timeBetweenTexts = 0.05f;
}
