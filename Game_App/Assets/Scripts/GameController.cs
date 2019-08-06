using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;
    private bool isRoundActivate;
    private float timeRemaining;
    private int questionIndex;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private int playerScore;
    public SimpleObjectPool answerButtonGameObjectPool;
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;


    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;

        timeRemaining = currentRoundData.timeLimitInSeconds;

        playerScore = 0;
        questionIndex = 0;

        ShowQuestion();
        isRoundActivate = true;

    }

    private void ShowQuestion(){

        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for(int i=0;i<questionData.answers.Length;i++){

            GameObject answerButtonGameObject = answerButtonGameObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.SetUp(questionData.answers[i]);
        }
    }

    private void RemoveAnswerButtons(){

        while(answerButtonGameObjects.Count>0){
            answerButtonGameObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect){

        if(isCorrect){
            playerScore += currentRoundData.pointAddedForCorrectAnswer;
            scoreDisplayText.text = "Score" + playerScore.ToString();
        }
        if(questionPool.Length>questionIndex+1){
            questionIndex++;
            ShowQuestion();
        }else{
            EndRound();
        }
    }
    public void EndRound(){
        isRoundActivate = false;

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
