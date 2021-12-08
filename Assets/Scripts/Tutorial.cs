using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject ToolTutorialTP;
    public GameObject ToolTutorialST;
    public GameObject ToolTutorialPT;
    public GameObject[] TPsteps;
    public GameObject[] STsteps;
    public GameObject[] PTsteps;
    //public GameObject[] currentStepsDisplay;
    public int stepIndex = 0;
    public static Tutorial Instance;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //ToolTutorial.SetActive(true);
        // ToolTutorialTP.SetActive(false);
        // ToolTutorialST.SetActive(false);
        // ToolTutorialPT.SetActive(false);
    }

    void Update(){
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT){
            //ToolTutorialTP.SetActive(true); //make it appear in the center
            //currentStepsDisplay = TPsteps;
            // TPsteps[stepIndex].SetActive(true);
        }else{
            //ToolTutorialTP.SetActive(false);
        }
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.STIRTOOL){
        }
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.POWDERTOOL){
        }
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NOTOOL){
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

}
