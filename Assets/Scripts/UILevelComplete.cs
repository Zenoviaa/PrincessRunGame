using UnityEngine;

internal class UILevelComplete : MonoBehaviour
{
    public void NextLevel()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.NextLevel();
    }
}
