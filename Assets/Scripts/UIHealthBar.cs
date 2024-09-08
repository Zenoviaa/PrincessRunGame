using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("UI")]
    [SerializeField] private Image[] _healthPips;
    [SerializeField] private Sprite _healthPipEmpty;
    [SerializeField] private Sprite _healthPipFilled;

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float health = _player.health;
        for(int i = 0; i < _healthPips.Length;i++)
        {
            if(i < health)
            {
                _healthPips[i].sprite = _healthPipFilled;
            } 
            else
            {
                _healthPips[i].sprite = _healthPipEmpty;
            }
        }
    }
}