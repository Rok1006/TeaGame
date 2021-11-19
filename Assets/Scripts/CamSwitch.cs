using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject StartCam;
    public GameObject TeaCam;
    public GameObject ConversationCam;
    void Start()
    {
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Q)){
            StartCamOn();
        }else if(Input.GetKey(KeyCode.W)){
            TeaCamOn();
        }else if(Input.GetKey(KeyCode.E)){
            ConversationCamOn();
        }
    }

    public void StartCamOn(){  //when player first start the game
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
    }
    public void TeaCamOn(){  //When player is making tea
        StartCam.SetActive(false);
        TeaCam.SetActive(true); 
        ConversationCam.SetActive(false);
    }
    public void ConversationCamOn(){ //when player have conversation with customers
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(true);
    }
}
