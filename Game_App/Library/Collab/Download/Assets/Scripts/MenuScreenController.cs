using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class MenuScreenController : MonoBehaviour
{
    public Text userNameText;
    public Text moneyText;
    public Text rankText;
    // Start is called before the first frame update

    private void Start() {   
        SetUserName();
        SetUserMoney();
        SetUserProfile();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetUserName(){

        userNameText.text = PlayerData.playerData.userName;

    }
    public void SetUserMoney(){
        moneyText.text = PlayerData.playerData.money.ToString();
    }

    public void SetUserProfile(){
        rankText.text = PlayerData.playerData.levelInText;
    }

 
}
