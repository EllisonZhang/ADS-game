using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    public AudioSource buttonAudio;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void onHover(){
        buttonAudio.PlayOneShot(hoverSound);
    }

    public void onClick(){
        buttonAudio.PlayOneShot(clickSound);
    }
}
