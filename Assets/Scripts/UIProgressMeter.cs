using UnityEngine;
using UnityEngine.UI;

public class UIProgressMeter : MonoBehaviour
{
    private LevelObjectSpawner _levelObjectSpawner;
    [SerializeField] private Slider _progressSlider;
    private void Start()
    {
        _levelObjectSpawner = FindObjectOfType<LevelObjectSpawner>();
    }

    private void Update()
    {
        _progressSlider.value = _levelObjectSpawner.Progress;
    }
}