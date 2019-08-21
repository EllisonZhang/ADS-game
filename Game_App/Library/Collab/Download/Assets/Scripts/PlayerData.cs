using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour 
{

    public static PlayerData playerData;
    public int highestScore;
    public string userName;
    public int rankLevel;
    public string levelInText;
    public int money;
    public int questionLevel;

    public string rawData;
    public string[] managedData;


    private void Awake() {
        
        if(playerData==null){
            PlayerData.playerData = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable() {

        // LoadPlayerData();
    }
    private void Start() {
        
        // SceneManager.LoadScene("Menu");
    }

    public void AssignPlayerData(){
        managedData = rawData.Split('@');
        userName = managedData[0];
        rankLevel = int.Parse(managedData[1]);
        money =  int.Parse(managedData[2]);
    }

    public void SetUpPlayerPrefs(string userName,int rankLevel, int money,int questionLevel){
        PlayerPrefs.SetString("userName",userName);
        PlayerPrefs.SetInt("rankLevel",rankLevel);
        PlayerPrefs.SetInt("money",money);
        PlayerPrefs.SetInt("questionLevel",questionLevel);
    }

    public void QuestionLevelGenerate(){

        if(rankLevel<2){
            questionLevel = 1;
            levelInText = "Iron";

        }else if(rankLevel>1&&rankLevel<4){
            questionLevel = 2;
            levelInText = "Bronze";
        }else if(rankLevel>3&&rankLevel<7){
            questionLevel = 3;
            levelInText = "Silver";
        }else if(rankLevel>6&&rankLevel<11){
            questionLevel = 4;
            levelInText = "Gold";
        }else if(rankLevel>10&&rankLevel<16){
            questionLevel = 5;
            levelInText = "Platinum";
        }else if(rankLevel>15&&rankLevel<22){
            questionLevel = 6;
            levelInText = "Diamond";
        }else if(rankLevel>21&&rankLevel<29){
            questionLevel = 7;
            levelInText = "Master";
        }else if(rankLevel>28&&rankLevel<37){
            questionLevel = 8;
            levelInText = "Grandmaster";
        }else if(rankLevel>36){
            questionLevel = 9;
            levelInText = "King";
        }

        SetUpPlayerPrefs(this.userName,this.rankLevel,this.money,this.questionLevel);
    }

}
