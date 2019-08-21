using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MatchUIController : MonoBehaviour
{
    private PhotonView photonView;

    public Text[] NetworkScoreDisplayText;

    public Text[] NetworkNameDisplayText;
    public Hashtable scoreTable = new  Hashtable();

    public static MatchUIController matchUIController;

    private  bool isWinner = true;

    private void Awake() {
        //set this controller to be singleton    
        if(matchUIController == null){
            MatchUIController.matchUIController = this;
        }else{
            if(matchUIController != this ){
                Destroy(MatchUIController.matchUIController.gameObject);
                MatchUIController.matchUIController = this;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("myScore",0);
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine){

            scoreTable.Add("myScore",PlayerPrefs.GetInt("myScore"));

        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUserScore();
    }

    public void UpdateUserScore(){
        scoreTable = new Hashtable();
        scoreTable.Add("myScore",PlayerPrefs.GetInt("myScore"));
        scoreTable.Add("userName",PlayerPrefs.GetString("userName"));
        // Player.SetCustomProperties(scoreTable);
        PhotonNetwork.LocalPlayer.SetCustomProperties(scoreTable);
        // Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["myScore"]);
        foreach (Player p in PhotonNetwork.PlayerList)
        {   
            // Debug.Log(p.NickName);
            NetworkScoreDisplayText[p.ActorNumber-1].text = p.CustomProperties["myScore"].ToString();
            NetworkNameDisplayText[p.ActorNumber-1].text = p.CustomProperties["userName"].ToString();
        }
    }

    public bool IsWinner(){

        foreach (Player p in PhotonNetwork.PlayerList){   

            if(PlayerPrefs.GetInt("myScore") < (int)p.CustomProperties["myScore"]){
                return false;
            }
        }
        return true;
    }
}
