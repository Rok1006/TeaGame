using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tutorial tutor;
    public Ghost currGhost;
    public GhostStudent stuGhost;
    public static GameManager Instance;
    public GameObject Fadeout;
    public Ghost gtcs;
    public GameObject DialogueUI;
    public GameObject SoundManager;
    public List<GameObject> ghostList = new List<GameObject>();
    public int ghostIndex = 0;
    bool changeToTeaCam = false;
    public GameObject Arrows;
    public Animator arrowAnim;
    private bool onoffarrow = false;
    private void Awake()
    { 
        Instance = this;
        Fadeout.SetActive(true);
        DialogueUI.SetActive(false);
        SoundManager.SetActive(false);
        StartCoroutine(BeforePlay());
    }
    void Start()
    {
        TeaCeremonyManager.Instance.startDiming = false; //also whenever player is in tutorial
        arrowAnim = Arrows.GetComponent<Animator>();
    }

    IEnumerator BeforePlay(){
        yield return new WaitForSeconds(.5f);
        SceneDataLoad.Instance.TitleScreen.SetActive(true); //with some pop up anim, display text
        yield return new WaitForSeconds(1.3f);
        Fadeout.SetActive(false);
        yield return new WaitForSeconds(1.8f); 
        SceneDataLoad.Instance.TitleScreen.SetActive(false);
        DialogueUI.SetActive(true);
        SoundManager.SetActive(true);
        GhostEnter(); //new
    }
    void Update()
    {
        CheckState();
    }
    public void CheckState()  //stageIndex 0 is stage1, stevo will change it
    {
        if (Input.GetKeyDown(KeyCode.O))
            currGhost.NextStage();
        switch (ghostIndex)
        {
            case (0): //Sensei
            {
                switch (currGhost.stageIndex)
            {
                case (0):  //when player is allowed to move things //make it when ever player is in a tutorial, startDimming to false
                    {
                        //arrowAnim.SetTrigger("");
                        arrowAnim.SetTrigger("Deactivate");
                        ServeTray.Instance.canServe = false;
                        //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;
                        break;//Intro
                    }
                case (1):  //Stage1 - Put on boiler
                    {
                        if (!changeToTeaCam)
                        {
                            arrowAnim.SetTrigger("teapot");
                            CamSwitch.Instance.TeaCamOn();
                            changeToTeaCam = true;
                        }
                        ServeTray.Instance.canServe = true;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                        if (Tutorial.Instance.usedStove)
                        {
                            currGhost.NextStage();
                        }
                        break;
                    }
                case (2):  //Stage2 Add powder
                    {
                        ServeTray.Instance.canServe = false;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UsePowderTool;
                        if (Tea.Instance.numOfPowder > 0 && Tutorial.Instance.usedPowderT)
                        {
                            currGhost.NextStage();
                        }
                        break;
                    }
                case (3):  //Stage3 Look ingredient, auto Next
                    {
                        if(!onoffarrow){ //this looping
                            arrowAnim.SetTrigger("Deactivate");
                            onoffarrow = true;
                        }
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.GetIngredient;
                        break;
                    }
                case (4):  //Stage4 is pour hot water
                    {
                        if(onoffarrow){ //this looping
                            arrowAnim.SetTrigger("Deactivate");
                            arrowAnim.SetTrigger("stove");
                            onoffarrow = false;
                        }
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                        if (Tutorial.Instance.usedTeaPot && Tea.Instance.distance < 0.2f)
                        {
                            currGhost.NextStage();
                        }
                        break;
                    }
                case (5):  //Stage5 is stir
                    {
                        if(!onoffarrow){ //this looping
                            arrowAnim.SetTrigger("Deactivate");
                            arrowAnim.SetTrigger("stir");
                            onoffarrow = true;
                        }
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseStirTool;
                        if (Tutorial.Instance.usedStirT&&Tea.Instance.teasprite.color != Tea.Instance.originalColor)// && Tea.Instance.teasprite.color == Tea.Instance.targetColor)
                            currGhost.NextStage();
                        if (Tea.Instance.numOfIngredients > 0)
                        {  //may need edit 

                        }
                        break;
                    }
                case (6): //serve...Nextstage in Ghost.DrinkTea()
                    {
                        if(onoffarrow){ //this looping
                            arrowAnim.SetTrigger("serve");
                            onoffarrow = false;
                        }
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.ServeOK;
                        ServeTray.Instance.canServe = true;
                        //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.ServeOK;
                        break;
                    }
                case (7): //Snacktime NextStage in Ghost.EatSnack()
                    {
                        if(!onoffarrow){ //this looping
                            GameManager.Instance.arrowAnim.SetTrigger("snack");
                            onoffarrow = true;
                        }
                        ServeTray.Instance.canServe = false;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseSnack;
                        break;
                    }
                case (8): //sensei abt to go, may be put the following to the next
                    {

                        //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;
                        TeaCeremonyManager.Instance.startDiming = true;
                        //Tutorial.Instance.tutorialComplete = true;  //this two may be put after case 8
                        TeaCeremonyManager.Instance.LightingButton.SetActive(true);
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
                break;            
            }
            case (1): //Student
            {
                switch (stuGhost.stageIndex)
                {
                    case (0):
                    {
                            TeaCeremonyManager.Instance.startDiming = true;
                            SnackOffer.Instance.canTakeSnack = true;
                            Tutorial.Instance.tutorialComplete = true;
                            TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;
                            break;
                    }
                }
                break;
            }
            case (2): //Student2
            {
                //save after this student
                break;
            }
        }
    }
    IEnumerator BetweenGhosts() //input day night switch?
    {
        yield return new WaitForSeconds(6);
        GhostEnter();
    }
    public void GhostEnter()
    {
        foreach (GameObject ghost in ghostList)
            ghost.SetActive(false);
        ghostList[ghostIndex].SetActive(true);
    }
    public void GhostLeave()
    {
        switch (ghostIndex)
        {
            case (0):
                {
                    TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;  //add these 2 when players is allow to freeplay 
                    SnackOffer.Instance.canTakeSnack = true;
                    Tutorial.Instance.tutorialComplete = true;
                    break;
                }
        }
        StartCoroutine(BetweenGhosts());
        ghostIndex++;
        //stuGhost = ghostList[ghostIndex];
    }
}
