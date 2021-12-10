using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tutorial tutor;
    public Ghost currGhost;

    
    void Start()
    {
        TeaCeremonyManager.Instance.startDiming = false; //also whenever player is in tutorial
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
    }
    public void CheckState()
    {
        switch (currGhost.stageIndex)
        {
            // case (0):
            // {
            //     if (TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT)
            //     {
            //         tutor.NextImage();
            //         currGhost.NextStage();
            //     }
            //     break;
            // }
            case (1):  //when player is allowed to move things //make it when ever player is in a tutorial, startDimming to false
            {
                //Change camera angle to teaCam
                CamSwitch.Instance.TeaCamOn();
                TeaCeremonyManager.Instance.startDiming = true; 
                //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;  
                break;
            }
            case (2):  //Stage2 is boiling tea
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                break;
            }
            case (3):  //Stage3 is pouring tea
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                break;
            }
            case (4):  //Stage4 is adding powder
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UsePowderTool;
                break;
            }
            case (5):  //Stage5 is stirring tea
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseStirTool;
                break;
            }
            case (6):  //Stage6 is add ingredients
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.GetIngredient;
                break;
            }

            // at some stage add SnackOffer.Instance.canTakeSnack = true; //allow player to take snack
            //after that is free play
            //how to turn them all to free to use later, afterwards  just put  or free play in too !! must be in front e.h Freeplay|| Usetool && ...

        }
    }
}
