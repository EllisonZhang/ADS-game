using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks 
{
    public static PhotonRoom room;
    private PhotonView PV;
    public int multiplayerScene;

    public bool isGameLoaded;
    public int currentScene;
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;
    public int playersInGame;
    // Start is called before the first frame update
    private void Awake() {
        
        if(PhotonRoom.room == null){
            PhotonRoom.room = this;
        }else{

            if(room != this ){
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
    }

    public override void OnEnable(){

        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable(){

        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom(){
        base.OnJoinedRoom();
        Debug.Log("room joined");
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        PhotonNetwork.NickName = myNumberInRoom.ToString();
        // PhotonNetwork.LoadLevel("Loading Screen");    
        
    }


    public void StartGame(){

        if(!PhotonNetwork.IsMasterClient){
            return;
        }

        // PhotonNetwork.LoadLevel(multiplayerScene);
        PhotonNetwork.LoadLevel(4);
    }
    public override void OnPlayerEnteredRoom(Player otherPlayer){
        Debug.Log("PLayer entered room");

        if(PhotonNetwork.IsMasterClient){
        Debug.Log("PLayer entered MasterClient room");
        StartGame();
        }
    }
    void OnSceneFinishedLoading(Scene scene,LoadSceneMode mode){

        currentScene = scene.buildIndex;
        if(currentScene == multiplayerScene){
            CreatePlayer();
            
        }
    }

    void CreatePlayer(){

        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PhotonNetWorkPlayer"),
            transform.position,Quaternion.identity,0);
    }

    
}





