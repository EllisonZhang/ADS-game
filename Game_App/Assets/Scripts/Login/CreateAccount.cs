using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{

    public InputField userNameInput;
    public InputField userPasswordInput;
    public InputField userPasswordConfirmInput;
    public Button LoginButton;
    public GameObject createAccountPage;
    public GameObject logInPage; 
    // public bool legalInput = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // LoginButton.onClick.AddListener(() => {
        //     StartCoroutine(Main.instance.webRequest.Login(userNameInput.text,userPasswordInput.text));
        // });
    }

    public void CreateAccountButtonClicked(){

        if(InputIsLegal()){
            //if password is same in two input
            StartCoroutine(Main.instance.webRequest.RegisterUser(userNameInput.text,userPasswordInput.text));
            logInPage.SetActive(true);
            createAccountPage.SetActive(false);
        }
        else{
            Debug.Log("please enter the same password");
        }     
    }

    public bool InputIsLegal(){
        if((userNameInput.text!="")&&(userPasswordInput.text!="")&&(userPasswordConfirmInput.text!="")){
            if(userPasswordInput.text.Equals(userPasswordConfirmInput.text)){
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    
}
