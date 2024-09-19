using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private bool _hasStarted;
    public string sceneToLoad = "Level1";
    private void Update()
    {
        if (!_hasStarted && Input.GetKeyDown(KeyCode.Space))
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.LoadSceneWithTransition(sceneToLoad);
            _hasStarted = true;
        }
    }
}