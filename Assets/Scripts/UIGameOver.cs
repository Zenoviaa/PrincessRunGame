using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    private bool _input;
    private float _time;
    private Vector3 _startLocal;
    private Vector3 _endLocal;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _startOffset;

    [SerializeField] private AudioClip _continueSound;

    public string sceneToLoad = "MainMenu";
    public void RestartLevel()
    {
        FXManager fXManager = FXManager.Instance;
        fXManager.PlaySound(_continueSound);
        GameManager gameManager = GameManager.Instance;
        gameManager.LoadSceneWithTransition(sceneToLoad);
    }

    private void Start()
    {
        _startLocal = _rectTransform.localPosition + _startOffset;
        _endLocal = _rectTransform.localPosition;
        _rectTransform.localPosition = _startLocal;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        float progress = _time / _duration;
        float ep = Easing.InOutCubic(progress);
        Vector3 localPos = Vector3.Lerp(_startLocal, _endLocal, ep);
        _rectTransform.localPosition = localPos;
        if (Input.GetButtonDown("Jump") && !_input)
        {
            RestartLevel();
            _input = true;
        }
    }
}