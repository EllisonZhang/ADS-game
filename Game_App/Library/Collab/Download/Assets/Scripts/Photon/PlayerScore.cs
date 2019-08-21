using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerScore : MonoBehaviour
{
    private PhotonView photonView;

    public Text[] NetworkScoreDisplayText;
    public Hashtable scoreTable = new  Hashtable();
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
        scoreTable = new Hashtable();
        scoreTable.Add("myScore",PlayerPrefs.GetInt("myScore"));
        PhotonNetwork.LocalPlayer.SetCustomProperties(scoreTable);
        // Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["myScore"]);
        foreach (Player p in PhotonNetwork.PlayerList)
        {   
            // Debug.Log(p.NickName);
            NetworkScoreDisplayText[p.ActorNumber-1].text = ": "+ p.CustomProperties["myScore"].ToString();
        }
    }
}
