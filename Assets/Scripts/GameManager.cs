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
    public int Captial_index;
    public static GameManager Instance;
    public GameObject Fadeout;
    public GameObject Fadeout2;
    Animator fd2;
    public Ghost gtcs;
    public GameObject DialogueUI; //OLD
    public GameObject YarnDialogueSys; //Yarn
    public GameObject SoundManager;
    public List<GameObject> ghostList = new List<GameObject>();
    public  int ghostIndex = 0;
    public static bool changeToTeaCam = false;
    public GameObject Arrows;
    public Animator arrowAnim;
    public bool onoffarrow = false;
    public LineView lineView;
    public DialogueRunner runner;
     static bool angry_time;
    public static bool count;
    public bool tutorialIngredGet = false;
    public bool tutorialAteSnack = false;
    public int wrongCount = 0;
    public PauseMenu PM;
    
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
   
           DialogueUI.SetActive(false);   
      
        SoundManager.SetActive(true);
        GhostEnter(); 
    }
    void Update()
    {
        if (runner.IsDialogueRunning) {
            TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.Nothing;
        }
        CheckState();
        if (count) { StartCoroutine(Angry());count = false; }
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
                                currGhost.stageIndex = 1;
                                //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay; //off when build 
                                break;//Intro
                    }
                case (1):  //Stage1 - Put on boiler
                    {
                       if (changeToTeaCam)
                        {
                            arrowAnim.SetTrigger("teapot");
                            CamSwitch.Instance.TeaCamOn();
                            changeToTeaCam = false;
                        }
                        TeaCup.Instance.canServe = true;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseTeapot;
                                if (Tutorial.Instance.usedStove)
                                {
                                    runner.Stop();
                                    runner.StartDialogue("Sensei_Stage_2");
                                    currGhost.stageIndex = 2;

                                }
                                else if (angry_time) {
                                   
                                    runner.StartDialogue("Sensei_Stage_1_Angry"); angry_time = false; }
                      
                        break;
                    }
                case (2):  //Stage2 Add powder
                    {
                        TeaCup.Instance.canServe = false;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UsePowderTool;
                        if (Tea.Instance.numOfPowder > 0 && Tutorial.Instance.usedPowderT)
                        {
                                    runner.Stop();
                                    runner.StartDialogue("Sensei_Stage_3");
                                    currGhost.stageIndex = 3;
                                }
                                else if (angry_time)
                                {

                                    runner.StartDialogue("Sensei_Stage_2_Angry"); angry_time = false;
                                }
                                break;
                    }
                case (3):  //Stage3 Look ingredient, auto Next
                    {
                        if(!onoffarrow){ //this looping
                            arrowAnim.SetTrigger("Deactivate");
                            arrowAnim.SetTrigger("ingredients");
                            onoffarrow = true;
                        }
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.GetIngredient;
                            //if have ingredients move on where the check at
                        //    if(Tea.Instance.numOfIngredients>0&&tutorialIngredGet){
                        //         currGhost.stageIndex = 4;
                        //     }
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
                            runner.Stop();
                            runner.StartDialogue("Sensei_Stage_4");
                            currGhost.stageIndex = 5;
                        }
                        else if (angry_time)
                        {

                            runner.StartDialogue("Sensei_Stage_3_Angry"); angry_time = false;
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
                                if (Tutorial.Instance.usedStirT && Tea.Instance.teasprite.color != Tea.Instance.originalColor)// && Tea.Instance.teasprite.color == Tea.Instance.targetColor)
                                {
                                    runner.Stop();
                                    runner.StartDialogue("Sensei_Stage_5");
                                    currGhost.stageIndex = 6;
                                }
                                else if (angry_time)
                                {

                                    runner.StartDialogue("Sensei_Stage_4_Angry"); angry_time = false;
                                }
                                if (Tea.Instance.numOfIngredients > 0)
                        {  //may need edit 

                        }
                        break;
                    }
                case (6): //serve...Nextstage in Ghost.DrinkTea()
                    {
                        if(onoffarrow){ //this looping
                            arrowAnim.SetTrigger("Deactivate");
                            arrowAnim.SetTrigger("serve");
                            onoffarrow = false;
                        }
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.ServeOK;
                        TeaCup.Instance.canServe = true;
                                //TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.ServeOK;
                                //JudgeTea.Instance.CheckCurrentTea();
                                if (TeaCeremonyManager.Instance.served)//JudgeTea.Instance.IFPass()
                                {
                                    runner.Stop();
                                    runner.StartDialogue("Sensei_Stage_6"); 
                                    currGhost.stageIndex = 7; Debug.Log("next stage");
                                }
                                break;
                    }
                case (7): //Snacktime NextStage in Ghost.EatSnack()
                    {
                        if(!onoffarrow){ //this looping
                            arrowAnim.SetTrigger("Deactivate");
                            GameManager.Instance.arrowAnim.SetTrigger("snack");
                            onoffarrow = true;
                        }
                        TeaCup.Instance.canServe = false;
                        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.UseSnack;
                                if (tutorialAteSnack) {
                                    runner.Stop();
                                    runner.StartDialogue("Sensei_Final");
                                    currGhost.stageIndex = 8;
                                }
                                else if (angry_time)
                                {

                                    runner.StartDialogue("Sensei_Stage_6_Angry"); angry_time = false;
                                }
                                break;
                    }
                case (8): //sensei abt to go, may be put the following to the next
                    {
                        PM.saveGhostIndex();
                        PM.saveBiggestIndex();
                        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NOTOOL;
                        // runner.Stop();
                        // runner.StartDialogue("Sensei_Final");
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
                                PM.saveGhostIndex(); 
                                PM.saveBiggestIndex();
                                //TeaCeremonyManager.Instance.startDiming = true; //off for now
                                SnackOffer.Instance.canTakeSnack = true;
                                Tutorial.Instance.tutorialComplete = true;
                                //ZoneStabllize.Instance.zoneHarm = true; //plant start being affected

                                if (TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.canProceed)
                                {
                                    Debug.Log("student_ghost_canserve");
                                    JudgeTea.Instance.CheckCurrentTea();
                                    if (JudgeTea.Instance.IFPass())
                                    { stuGhost.stageIndex = 1;}
                                    else
                                    {
                                        wrongCount+=1;
                                        runner.Stop();
                                      
                                        runner.StartDialogue("Student_Wrong");
                                        CamSwitch.Instance.ConversationCamOn();

                                        TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                        TeaCeremonyManager.Instance.canProceed = false; //new}

                                    }
                                    
                                }
                                break;
                            }
                        case (1): {
                                ZoneStabllize.Instance.ResetPlantStatus();
                                ZoneStabllize.Instance.zoneHarm = false;
                                runner.Stop();
                                runner.StartDialogue("Student_Right");
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                CamSwitch.Instance.ConversationCamOn();
                                stuGhost.stageIndex = 2;
                                break;
                            }
                        case (2):
                            {
                                Debug.Log("student_ghost_level_1");
                                runner.Stop();
                                runner.StartDialogue("Student_2nd_Phase");
                                //CamSwitch.Instance.ConversationCamOn();
                                stuGhost.stageIndex = 3;
                                wrongCount = 0;
                                break;
                                
                            }
                        case (3): { break; }
                        case (4): { break; }
                    }
                break;
            }
            case (2): //Laika
                {
                    switch (Laikai_index)
                    {
                        case (0):
                            {
                                PM.saveGhostIndex();
                                PM.saveBiggestIndex();
                                //TeaCeremonyManager.Instance.startDiming = true; //off for now
                                SnackOffer.Instance.canTakeSnack = true;
                                Tutorial.Instance.tutorialComplete = true;
                                // ZoneStabllize.Instance.zoneHarm = true;

                                if (TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.canProceed)
                                {
                                    JudgeTea.Instance.CheckCurrentTea(); //must put
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("laikai!");
                                        Laikai_index = 1;
                                    }
                                    else
                                    {
                                        wrongCount+=1;
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
                                ZoneStabllize.Instance.ResetPlantStatus();
                                runner.StartDialogue("Laikai_Stage_1_human");
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                wrongCount = 0;
                                Laikai_index = 2;
                                break;
                            }
                        case (2):
                            {
                                if (TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.canProceed) //must
                                {
                                    JudgeTea.Instance.CheckCurrentTea(); //must
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("laikai!");
                                        Laikai_index = 3;

                                    }
                                    else
                                    {
                                        wrongCount+=1;
                                        runner.StartDialogue("Laikai_Wrong_Choice_2");
                                        CamSwitch.Instance.ConversationCamOn();
                                        TeaCeremonyManager.Instance.OtherTeaReturn(); //must
                                        TeaCeremonyManager.Instance.canProceed =  false; //must
                                        //TeaCeremonyManager.Instance.TeaReturn();
                                    }

                                    Debug.Log("laikai?");

                                }
                                break;

                            }
                        case (3):
                            {
                                wrongCount = 0;
                                ZoneStabllize.Instance.ResetPlantStatus();
                                ZoneStabllize.Instance.zoneHarm = false;
                                runner.StartDialogue("Laikai_Final_Stage");
                                Laikai_index = 4;
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                //TeaCeremonyManager.Instance.TeaReturn();
                                break;
                            }
                        case (4):
                            {
                                JudgeTea.Instance.GhostTeaNum = 0;
                                break;
                            }
                    }
                //save after this student
                break;
            }
            case (3):  //Capitalist
            {
                    switch (Captial_index)
                    {
                        case (0): {
                                PM.saveGhostIndex();
                                PM.saveBiggestIndex();
                                SnackOffer.Instance.canTakeSnack = true;
                                Tutorial.Instance.tutorialComplete = true;
                                //ZoneStabllize.Instance.zoneHarm = true;

                                if (TeaCeremonyManager.Instance.served && TeaCeremonyManager.Instance.canProceed)
                                {
                                    JudgeTea.Instance.CheckCurrentTea(); //must put
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("that is very captial of you!");
                                        Captial_index = 1;

                                    }
                                    else
                                    {
                                        wrongCount+=1;
                                        runner.StartDialogue("Captial_Stage_1_wrong_tea");
                                        CamSwitch.Instance.ConversationCamOn();

                                        TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                        TeaCeremonyManager.Instance.canProceed = false; //new
                                        //TeaCeremonyManager.Instance.TeaReturn(); //dont want this to do this fast but if remove will pause the game
                                    }

                                    Debug.Log("laikai?");

                                }
                                break;
                            }
                        case (1):
                            {
                                ZoneStabllize.Instance.ResetPlantStatus();
                                runner.StartDialogue("Captial_Stage_2");
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                JudgeTea.Instance.GhostTeaNum = 1;
                                Captial_index = 2;
                                wrongCount=0;
                                break;
                            }
                        case (2):
                            {
                                if (TeaCeremonyManager.Instance.served && TeaCeremonyManager.Instance.canProceed) //must
                                {
                                    JudgeTea.Instance.CheckCurrentTea(); //must
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("captial!");
                                        Captial_index = 3;

                                    }
                                    else
                                    {
                                        wrongCount+=1;
                                        runner.StartDialogue("Captial_Stage_2_wrong_tea");
                                        CamSwitch.Instance.ConversationCamOn();
                                        TeaCeremonyManager.Instance.OtherTeaReturn(); //must
                                        TeaCeremonyManager.Instance.canProceed = false; //must
                                        //TeaCeremonyManager.Instance.TeaReturn();
                                    }
                                }
                                break;
                            }
                        case (3):
                            {
                                ZoneStabllize.Instance.ResetPlantStatus();
                                runner.StartDialogue("Captial_Stage_3");
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                JudgeTea.Instance.GhostTeaNum = 2;
                                Captial_index = 4;
                                wrongCount = 0;
                                break;
                            }
                        case (4):
                            {
                                if (TeaCeremonyManager.Instance.served && TeaCeremonyManager.Instance.canProceed) //must
                                {
                                    JudgeTea.Instance.CheckCurrentTea(); //must
                                    if (JudgeTea.Instance.IFPass())
                                    {
                                        Debug.Log("captial!");
                                        Captial_index = 5;

                                    }
                                    else
                                    {
                                        wrongCount+=1;
                                        runner.StartDialogue("Captial_Stage_3_wrong_tea");
                                        CamSwitch.Instance.ConversationCamOn();
                                        TeaCeremonyManager.Instance.OtherTeaReturn(); //must
                                        TeaCeremonyManager.Instance.canProceed = false; //must
                                        //TeaCeremonyManager.Instance.TeaReturn();
                                    }
                                }
                                break;
                            }
                        case (5):
                            {
                                ZoneStabllize.Instance.ResetPlantStatus();
                                ZoneStabllize.Instance.zoneHarm = false;
                                runner.StartDialogue("Captial_Stage_3_right_tea");
                                TeaCeremonyManager.Instance.OtherTeaReturn(); //new
                                JudgeTea.Instance.GhostTeaNum = 0;
                                Captial_index = 4;
                                wrongCount = 0;
                                break;
                            }
                    }

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
        GhostEnter();
        fd2.SetBool("out",true);
        fd2.SetBool("in",false);
        yield return new WaitForSeconds(.2f);
        SceneDataLoad.Instance.TitleScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneDataLoad.Instance.TitleScreen.SetActive(false);
    }
    public static IEnumerator  Angry() {
        yield return new WaitForSeconds(5f);
        angry_time = true;
    }
    public void GhostEnter()
    {
        foreach (GameObject ghost in ghostList)
            ghost.SetActive(false);
            ghostList[ghostIndex].SetActive(true);
        if (ghostIndex == 0)
        {
            Debug.Log("sensei");
            runner.StartDialogue("Sensei_Start");
        }
        if (ghostIndex==1){
      
            runner.StartDialogue("Student_Start");}
        if (ghostIndex == 2) { runner.StartDialogue("Laikai_Start"); }

        if (ghostIndex == 3) { runner.StartDialogue("Captial_Stage_1"); }
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
                  // DialogueUI.SetActive(false);
                    //YarnDialogueSys.SetActive(true);
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
