using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WebRequest : MonoBehaviour
{

    public string rawGameData;
    private RoundData currentRoundData;
    public QuestionData[] questionDatas;
    // Start is called before the first frame update
    void Start()
    {
        // A correct website page.
        // StartCoroutine(GetRequest("http://localhost/UnityBackEnd/GetData.php"));
        // A non-existing page.
        // StartCoroutine(GetUsers("http://localhost/UnityBackEnd/GetData.php"));
        // StartCoroutine(Login("Ellison","zwk!19951115"));
        //  StartCoroutine(GetQuestions(1));
        //  StartCoroutine(UpdateUser("Ellison",10,10));
        // StartCoroutine(RegisterUser("Kevin","kevin"));
        
    }

    public IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetUsers(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //login successful
                if(www.downloadHandler.text.Equals("Login Success")){
                    //if user exists then loading user data
                    Main.instance.hasUser = true;
                    StartCoroutine(PlayerDataRetrieve(username,password));
                    // SceneManager.LoadScene("LoadingScene");
                    Debug.Log(www.downloadHandler.text);

                }
                
            }
        }
    }

    public IEnumerator PlayerDataRetrieve(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/PlayerDataRetrieve.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //login successful   
                PlayerData.playerData.rawData = www.downloadHandler.text;
                PlayerData.playerData.AssignPlayerData();
                PlayerData.playerData.QuestionLevelGenerate();
                SceneManager.LoadScene("LoadingScene");
                // Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPassword", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/Register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator UpdateUser(string username, int rankLevel, int money)
    {
        //we can use this function to updata user data
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("rankLevel", rankLevel);
        form.AddField("money", money);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/UpdateUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetQuestions(int questionLevel)
    {

        // we only get questions based on question level,
        // we need to get 5 random questions! this function not finished.
        WWWForm form = new WWWForm();
        form.AddField("questionLevel", questionLevel);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackEnd/GetQuestions.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Debug.Log(www.downloadHandler.text);
                rawGameData = www.downloadHandler.text;  
                LoadQuestionFromDatabase();  
            }
        }
    }



    private RoundData LoadQuestionFromDatabase(){

        currentRoundData = new RoundData();
        questionDatas = new QuestionData[5];

        for(int j =0;j<5;j++){
            questionDatas[j] = new QuestionData();
        }
        for(int j =0;j<5;j++){
            questionDatas[j].answers = new AnswerData[4];
            for(int i=0;i<4;i++){
                questionDatas[j].answers[i] = new AnswerData();
            }
            
        }

        string[] rawData = rawGameData.Split('@');

        currentRoundData.pointAddedForCorrectAnswer = 10;
        currentRoundData.timeLimitInSeconds = 10;
        currentRoundData.questions = questionDatas;


        for(int j =0;j<5;j++){

            for(int i=0;i<6;i++){
                // Debug.Log(rawData[i+6*j]);
                if(i==0){
                    questionDatas[j].questionText =  rawData[i+6*j];
                    // Debug.Log(questionDatas[j].questionText);
                }else if(i<5&&i>0){
                    questionDatas[j].answers[i-1].answerText = rawData[i+6*j];
                    questionDatas[j].answers[i-1].isCorrect = false;
                }else if(i == 5 ){
                    int rightAnswer = int.Parse(rawData[i+6*j]);
                    questionDatas[j].answers[rightAnswer-1].isCorrect = true;
                }
                     
            }
        }
    DataController.dataController.allRoundData = currentRoundData;
    return currentRoundData;
            
    }




}
