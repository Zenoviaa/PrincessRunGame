using TMPro;
using UnityEngine;

internal class UIShardCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _shardTmp;
    private void Update()
    {
        _shardTmp.text = $"{_player.coins}";
    }
}