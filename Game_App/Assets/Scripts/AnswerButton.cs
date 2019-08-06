using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public Text answerText;
    private AnswerData answerData;
    // Start is called before the first frame update
    private GameController gameController;
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    public void SetUp(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }
    
    public void HandleClick(){

        gameController.AnswerButtonClicked (answerData.isCorrect);

    }

}
