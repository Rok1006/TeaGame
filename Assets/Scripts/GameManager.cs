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
                TeaCeremonyManager.Instance.startDiming = true;   
                break;
            }
        }
    }
}
