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
    public GameObject HarvestCam;  //harvest special fruit
    public GameObject drawer;
    Animator drawerAnim;

    //Enum for access
    public enum CamState {StartCam, TeaCam, ConvCam, ChoiceCam, BoardCam, HarvestCam}
    public CamState camState;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NOTOOL;
        camState = CamState.StartCam;
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(false);
        CupboardCam.SetActive(false);
        HarvestCam.SetActive(false);
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
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            camState = CamState.HarvestCam;
            HarvestCamOn();
        }
    }

    public void StartCamOn(){  //when player first start the game
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(true);
        TeaCam.SetActive(false); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(false);
        HarvestCam.SetActive(false);
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NOTOOL;
        TeaCeremonyManager.Instance.toolText.SetActive(false);
    }
    public void TeaCamOn(){  //When player is making tea
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(true); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(false);
        CupboardCam.SetActive(false);
        HarvestCam.SetActive(false);
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
        TeaCeremonyManager.Instance.toolText.SetActive(true);
    }
    public void ConversationCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(true);
        CupboardCam.SetActive(false);
        HarvestCam.SetActive(false);
        TeaCeremonyManager.Instance.toolText.SetActive(false);
        camState = CamState.ConvCam;
    }
    public void ChoiceCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("Out",true);
        drawerAnim.SetBool("In",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(true);
        CupboardCam.SetActive(false);
        HarvestCam.SetActive(false);
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.INGRED;
        TeaCeremonyManager.Instance.toolText.SetActive(false);
    }
    public void CupboardCamOn(){ //when player have conversation with customers
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(false);
        CupboardCam.SetActive(true);
        HarvestCam.SetActive(false);
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NOTOOL;
        camState = CamState.BoardCam;
        SnackOffer.Instance.snackText.SetActive(true);
        TeaCeremonyManager.Instance.toolText.SetActive(false);
    }
    public void HarvestCamOn(){ //when player harvest plant
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ChoiceCam.SetActive(false);
        ConversationCam.SetActive(false);
        CupboardCam.SetActive(false);
        HarvestCam.SetActive(true);
        TeaCeremonyManager.Instance.toolText.SetActive(false);
        camState = CamState.HarvestCam;
    }
}
