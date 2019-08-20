using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using Photon.Pun;

[System.Serializable]
public class DataController : MonoBehaviour
{
    public RoundData allRoundData;
    private int questionLevel = 1;
    // private string gameDataFileName = "data.json";
    // Start is called before the first frame update
    public static DataController dataController;

    private void Awake() {
        
        if(dataController==null){
            DataController.dataController = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        LoadGameData();
        // LoadPlayerProgress();    
        SceneManager.LoadScene("Menu");    
    }

    public RoundData GetCurrentRoundData(){
        return this.allRoundData;
    }

    
    public void LoadPlayerProgress(){
        
        if(PlayerPrefs.HasKey("highestScore")){
            PlayerData.playerData.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress(){
        PlayerPrefs.SetInt ("highestScore", PlayerData.playerData.highestScore);
    }

    public void SubmitNewPlayerScore(int newScore){

        if(newScore>PlayerPrefs.GetInt("highestScore")){
            PlayerData.playerData.highestScore = newScore;
            SavePlayerProgress();
        }
    }
    public int GetHighestPlayerScore(){
        return PlayerData.playerData.highestScore;
    }

    private void LoadGameData(){
        questionLevel = PlayerData.playerData.questionLevel;
        StartCoroutine(Main.instance.webRequest.GetQuestions(questionLevel));
        
    }  

            // string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        // if(File.Exists(filePath)){ 
            
        //     // read and store the json serialized content in "dataAsJson"
        //     string dataAsJson = File.ReadAllText(filePath);

        //     // deserialization the Json to GameData Object "loadedData"
        //     GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

        //     allRoundData = loadedData.allRoundData;        
        
        // } else{
        //     Debug.LogError("Cannot load game data!");
        // }
    
}
