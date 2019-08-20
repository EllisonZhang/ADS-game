using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController myCC;

    public float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        myCC = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine){
            BasicMovemnet();
        }
    }

    void BasicMovemnet(){

        if(Input.GetKey(KeyCode.W)){
            myCC.Move(transform.forward*Time.deltaTime*movementSpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            myCC.Move(-transform.right*Time.deltaTime*movementSpeed);
        }
        if(Input.GetKey(KeyCode.S)){
            myCC.Move(-transform.forward*Time.deltaTime*movementSpeed);
        }
        if(Input.GetKey(KeyCode.D)){
            myCC.Move(transform.forward*Time.deltaTime*movementSpeed);
        }
    }
}
