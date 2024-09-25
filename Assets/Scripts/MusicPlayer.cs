using System.Collections;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;
    private void Start()
    {
        FXManager fXManager = FXManager.Instance;
        fXManager.PlayMusic(_musicClip);
    }
}