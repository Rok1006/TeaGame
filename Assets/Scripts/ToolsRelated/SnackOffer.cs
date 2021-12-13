using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnackOffer : MonoBehaviour
{
    public static SnackOffer Instance;
    public string toolName;
    public Outline otsc;
    public GameObject tutorial;
    public GameObject snackText;  //put the text foreverything
    public string skText;
    public bool canTakeSnack = true;
    public GameObject snackParticles;
    public SoundManager sc;
    void Awake() {
        Instance=this;
    }
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        otsc.enabled = false;
        tutorial.SetActive(false);
        snackText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        snackText.GetComponent<Text>().text =  skText.ToString();
    }
    void OnMouseOver() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE
            &&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseSnack||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE
            ){ //&&canTakeSnack
            otsc.enabled = true;
            tutorial.SetActive(true);
            TeaCeremonyManager.Instance.tText = toolName;
        }
    }
    void OnMouseExit() {
        otsc.enabled = false;
        tutorial.SetActive(false);
        TeaCeremonyManager.Instance.tText = "";
    }
    void OnMouseDown() {
        if (TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE
            && TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseSnack||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE
            )//&& canTakeSnack
        {
            sc.PickToolUp();
            tutorial.SetActive(false);
            CamSwitch.Instance.CupboardCamOn();
            snackText.SetActive(true);
        }
    }
    public void GoBack(){
        snackText.SetActive(false);
    }
}
