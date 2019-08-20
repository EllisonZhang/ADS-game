using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject myAvatar;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine){

            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerAvatar"),
            transform.position,Quaternion.identity,0);

        }
    }

}
