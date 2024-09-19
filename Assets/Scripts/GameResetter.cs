using System.Collections;
using UnityEngine;

public class GameResetter : MonoBehaviour
{

    // Use this for initialization
    private void Start()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.ResetDefaults();
    }


}