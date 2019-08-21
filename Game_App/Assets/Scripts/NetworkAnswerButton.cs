using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NetworkAnswerButton : MonoBehaviour
{
    public Text answerText;
    private AnswerData answerData;
    // Start is called before the first frame update
    private MatchController matchController;

    void Start()
    {
        matchController = FindObjectOfType<MatchController>();
    }

    public void SetUp(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }
    
    public void HandleClick(){

        matchController.AnswerButtonClicked (answerData.isCorrect);

    }
    

}
