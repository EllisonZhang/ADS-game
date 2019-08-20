using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{

    public InputField userNameInput;
    public InputField userPasswordInput;
    public Button LoginButton;

    public GameObject createAccountPage;
    public GameObject logInPage; 
    // Start is called before the first frame update
    void Start()
    {

        // LoginButton.onClick.AddListener(() => {
        //     StartCoroutine(Main.instance.webRequest.Login(userNameInput.text,userPasswordInput.text));
        // });
    }

    public void LoginButtonClicked(){

        if(InputIsLegal()){
            StartCoroutine(Main.instance.webRequest.Login(userNameInput.text,userPasswordInput.text));
        // loading scene in web.Login function
        }
        
    }

    public void CreateAccountButtonClicked(){

        logInPage.SetActive(false);
        createAccountPage.SetActive(true);
    }

    // Update is called once per frame
    public bool InputIsLegal(){
    // input check
        if((userNameInput.text!="")&&(userPasswordInput.text!="")){
            return true;
        }
    return false;
    }

    public void QuitGame(){

        Application.Quit();
    }

    
}

