using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
public class Ghost : MonoBehaviour
{
    #region TextReader
    TextAsset txtFile;

    [System.Serializable]
    public class StageData
    {
        public List<DialogData> dialogList = new List<DialogData>();
        public List<DialogData> TeaReactList = new List<DialogData>();
        public List<DialogData> TeaTutorList = new List<DialogData>();
        public float stagePause;
    }
    [System.Serializable]
    public class DialogData
    {
        public string dialog;
        public float pauseTime;
        public string animType;
    }
    #endregion

    public List<StageData> stageList = new List<StageData>();
    public TextMeshProUGUI textDisplay;
    
    public GameObject objDialogBox;
    public RectTransform rectTrans;
    float dialogBoxPosDown = -162f;
    float dialogBoxPosUp = 160f;

    public float typingSpeed = 0.2f;
    public int stageIndex = 0; //real stage number -1
    public int dialogIndex = 0; //the line we are currently on
    Coroutine dialogLoopCor; //Store so we can stop
    Coroutine reactCor;
    int wrongCount = 0;
    public AudioSource sensi_SE;
    public AudioClip happy;
    public AudioClip shy;
    public AudioClip angry;
    public AudioClip normal;

    public Animator anim;

    public Button button;
    public bool buttonPressed;
    public bool dialogueOngoing;
    void Start()
    {
        ReadDialogText();
        ReadReactText();
        ReadTutorText();
        textDisplay.text = "";
        StartCoroutine(StageLoop());
        
        anim = GetComponent<Animator>();

        rectTrans = objDialogBox.GetComponent<RectTransform>();
        objDialogBox.SetActive(false);
        button.onClick.AddListener(ifButtonClicked);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //DrinkTea("Bad");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //DrinkTea("Good");
        }

        //DialogBoxPos
        if(CamSwitch.Instance.camState == CamSwitch.CamState.TeaCam||CamSwitch.Instance.camState == CamSwitch.CamState.BoardCam)
            rectTrans.anchoredPosition3D = new Vector3(rectTrans.anchoredPosition3D.x,dialogBoxPosUp, rectTrans.anchoredPosition3D.z);
        else if (CamSwitch.Instance.camState == CamSwitch.CamState.ConvCam)
            rectTrans.anchoredPosition3D = new Vector3(rectTrans.anchoredPosition3D.x, dialogBoxPosDown, rectTrans.anchoredPosition3D.z);
    }
    IEnumerator StageLoop() //Main loop for each stage
    {
        Debug.Log("New Stage: "+stageIndex);
        yield return new WaitForSeconds(stageList[stageIndex].stagePause); //Wait before begin
        Debug.Log("Start dialog");
        dialogLoopCor = StartCoroutine(TypeAll());

    }
    IEnumerator TypeAll() //Go through every dialog of the stage
    {
        objDialogBox.SetActive(true);
        button.gameObject.SetActive(true);
        for(int i=dialogIndex; i<stageList[stageIndex].dialogList.Count; i++)
        {
            DialogData dialogData = stageList[stageIndex].dialogList[i];
            if (dialogData.dialog == "<Next>")
            {
                NextStage();
            }
            else
            {
                buttonPressed = false;
                Animate(dialogData.animType);
                textDisplay.text = "";
                dialogueOngoing = true;
                foreach (char letter in dialogData.dialog.ToCharArray())
                {
                    if (Input.GetKeyDown(KeyCode.Period)) //Debug Stuff
                    {
                        i++;
                        print(i);
                    }
                    else if (Input.GetKeyDown(KeyCode.Comma))
                    {
                        i--;
                        print(i);
                    }
                    textDisplay.text += letter;
                    
                    yield return new WaitForSeconds(typingSpeed);
                }
                dialogueOngoing = false;
                //yield return new WaitForSeconds(dialogData.pauseTime);
                if (buttonPressed && !dialogueOngoing)
                {
                    
                    dialogIndex++;
                    
                }
                yield return new WaitUntil(()=>buttonPressed);

            }
        }
        Animate(null); //End of all dialog reset animation

        objDialogBox.SetActive(false);//Maybe delte later!!!!!IDK   
        button.gameObject.SetActive(false);
        //Player game over here!!!
    }
    IEnumerator Type(DialogData dialogData) //Go through one specific dialog
    {
        objDialogBox.SetActive(true);
        Debug.Log("Drink Reacting..."+dialogData.animType);
        Animate(dialogData.animType);
        foreach (char letter in dialogData.dialog.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(dialogData.pauseTime);
        textDisplay.text = "";
        dialogLoopCor = StartCoroutine(TypeAll());  //Back to main dialog list
    }
    IEnumerator TypeDelay(DialogData dialogData, float delay) //Go through one specific dialog
    {
        yield return new WaitForSeconds(delay);
        objDialogBox.SetActive(true);
        Debug.Log("Drink Reacting..." + dialogData.animType);
        Animate(dialogData.animType);
        foreach (char letter in dialogData.dialog.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(dialogData.pauseTime);
        textDisplay.text = "";
        dialogLoopCor = StartCoroutine(TypeAll());  //Back to main dialog list
    }

    public void NextStage()
    {
        //Stop everything now --------Merge this with part of DrinkTea() to optimize
        textDisplay.text = "";
        StopCoroutine(dialogLoopCor);
        Animate(null);

        //Next Stage
        wrongCount = 0;
        stageIndex++;
        dialogIndex = 0;
        StartCoroutine(StageLoop());
    }
    public void DrinkTea(Tea tea)
    {
        Debug.Log("Drinking tea...");
        Animate("Drink");
        if (tea.numOfPowder == 0 && tea.numOfIngredients == 0)
        {
            //Stop everything now
            textDisplay.text = "";
            StopCoroutine(dialogLoopCor);
            if (reactCor != null)
                StopCoroutine(reactCor);
            reactCor = StartCoroutine(TypeDelay(stageList[stageIndex].TeaReactList[0], 2.1f)); //If you serve him water in stage 1
        }
        else
            Invoke("NextStage", 2f);
    }
    public void Leave()
    {
        //Stop everything now
        textDisplay.text = "";
        StopCoroutine(dialogLoopCor);
        if (reactCor != null)
            StopCoroutine(reactCor);
        reactCor = StartCoroutine(TypeDelay(stageList[stageIndex].TeaReactList[0], 2.1f)); //If you serve him water in stage 1
        GameManager.Instance.GhostLeave();
        gameObject.SetActive(false); // go to next life..........
    }

    public void EatSnack()
    {
        Debug.Log("Snacking..."); // Sensei Always like snack
        sensi_SE.PlayOneShot(happy);
        NextStage();
    }

    void Animate(string animType)
    {
        switch (animType)
        {
            case ("Angry"):
            {
                    print("GETMAD");
                anim.SetBool("isAngry", true);
                    sensi_SE.PlayOneShot(angry);
                    break;
            }
            case ("Happy"):
            {
                anim.SetBool("isHappy", true);
                    sensi_SE.PlayOneShot(happy);
                    break;
            }
            case ("Drink"):
            {
                anim.SetTrigger("toDrinkT");
                    sensi_SE.PlayOneShot(normal);
                    break;
            }
            case ("Leave"):
            {
                anim.SetBool("toLeave", true);
                    sensi_SE.PlayOneShot(normal);
                    break;
            }
            case (null): //empty case, set everything to false!
            {       //It's null so you don't need to write <a> in txt if no animation
                anim.SetBool("toLeave", false);
                anim.SetBool("toFlip", false);
                anim.SetBool("isAngry", false);
                anim.SetBool("isHappy", false);
                break;
            }
        }
    }

    void ReadDialogText()
    {
        //Reading txt
        txtFile = Resources.Load<TextAsset>("Sensei/GhostData"); //Read the file
        List<string> lineTextList = new List<string>(txtFile.text.Split('\n')); //Spilt file to lines

        //Processing txt
        StageData currStage = new StageData(); // empty new stage and dialog to use
        DialogData currDialog = new DialogData();

        for (int i = 0; i < lineTextList.Count; i++)
        {
            lineTextList[i] = Regex.Replace(lineTextList[i], @"\t|\n|\r", "");//Regex to remove the pesky '\r' (<-what a jerk)
        }

        foreach (string lineText in lineTextList)
        {
            if (lineText.Contains("<Stage>"))//Create and store stagedata on <Stage>
            {
                currStage = new StageData();
                stageList.Add(currStage);
                Debug.Log("ReadingReact" + lineText + "...");
            }
            else
            {
                if (lineText.Contains("<p>")) //read and store pausetime
                {
                    currDialog.pauseTime = float.Parse(lineText.Substring(lineText.IndexOf(">") + 1));
                }
                else if (lineText.Contains("<stagePause>")) //read and store pausetime for beginning of stage
                {
                    currStage.stagePause = float.Parse(lineText.Substring(lineText.IndexOf(">") + 1));
                }
                else if (lineText.Contains("<a>")) //read and store animation type
                {
                    currDialog.animType = lineText.Substring(lineText.IndexOf(">") + 1);
                }
                else //read and stor sentence
                {
                    currDialog = new DialogData();
                    currStage.dialogList.Add(currDialog);
                    currDialog.dialog = lineText;
                }
            }
        }
        Debug.Log("Reading Dialog done.");
    }
    void ReadReactText()
    {
        //Reading React txt
        txtFile = Resources.Load<TextAsset>("Sensei/TeaReact"); //Read the file
        List<string> lineTextList = new List<string>(txtFile.text.Split('\n')); //Spilt file to lines

        //Processing txt
        int stageInd = -1; //so the first time it goes to 0
        StageData currStage = new StageData();
        DialogData currDialog = new DialogData();

        for (int i = 0; i < lineTextList.Count; i++)
        {
            lineTextList[i] = Regex.Replace(lineTextList[i], @"\t|\n|\r", "");//Regex to remove the pesky '\r' (<-what a jerk)
        }

        foreach (string lineText in lineTextList)
        {
            if (lineText.Contains("<Stage>"))//Create and store stagedata on <Stage>
            {
                stageInd++;
                currStage = stageList[stageInd];
                Debug.Log("Reading" + lineText + "...");
            }
            else
            {
                if (lineText.Contains("<p>")) //read and store pausetime
                {
                    currDialog.pauseTime = float.Parse(lineText.Substring(lineText.IndexOf(">") + 1));

                }
                else if (lineText.Contains("<a>")) //read and store animation type
                {
                    currDialog.animType = lineText.Substring(lineText.IndexOf(">") + 1);
                }
                else //read and stor sentence
                {
                    currDialog = new DialogData();
                    currStage.TeaReactList.Add(currDialog);
                    currDialog.dialog = lineText;
                }
            }
        }
        Debug.Log("Reading React done.");
    }

    void ReadTutorText()
    {
        //Reading React txt
        txtFile = Resources.Load<TextAsset>("Sensei/GhostData_tutorial"); //Read the file
        List<string> lineTextList = new List<string>(txtFile.text.Split('\n')); //Spilt file to lines

        //Processing txt
        int stageInd = -1; //so the first time it goes to 0
        StageData currStage = new StageData();
        DialogData currDialog = new DialogData();

        for (int i = 0; i < lineTextList.Count; i++)
        {
            lineTextList[i] = Regex.Replace(lineTextList[i], @"\t|\n|\r", "");//Regex to remove the pesky '\r' (<-what a jerk)
        }

        foreach (string lineText in lineTextList)
        {
            if (lineText.Contains("<Stage>"))//Create and store stagedata on <Stage>
            {
                stageInd++;
                currStage = stageList[stageInd];
                Debug.Log("Reading" + lineText + "...");
            }
            else
            {
                if (lineText.Contains("<p>")) //read and store pausetime
                {
                    currDialog.pauseTime = float.Parse(lineText.Substring(lineText.IndexOf(">") + 1));

                }
                else if (lineText.Contains("<a>")) //read and store animation type
                {
                    currDialog.animType = lineText.Substring(lineText.IndexOf(">") + 1);
                }
                else //read and stor sentence
                {
                    currDialog = new DialogData();
                    currStage.TeaTutorList.Add(currDialog);
                    currDialog.dialog = lineText;
                }
            }
        }
        Debug.Log("Reading React done.");
    }

    void ifButtonClicked()
    {
        if (!dialogueOngoing)
        {
            buttonPressed = true;
        }
    }

}

