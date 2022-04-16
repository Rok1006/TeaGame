using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script is on UI_overlay>ToolTutorial
public class Tutorial : MonoBehaviour
{
    public GameObject ToolTutorialTP;
    public GameObject ToolTutorialST;
    public GameObject ToolTutorialPT;
    public GameObject ToolTutorialCT;
    public GameObject[] TPsteps;
    public GameObject[] STsteps;
    public GameObject[] PTsteps;
    public GameObject[] CTsteps;
    //public GameObject[] currentStepsDisplay;
    public int stepIndex = 0;
    public static Tutorial Instance;
    public bool tutorialComplete = false;
    [Header("TutorialCheck")]
    public bool usedStove = false;  //placed on stove
    public bool usedTeaPot = false;  //placed back on table
    public bool usedPowderT = false;  //placed bk o table
    public bool usedStirT = false; //placed bk on table
    public bool usedIngred = false;  
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
    }

    void Update(){
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT){
            //ToolTutorialTP.SetActive(true); //make it appear in the center
            //currentStepsDisplay = TPsteps;
            // TPsteps[stepIndex].SetActive(true);
        }
        if(TPsteps[stepIndex].activeSelf&&stepIndex>0){
            TPsteps[stepIndex-1].SetActive(false);
        }
        if(STsteps[stepIndex].activeSelf&&stepIndex>0){
            STsteps[stepIndex-1].SetActive(false);
        }
        if(PTsteps[stepIndex].activeSelf&&stepIndex>0){
            PTsteps[stepIndex-1].SetActive(false);
        }

    }
    public void NextImage()
    {
        //stepIndex++;
        //img.sprite = sprArr[sprIndex];
    }
    public void NextStep(){
        stepIndex++;
    }
    public void ResetSteps(){
        stepIndex=0;
        TPsteps[Tutorial.Instance.stepIndex].SetActive(false);
        STsteps[Tutorial.Instance.stepIndex].SetActive(false);
        PTsteps[Tutorial.Instance.stepIndex].SetActive(false);
    }

    // void OrderOfTutorialCheck(){
    //     switch()
    // }
}
