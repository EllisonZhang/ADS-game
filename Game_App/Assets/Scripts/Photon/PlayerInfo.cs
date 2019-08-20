using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // Start is called before the first frame update
    public static PlayerInfo playerInfo;
    public int currentScore;

    public GameObject[] allCharacters;

    private void OnEnable() {

        if(PlayerInfo.playerInfo == null){
            PlayerInfo.playerInfo = this;
        }
        else{
            if(PlayerInfo.playerInfo!= this){
                Destroy(PlayerInfo.playerInfo.gameObject);
                PlayerInfo.playerInfo = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);

    }
    void Start()
    {
        if(PlayerPrefs.HasKey("MyScore")){
            currentScore = PlayerPrefs.GetInt("MyScore");
        }
        else{
            currentScore = 0;
            PlayerPrefs.SetInt("MyScore",currentScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
