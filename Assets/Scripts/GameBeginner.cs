using UnityEngine;

public class GameBeginner : MonoBehaviour
{
    [SerializeField] private UIDialogueMaker _dialogueMaker;
    private void Start()
    {
        if(_dialogueMaker != null)
        {
            GameManager gameManager = GameManager.Instance;
            _dialogueMaker.StartDialogue(gameManager.StartLevel);
        }
        else
        {
            GameManager gameManager = GameManager.Instance;
            gameManager.StartLevel();
        }     
    }
}