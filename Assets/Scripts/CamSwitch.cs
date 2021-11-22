using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject StartCam;
    public GameObject TeaCam;
    public GameObject ConversationCam;
    public GameObject ChoiceCam;  //pick ingredients
    public GameObject drawer;
    Animator drawerAnim;
    void Start()
    {
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(false);
        drawerAnim = drawer.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            StartCamOn();
        }else if(Input.GetKeyDown(KeyCode.Alpha2)){
            TeaCamOn();
            //drawerAnim.SetTrigger("in");
        }else if(Input.GetKeyDown(KeyCode.Alpha3)){
            ConversationCamOn();
        }else if(Input.GetKeyDown(KeyCode.Alpha4)){
            ChoiceCamOn();
        }
        if(ChoiceCam.activeSelf){

        }else{
            //drawerAnim.SetTrigger("in");
        }
    }

    public void StartCamOn(){  //when player first start the game
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(false);
    }
    public void TeaCamOn(){  //When player is making tea
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(true); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(false);
    }
    public void ConversationCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(true);
    }
    public void ChoiceCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("Out",true);
        drawerAnim.SetBool("In",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(true);
    }
}
