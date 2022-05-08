using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("Others")]
    public GameObject snackButton;
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

        snackButton.SetActive(false);
        Effects.Instance.AngryEffect.SetActive(true);
        // Effects.Instance.AngryEffect.transform.position = new Vector3(TeaCam.transform.position.x+0.405f,TeaCam.transform.position.y-3.266f,TeaCam.transform.position.z+3.785f);
        // Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(47.1859131f,0,0);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            camState = CamState.StartCam;
            StartCamOn();
        }
        else if(Input.GetKeyDown(KeyCode.Z)) //using
        {
            camState = CamState.TeaCam;
            TeaCamOn();
            //drawerAnim.SetTrigger("in");
        }
        else if(Input.GetKeyDown(KeyCode.X)) //using
        {
            camState = CamState.ConvCam;
            ConversationCamOn();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4)) //using
        {
            camState = CamState.ChoiceCam;
            ChoiceCamOn();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5)) //using
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
        Effects.Instance.AngryEffect.SetActive(false);
    }
    public void TeaCamOn(){  //When player is making tea
    camState = CamState.TeaCam;
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
        if(ZoneStabllize.Instance.warning){
            //StartCoroutine(AngryEffect());
            Effects.Instance.AngryEffect.SetActive(false);
            Effects.Instance.AngryEffect.SetActive(true);
            Effects.Instance.AngryEAnim.SetTrigger("IN");
            Effects.Instance.AngryEffect.transform.position = new Vector3(TeaCam.transform.position.x+0.405f,TeaCam.transform.position.y-3.266f,TeaCam.transform.position.z+3.785f);
            Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(47.1859131f,0,0);
        }
    }
    public void ConversationCamOn(){ //when player have conversation with customers
    camState = CamState.ConvCam;
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
        if(ZoneStabllize.Instance.warning){
            Debug.Log("test");
            //StartCoroutine(AngryEffect());
        Effects.Instance.AngryEffect.SetActive(false);
        Effects.Instance.AngryEffect.SetActive(true);
        Effects.Instance.AngryEAnim.SetTrigger("IN");
        Effects.Instance.AngryEffect.transform.position = new Vector3(ConversationCam.transform.position.x+0.525f,ConversationCam.transform.position.y-0.202f,ConversationCam.transform.position.z+4.89f);
        Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(0,0,0);
        }
    }
    public void ChoiceCamOn(){ //when player have conversation with customers
    camState = CamState.ChoiceCam;
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
        if(ZoneStabllize.Instance.warning){
           // StartCoroutine(AngryEffect());
        Effects.Instance.AngryEffect.SetActive(false);
        Effects.Instance.AngryEffect.SetActive(true);
        Effects.Instance.AngryEAnim.SetTrigger("IN");
        Effects.Instance.AngryEffect.transform.position = new Vector3(ChoiceCam.transform.position.x+0.875f,ChoiceCam.transform.position.y-4.192f,ChoiceCam.transform.position.z+2.84f);
        Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(47.19f,0,0);
        }
    }
    public void CupboardCamOn(){ //when player have conversation with customers
    camState = CamState.BoardCam;
        drawerAnim.SetBool("In",true);
        drawerAnim.SetBool("Out",false);
        StartCam.SetActive(false);
        TeaCam.SetActive(false); 
        ConversationCam.SetActive(false);
        ChoiceCam.SetActive(false);
        CupboardCam.SetActive(true);
        HarvestCam.SetActive(false);
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NOTOOL;
        SnackOffer.Instance.snackText.SetActive(true);
        TeaCeremonyManager.Instance.toolText.SetActive(false);
        if(ZoneStabllize.Instance.warning){
            //StartCoroutine(AngryEffect());
        Effects.Instance.AngryEffect.SetActive(false);
        Effects.Instance.AngryEffect.SetActive(true);
        Effects.Instance.AngryEAnim.SetTrigger("IN");
        Effects.Instance.AngryEffect.transform.position = new Vector3(CupboardCam.transform.position.x-4.615f,CupboardCam.transform.position.y-0.652f,CupboardCam.transform.position.z+0.49f);
        Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(0,270f,0);
        }
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
//-----------------Others
    public void ClickSnackButtonBack(){
        camState = CamState.TeaCam;
        TeaCamOn();
        snackButton.SetActive(false);
    }
    IEnumerator AngryEffect(){
        Effects.Instance.AngryEAnim.SetTrigger("OUT");
        yield return new WaitForSeconds(2f);
        Effects.Instance.AngryEffect.SetActive(false);
        Effects.Instance.AngryEffect.SetActive(true);
        Effects.Instance.AngryEAnim.SetTrigger("IN");
    }
    public void DetermineAngryEffectPos(){
        switch(camState){
            case CamState.TeaCam:
                Effects.Instance.AngryEffect.transform.position = new Vector3(TeaCam.transform.position.x+0.405f,TeaCam.transform.position.y-3.266f,TeaCam.transform.position.z+3.785f);
                Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(47.1859131f,0,0);
            break;
            case CamState.ConvCam:
                Effects.Instance.AngryEffect.transform.position = new Vector3(ConversationCam.transform.position.x+0.525f,ConversationCam.transform.position.y-0.202f,ConversationCam.transform.position.z+4.89f);
                Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(0,0,0);
            break;
            case CamState.BoardCam:
                Effects.Instance.AngryEffect.transform.position = new Vector3(CupboardCam.transform.position.x-4.615f,CupboardCam.transform.position.y-0.652f,CupboardCam.transform.position.z+0.49f);
                Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(0,270f,0);
                
            break;
            case CamState.ChoiceCam:
                Effects.Instance.AngryEffect.transform.position = new Vector3(ChoiceCam.transform.position.x+0.875f,ChoiceCam.transform.position.y-4.192f,ChoiceCam.transform.position.z+2.84f);
                Effects.Instance.AngryEffect.transform.eulerAngles = new Vector3(47.19f,0,0);
            break;
        }
    }
}
