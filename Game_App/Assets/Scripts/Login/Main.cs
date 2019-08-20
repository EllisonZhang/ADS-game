using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    public static Main instance;
    public WebRequest webRequest;

    public bool hasUser = false; // use to check if the user exists after clik login button
    public bool newUserCreated = false; // use to check if new account create successfully
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        webRequest = GetComponent<WebRequest>();
        DontDestroyOnLoad(this.gameObject);  //Main won't be destroyed. so we can always use web function from Main.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
