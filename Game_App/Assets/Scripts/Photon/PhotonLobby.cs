using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public GameObject battleButton; // Start searching room after clicking 
    public GameObject cancelButton; // Cancel the searching
    private void Awake(){
        lobby = this;    //Creating a lobby using Singleton design pattern.
    }
    // Start is called before the first frame update
    void Start()
    {

        PhotonNetwork.ConnectUsingSettings(); // connect to Master photon server
        
    }

    public override void OnConnectedToMaster(){

            Debug.Log("Player has connected to Photon server");
            battleButton.SetActive(true);       
    }

    public void onBattleButtonClicked(){

        Debug.Log("Start searchong a room");
        battleButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.Log("no room available, trying to created a new room");
        CreateRoom();
    }

    public void CreateRoom(){
        int randomRoomName = Random.Range(0,10000);
        RoomOptions roomOptions = new RoomOptions(){
            IsVisible = true, IsOpen = true, MaxPlayers = 10
        };

        PhotonNetwork.CreateRoom("Room"+randomRoomName,roomOptions);
        Debug.Log("creating new room");
    }
    public override void OnCreateRoomFailed(short returnCode, string message){

        Debug.Log("already has a room with same id");
        CreateRoom();
    }

    public void OnCancelButtonClicked(){
        battleButton.SetActive(true);
        cancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();  

    }

    //This function is called when player successfully joined the room
    public override void OnJoinedRoom(){
        
        Debug.Log("room joined");
    }

    public override void OnLeftRoom(){
        Debug.Log("room left");
    }

}
