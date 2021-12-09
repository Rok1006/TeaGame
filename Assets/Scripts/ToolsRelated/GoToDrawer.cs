using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToDrawer : MonoBehaviour
{
    public static GoToDrawer Instance;
    public string toolName;
    public Outline oc;
    public int state = 0;
    bool canClick;
    public GameObject tutorial;
    public GameObject ingredientText;
    public string IGText;
    private string tutorialText;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        oc.enabled = false;
        tutorial.SetActive(false);
        tutorialText = "ADD INGREDIENTS";
        ingredientText.SetActive(false);
    }

    void Update()
    {
        tutorial.GetComponent<Text>().text =  tutorialText.ToString();
        ingredientText.GetComponent<Text>().text =  IGText.ToString();
        if(state==1){
            ingredientText.SetActive(true);
            tutorialText = "GO BACK";
        }else{
            ingredientText.SetActive(false);
            Invoke("OriginalText",1f);
        }
    }
    void OnMouseOver() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.INGRED){
            oc.enabled = true;
            canClick=true;
            tutorial.SetActive(true);
            TeaCeremonyManager.Instance.tText = toolName;
        }
    }

    void OnMouseExit(){
        oc.enabled = false;
        canClick=false;
        tutorial.SetActive(false);
        TeaCeremonyManager.Instance.tText = "";
    }
    void OnMouseDown() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.INGRED){
        tutorial.SetActive(false);
        if(state==0){
            CamSwitch.Instance.ChoiceCamOn();
            Invoke("stateChange", .5f);
        }
        if(state==1){
            CamSwitch.Instance.TeaCamOn();
            state=0;
        }
        }
    }
    void stateChange(){
        state=1;
    }
    void OriginalText(){
        tutorialText = "ADD INGREDIENTS";
    }
    public void GoBack(){
        CamSwitch.Instance.TeaCamOn();
    }
}
