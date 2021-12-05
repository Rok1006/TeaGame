﻿using System.Collections;
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
    float dialogBoxPosDown = -178f;
    float dialogBoxPosUp = 160f;

    public float typingSpeed = 0.2f;
    public int stageIndex = 0; //real stage number -1
    public int dialogIndex = 0; //the line we are currently on
    Coroutine dialogLoopCor; //Store so we can stop
    int wrongCount = 0;

    public Animator anim;

    void Start()
    {
        ReadDialogText();
        ReadReactText();
        textDisplay.text = "";
        StartCoroutine(StageLoop());
        anim = GetComponent<Animator>();
        rectTrans = objDialogBox.GetComponent<RectTransform>();
        objDialogBox.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DrinkTea("Bad");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            DrinkTea("Good");
        }

        //DialogBoxPos
        if(CamSwitch.Instance.camState == CamSwitch.CamState.TeaCam)
            rectTrans.anchoredPosition3D = new Vector3(rectTrans.anchoredPosition3D.x,dialogBoxPosUp, rectTrans.anchoredPosition3D.z);
        else if (CamSwitch.Instance.camState == CamSwitch.CamState.ConvCam)
            rectTrans.anchoredPosition3D = new Vector3(rectTrans.anchoredPosition3D.x, dialogBoxPosDown, rectTrans.anchoredPosition3D.z);
    }
    IEnumerator StageLoop() //Main loop for each stage
    {
        Debug.Log("Intro Animation HERE");
        yield return new WaitForSeconds(2f);
        Debug.Log("Start dialog");
        dialogLoopCor = StartCoroutine(TypeAll());

    }
    IEnumerator TypeAll() //Go through every dialog of the stage
    {
        objDialogBox.SetActive(true);
        for(int i=dialogIndex; i<stageList[stageIndex].dialogList.Count; i++)
        {
            DialogData dialogData = stageList[stageIndex].dialogList[i];
            Animate(dialogData.animType);
            foreach (char letter in dialogData.dialog.ToCharArray())
            {
                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(dialogData.pauseTime);
            textDisplay.text = "";
            dialogIndex++;
        }
        Animate(null); //End of all dialog reset animation

        objDialogBox.SetActive(false);//Maybe delte later!!!!!IDK   
        //Player game over here!!!
    }
    IEnumerator Type(DialogData dialogData) //Go through one specific dialog
    {
        objDialogBox.SetActive(true);
        Debug.Log("Drink Reacting...");
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
    void DrinkTea(string teaType)
    {
        Debug.Log("Drink " + teaType);
        if (teaType == "Bad")
        {
            //Stop everything now
            textDisplay.text = "";
            StopCoroutine(dialogLoopCor);
            Animate(null);

            //Start React
            StartCoroutine(Type(stageList[stageIndex].TeaReactList[wrongCount]));//Get dialog based on wrongCount
            wrongCount++;
        }
        if (teaType == "Good")
        {
            //Stop everything now
            textDisplay.text = "";
            StopCoroutine(dialogLoopCor);
            Animate(null);

            //Next Stage
            wrongCount = 0;
            stageIndex++;
            dialogIndex = 0;
            StartCoroutine(StageLoop());
        }
    }

    void Animate(string animType)
    {
        if (!string.IsNullOrEmpty(animType))
        {
            animType = Regex.Replace(animType, @"\t|\n|\r", "");//Regex to remove the pesky '\r' (<-what a jerk)
        }
        switch (animType)
        {
            case ("Flip"):
                {
                    anim.SetBool("toFlip", true);
                    break;
                }
            case (null): //empty case, set everything to false!
                {       //It's null so you don't need to write <a> in txt if no animation
                    anim.SetBool("toFlip", false);
                    break;
                }
        }
    }

    void ReadDialogText()
    {
        //Reading txt
        txtFile = Resources.Load<TextAsset>("GhostData"); //Read the file
        List<string> lineTextList = new List<string>(txtFile.text.Split('\n')); //Spilt file to lines

        //Processing txt
        StageData currStage = new StageData(); // empty new stage and dialog to use
        DialogData currDialog = new DialogData();

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
        Debug.Log("Reading React done.");
    }
    void ReadReactText()
    {
        //Reading React txt
        txtFile = Resources.Load<TextAsset>("TeaReact"); //Read the file
        List<string> lineTextList = new List<string>(txtFile.text.Split('\n')); //Spilt file to lines

        //Processing txt
        int stageInd = 0;
        StageData currStage = new StageData();
        DialogData currDialog = new DialogData();

        foreach (string lineText in lineTextList)
        {
            if (lineText.Contains("<Stage>"))//Create and store stagedata on <Stage>
            {
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
        Debug.Log("Reading done.");
    }
}
