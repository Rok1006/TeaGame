using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public Tutorial tutor;
    public Ghost currGhost;
    public GhostStudent stuGhost;
    public int Laikai_index;
    public static GameManager Instance;
    public GameObject Fadeout;
    public GameObject Fadeout2;
    Animator fd2;
    public Ghost gtcs;
    public GameObject DialogueUI; //OLD
    public GameObject YarnDialogueSys; //Yarn
    public GameObject SoundManager;
    public List<GameObject> ghostList = new List<GameObject>();
    public int ghostIndex = 0;
    bool changeToTeaCam = false;
    public GameObject Arrows;
    public Animator arrowAnim;
    private bool onoffarrow = false;
    public LineView lineView;
    public DialogueRunner runner;
    
    private void Awake()
    { 
        Instance = this;
        fd2 = Fadeout2.GetComponent<Animator>();
        Fadeout.SetActive(true);
        DialogueUI.SetActive(false);
        SoundManager.SetActive(false);
        StartCoroutine(BeforePlay());
    }
    void Start()
    {
        TeaCeremonyManager.Instance.startDiming = false; //also whenever player is in tutorial
        arrowAnim = Arrows.GetComponent<Animator>();
        runner.StartDialogue("Sensi_Start");
    }

    IEnumerator BeforePlay(){
        yield return new WaitForSeconds(.5f);
        SceneDataLoad.Instance.TitleScreen.SetActive(true); //with some pop up anim, display text
        yield return new WaitForSeconds(1.3f);
        Fadeout.SetActive(false);
        yield return new WaitForSeconds(1.8f); 
        SceneDataLoad.Instance.TitleScreen.SetActive(false);
        if(ghostIndex==0){
            DialogueUI.SetActive(true);   
        }
        SoundManager.SetActive(true);
        GhostEnter(); 
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
                        TeaCup.Instance.canServe = false;
                        //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay; //off when build 
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
                        TeaCup.Instance.canServe = true;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                        if (Tutorial.Instance.usedStove)
                        {
                            currGhost.NextStage();
                        }
                        break;
                    }
                case (2):  //Stage2 Add powder
                    {
                        TeaCup.Instance.canServe = false;
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
                        TeaCup.Instance.canServe = true;
                        //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.ServeOK;
                        break;
                    }
                case (7): //Snacktime NextStage in Ghost.EatSnack()
                    {
                        if(!onoffarrow){ //this looping
                            GameManager.Instance.arrowAnim.SetTrigger("snack");
                            onoffarrow = true;
                        }
                        TeaCup.Instance.canServe = false;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseSnack;
                        break;
                    }
                case (8): //sensei abt to go, may be put the following to the next
                    {

                        //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;
                        //TeaCeremonyManager.Instance.startDiming = true; //removed the candle, off for now
                        //Tutorial.Instance.tutorialComplete = true;  //this two may be put after case 8
                        //TeaCeremonyManager.Instance.LightingButton.SetActive(true);//off for now
                        break;
                    }

                }
                break;            
            }
            case (1): //Student
            {
                //DialogueUI.SetActive(false);
                switch (stuGhost.stageIndex)
                {
                    case (0):
                    {
                            //TeaCeremonyManager.Instance.startDiming = true; //off for now
                            SnackOffer.Instance.canTakeSnack = true;
                            Tutorial.Instance.tutorialComplete = true;
                            
                          
                                if (TeaCeremonyManager.Instance.served) {
                                    Debug.Log("student_ghost_canserve");

                                    stuGhost.stageIndex = 1;
                                }
                            break;
                    }
                        case (1): {
                                Debug.Log("student_ghost_level_1");
                              
                                runner.StartDialogue("Student_2nd_Phase");
                                CamSwitch.Instance.ConversationCamOn();
                                stuGhost.stageIndex = 2;
                                break;
                            }
                        case (2):
                            {
                            
                                break;
                            }
                    }
                break;
            }
            case (2): //Laika
                {
                    switch (Laikai_index)
                    {
                        case (0):
                            {
                                //TeaCeremonyManager.Instance.startDiming = true; //off for now

                                SnackOffer.Instance.canTakeSnack = true;
                                Tutorial.Instance.tutorialComplete = true;


                                if (TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.canProceed)
                                {
                                    JudgeTea.Instance.CheckCurrentTea();
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("laikai!");
                                        Laikai_index = 1;

                                    }
                                    else
                                    {
                                        runner.StartDialogue("Laikai_Wrong_Choice");
                                        CamSwitch.Instance.ConversationCamOn();
                                        
                                        TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                        TeaCeremonyManager.Instance.canProceed =  false; //new
                                        //TeaCeremonyManager.Instance.TeaReturn(); //dont want this to do this fast but if remove will pause the game
                                    }

                                    Debug.Log("laikai?");

                                }
                                break;
                            }
                        case (1):
                            {

                                runner.StartDialogue("Laikai_Stage_1_human");
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new

                                Laikai_index = 2;
                                break;
                            }
                        case (2):
                            {
                                if (TeaCeremonyManager.Instance.served)
                                {
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("laikai!");
                                        Laikai_index = 3;

                                    }
                                    else
                                    {
                                        runner.StartDialogue("Laikai_Wrong_Choice");
                                        CamSwitch.Instance.ConversationCamOn();

                                        TeaCeremonyManager.Instance.TeaReturn();
                                    }

                                    Debug.Log("laikai?");

                                }
                                break;

                            }
                        case (3):
                            {
                                runner.StartDialogue("Laikai_Final_Stage");
                                Laikai_index = 4;
                                TeaCeremonyManager.Instance.TeaReturn();
                                break;
                            }
                        case (4):
                            {
                                break;
                            }
                    }
                //save after this student
                break;
            }
            case (3):  //Capitalist
            {


            break;
            }
        }
    }
    IEnumerator BetweenGhosts() //input day night switch?
    {
        yield return new WaitForSeconds(.2f);  //need refine
        fd2.SetBool("in",true);
        fd2.SetBool("out",false);
        yield return new WaitForSeconds(3f);
        fd2.SetBool("out",true);
        fd2.SetBool("in",false);
        yield return new WaitForSeconds(.2f);
        SceneDataLoad.Instance.TitleScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneDataLoad.Instance.TitleScreen.SetActive(false);
        GhostEnter();
    }
    public void GhostEnter()
    {
        foreach (GameObject ghost in ghostList)
            ghost.SetActive(false);
            ghostList[ghostIndex].SetActive(true);
        if(ghostIndex==1){
            Debug.Log("ghostindex");
            runner.StartDialogue("Student_Start");}
        if (ghostIndex == 2) { runner.StartDialogue("Laikai_Start"); }
        

    }
    public  void GhostLeave()
    {
        switch (ghostIndex)
        {
            case (0):  //when sensei leave
                {
                    TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.Nothing;  //add these 2 when players is allow to freeplay 
                    SnackOffer.Instance.canTakeSnack = true;
                    Tutorial.Instance.tutorialComplete = true;
                    DialogueUI.SetActive(false);
                    YarnDialogueSys.SetActive(true);
                    break;
                }
            case (1): //when student leave
            {
                break;
            }

        }
        StartCoroutine(BetweenGhosts());
        ghostIndex++;
       //GhostEnter();
        //stuGhost = ghostList[ghostIndex];
    }
}
