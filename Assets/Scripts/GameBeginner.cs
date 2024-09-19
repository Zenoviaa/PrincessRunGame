using System.Collections;
using UnityEngine;
public class GameBeginner : MonoBehaviour
{

    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.StartLevel();
    }
}