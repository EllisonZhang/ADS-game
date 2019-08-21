using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject myAvatar;
    public GameObject scoreBoard;

    public Text[] NetworkScoreDisplayText;
    public Hashtable scoreTable = new  Hashtable();
    // Start is called before the first frame update
    void Start()
    {   
        
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine){

            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerAvatar"),
            transform.position,Quaternion.identity,0);

            // photonView.RPC("RPC_SetupScoreBoard",RpcTarget.AllBuffered);

            
            scoreTable.Add("myScore",PlayerPrefs.GetInt("myScore"));


        }
    }

    private void Update() {
        // scoreTable = new Hashtable();
        // scoreTable.Add("myScore",PlayerPrefs.GetInt("myScore"));
        // PhotonNetwork.LocalPlayer.SetCustomProperties(scoreTable);
        // Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["myScore"]);
        // foreach (Player p in PhotonNetwork.PlayerList)
        // {
        //     NetworkScoreDisplayText[0].text = p.UserId + p.CustomProperties["myScore"].ToString();
        // }
    }

    [PunRPC]
    public void RPC_SetupScoreBoard(){

        scoreBoard = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerScore"),
        transform.position,Quaternion.identity,0);
        scoreBoard.transform.SetParent(GameSetup.GS.scoreBoardParent);
    }

}
