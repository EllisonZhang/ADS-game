using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

// IPunObservable makes the GameController observable to all clients,
// we want to sync the time!  
public class MatchController : MonoBehaviourPunCallbacks, IPunObservable
{
    private DataController dataController;
    public RoundData currentRoundData;
    public QuestionData[] questionPool;
    private bool isRoundActivate;
    public float timeRemaining;
    private int questionIndex;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();
    private int playerScore;
    public SimpleObjectPool answerButtonGameObjectPool;
    public Text questionDisplayText;
    public Text timeDisplayText;
    public Text scoreDisplayText;
    public Text highScoreDisplay;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    private PhotonView photonView;

    public GameObject matchUIController;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        dataController = FindObjectOfType<DataController>();
        currentRoundData = dataController.GetCurrentRoundData();
        
        questionPool = currentRoundData.questions;
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdataTimeRemainingDisplay();
        playerScore = 0;
        questionIndex = 0;
        ShowQuestion();
        isRoundActivate = true;
        matchUIController.SetActive(true);
    }

   
    void Update()
    {   

        if(isRoundActivate){
            timeRemaining -= Time.deltaTime;
            UpdataTimeRemainingDisplay();

            if(timeRemaining<= 0f){
            //updata new set of questions every 10s
                if(questionPool.Length>questionIndex+1){
                    questionIndex++;                 
                     ShowQuestion();
                 }else{
                    EndRound();
                        }
            }
        }  

    }
    

    private void ShowQuestion(){    
        // for each question, player has 10s to answer
        timeRemaining = 15;
        RemoveAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText;

        for(int i=0;i<questionData.answers.Length;i++){

            GameObject answerButtonGameObject = answerButtonGameObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            NetworkAnswerButton answerButton = answerButtonGameObject.GetComponent<NetworkAnswerButton>();
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
            //the final question worth double marks
            if(questionPool.Length == questionIndex+1){
                    playerScore += 2*currentRoundData.pointAddedForCorrectAnswer*(int)Mathf.Round (timeRemaining);
                    scoreDisplayText.text = playerScore.ToString();
                 }else{
                    playerScore += currentRoundData.pointAddedForCorrectAnswer*(int)Mathf.Round (timeRemaining);
                    scoreDisplayText.text = playerScore.ToString();
                        }   

            if(PlayerInfo.playerInfo!=null){
               PlayerInfo.playerInfo.currentScore = playerScore;
               PlayerPrefs.SetInt("myScore",playerScore);
            }

            // photonView.RPC("RPC_UpdateUI",RpcTarget.AllBuffered);
        }
        
    }

    // [PunRPC]
    // public void RPC_UpdateUI(){
    //     MatchUIController.matchUIController.UpdateUserScore();
    // }

    public void EndRound(){
        isRoundActivate = false;

        Debug.Log(MatchUIController.matchUIController.IsWinner());
        PlayerDataUpdate();
        DatabaseUpdate();
        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);
    }

    public void PlayerDataUpdate(){

        if(MatchUIController.matchUIController.IsWinner()){

            //if win level up, add money
            PlayerData.playerData.rankLevel += 1;
            PlayerData.playerData.money += 10;
            PlayerData.playerData.QuestionLevelGenerate();
        }else{
            PlayerData.playerData.rankLevel -= 1;
            PlayerData.playerData.money -= 10;
            PlayerData.playerData.QuestionLevelGenerate();
        }   
    }

    public void DatabaseUpdate(){

        string userName = PlayerData.playerData.userName;
        int currentRankLevel = PlayerData.playerData.rankLevel;
        int currentMoney = PlayerData.playerData.money;
        StartCoroutine(Main.instance.webRequest.UpdateUser(userName,currentRankLevel,currentMoney));
    }
    



    public void ReturnToMenu(){
        SceneManager.LoadScene("Menu");
    }

    private void UpdataTimeRemainingDisplay(){
      
        timeDisplayText.text =  Mathf.Round (timeRemaining).ToString();

    }

    public void LoadMenuScene(){
        SceneManager.LoadScene("Menu");
    }

    // interface from IPnunObservable 
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

        if(stream.IsWriting){

            stream.SendNext(timeRemaining);
        }
        else{
            this.timeRemaining = (float)stream.ReceiveNext();
        }

    }
}
