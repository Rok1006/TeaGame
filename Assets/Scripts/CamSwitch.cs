using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public static CamSwitch Instance;
    public GameObject StartCam;
    public GameObject TeaCam;
    public GameObject ConversationCam;
    public GameObject ChoiceCam;  //pick ingredients
    public GameObject CupboardCam;  //pick ingredients
    public GameObject drawer;
    Animator drawerAnim;

    //Enum for access
    public enum CamState {StartCam, TeaCam, ConvCam, ChoiceCam, BoardCam}
    public CamState camState;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        camState = CamState.StartCam;
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(false);
        CupboardCam.SetActive(false);
        drawerAnim = drawer.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            camState = CamState.StartCam;
            StartCamOn();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            camState = CamState.TeaCam;
            TeaCamOn();
            //drawerAnim.SetTrigger("in");
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            camState = CamState.ConvCam;
            ConversationCamOn();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            camState = CamState.ChoiceCam;
            ChoiceCamOn();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            camState = CamState.BoardCam;
            CupboardCamOn();
        }
        if(ChoiceCam.activeSelf){ //?

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
        CupboardCam.SetActive(false);
    }
    public void ConversationCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(true);
        CupboardCam.SetActive(false);
    }
    public void ChoiceCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("Out",true);
        drawerAnim.SetBool("In",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(true);
        CupboardCam.SetActive(false);
    }
    public void CupboardCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(false);
        CupboardCam.SetActive(true);
    }
}
