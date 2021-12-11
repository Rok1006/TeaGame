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
    public void CheckState()  //stageIndex 0 is stage1, stevo will change it
    {
        if (Input.GetKeyDown(KeyCode.O))
            currGhost.NextStage();
        switch (currGhost.stageIndex)
        {
            case (999):   //testing
            {
                // TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;  //add these 2 when players is allow to freeplay 
                // SnackOffer.Instance.canTakeSnack = true;
            //     if (TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT)
            //     {
            //         tutor.NextImage();
            //         currGhost.NextStage();
            //     }
                break;
            }
            case (0):  //when player is allowed to move things //make it when ever player is in a tutorial, startDimming to false
            {
                
                
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                //tell player about the lighting will go dimm
                //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot; 
                //auto next stage after dialogue finish <Next> in txt 
                break;
            }
            case (1):  //Stage2 is boiling tea
            {
                    CamSwitch.Instance.TeaCamOn();
                    TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                    TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseStirTool;
                    TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.GetIngredient;
                    TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UsePowderTool;
                    //if boiled tea on stove, NextStage() 3
                    if (Tutorial.Instance.usedStove){
                    //NextStage() 3
                }
                break;
            }
            case (2):  //Stage3 is pouring tea
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                //if tea distance in cup increase larger than half and put down pot, NextStage() 4 
                if(Tutorial.Instance.usedTeaPot&&Tea.Instance.distance<0.2f){
                    //NextStage() 4 
                }
                break;
            }
            case (3):  //Stage4 is adding powder
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UsePowderTool;
                //if powder in cup >0 and put down powder tool, NextStage() 5 
                if(Tutorial.Instance.usedPowderT&&Tea.Instance.numOfPowder>0){
                    //NextStage() 5 
                }
                break;
            }
            case (4):  //Stage5 is stirring tea
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseStirTool;
                //if color change, stir tool down , NextStage() 6 
                if(Tutorial.Instance.usedStirT&&Tea.Instance.teasprite.color ==Tea.Instance.targetColor){
                   // NextStage() 6 
                }
                break;
            }
            case (5):  //Stage6 is add ingredients
            {
                TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.GetIngredient;
                //if added ingredients>0 , NextStage()
                if(Tea.Instance.numOfIngredients>0){  //may need edit 
                                                      //NextStage() go on and serve it
                        TeaCeremonyManager.Instance.startDiming = true;
                    }
                break;
            }
            //case 7 : pen camera to teaCam, allow player to serve
            //case (?):  //Tutorial ENDED , make real tea now
            //{
                //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;  //add these 2 when players is allow to freeplay 
                //SnackOffer.Instance.canTakeSnack = true;
                //Tutorial.Instance.tutorialComplete = true;
            //}

            // at some stage add SnackOffer.Instance.canTakeSnack = true; //allow player to take snack
            //after that is free play
            //how to turn them all to free to use later, afterwards  just put  or free play in too !! must be in front e.h Freeplay|| Usetool && ...

        }
    }
}
