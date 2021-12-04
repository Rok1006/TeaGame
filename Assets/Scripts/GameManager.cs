using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tutorial tutor;
    public Ghost currGhost;

    
    void Start()
    {
        
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
            case (0):
            {
                if (TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT)
                {
                    tutor.NextImage();
                    currGhost.NextStage();
                }
                break;
            }
        }
    }
}
