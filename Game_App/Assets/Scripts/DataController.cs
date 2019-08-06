using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;
    private PlayerData playerData;
    private string gameDataFileName = "data.json";
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad (this);
        LoadGameData();
        LoadPlayerProgress();

        SceneManager.LoadScene("Menu");
       
    }

    public RoundData GetCurrentRoundData(){
        return allRoundData[0];
    }

    
    public void LoadPlayerProgress(){

        playerData = new PlayerData();

        if(PlayerPrefs.HasKey("highestScore")){
            playerData.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress(){
        PlayerPrefs.SetInt ("highestScore", playerData.highestScore);
    }

    public void SubmitNewPlayerScore(int newScore){

        if(newScore>PlayerPrefs.GetInt("highestScore")){
            playerData.highestScore = newScore;
            SavePlayerProgress();
        }
    }
    public int GetHighestPlayerScore(){
        return playerData.highestScore;
    }

    private void LoadGameData(){
        
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if(File.Exists(filePath)){
            
            // read and store the json serialized content in "dataAsJson"
            string dataAsJson = File.ReadAllText(filePath);

            // deserialization the Json to GameData Object "loadedData"
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            allRoundData = loadedData.allRoundData;        
        
        } else{
            Debug.LogError("Cannot load game data!");
        }
    }
    
}
