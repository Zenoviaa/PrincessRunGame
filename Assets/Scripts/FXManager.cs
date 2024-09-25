using System.Collections;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private AudioSource _musicFadeSource;
    private AudioSource _musicSource;
    private AudioSource[] _audioSources;

    [SerializeField] private float _maxPitchVariation = 0.05f;
    [SerializeField] private EffectSpriteFlash _spriteFlashPrefab;

    private void Awake()
    {
        if (Instance == this)
            return;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _audioSources = new AudioSource[20];
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicFadeSource = gameObject.AddComponent<AudioSource>();
        _musicFadeSource.loop = true;
        for (int i = 0; i < _audioSources.Length; i++)
        {
            _audioSources[i] = gameObject.AddComponent<AudioSource>();
        }

        DontDestroyOnLoad(gameObject);
    }

    public static FXManager Instance { get; private set; }

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        AudioSource targetSource = null;
        for (int i = 0; i < _audioSources.Length; i++)
        {
            AudioSource audioSource = _audioSources[i];
            if (audioSource.clip == audioClip)
            {
                targetSource = audioSource;
                break;
            }

            if (audioSource.isPlaying)
                continue;
            targetSource = audioSource;
        }

        float oldestTime = float.MinValue;
        if (targetSource == null)
        {
            //Find oldest audio source if ran out of sources.
            for (int i = 0; i < _audioSources.Length; i++)
            {
                AudioSource audioSource = _audioSources[i];
                if (audioSource.time > oldestTime)
                    continue;
                oldestTime = audioSource.time;
                targetSource = audioSource;
            }
        }

        targetSource.clip = audioClip;
        targetSource.pitch = 1f + UnityEngine.Random.Range(-_maxPitchVariation, _maxPitchVariation);
        targetSource.Play();
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if (_musicSource.clip != audioClip)
        {
            StartCoroutine(FadeInMusic(audioClip));
        }
    }

    private IEnumerator FadeInMusic(AudioClip audioClip)
    {
        float t = 0f;
        float duration = 1.0f;
        _musicSource.volume = 1f;
        _musicFadeSource.volume = 0f;
        _musicFadeSource.clip = audioClip;
        _musicFadeSource.Play();
        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime;
            float p = t / duration;
            _musicSource.volume = Mathf.Lerp(1f, 0f, p);
            _musicFadeSource.volume = Mathf.Lerp(0f, 1f, p);
            yield return null;
        }

        _musicSource.clip = _musicFadeSource.clip;
        _musicSource.time = _musicFadeSource.time;
        _musicSource.volume = 1f;
        _musicFadeSource.volume = 0f;
        _musicSource.Play();
    }

    public void Screenshake(int pixelStrength, float duration)
    {
        CameraFollow cameraFollow = CameraFollow.Instance;
        cameraFollow.Screenshake(pixelStrength, duration);
    }
    public void SpriteFlash(SpriteRenderer target)
    {
        EffectSpriteFlash spriteFlash = Instantiate(_spriteFlashPrefab);
        spriteFlash.Target = target;
    }

}