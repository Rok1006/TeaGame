using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class dialogue_next_line : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]DialogueRunner runner;
    [SerializeField]LineView line;
    [SerializeField] GameObject scroll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (runner.IsDialogueRunning&& Input.GetMouseButtonDown(0)&& MomentoDisplay.Instance.IfMomentoUP()==false&&scroll.activeSelf==false) 
        { line.OnContinueClicked(); }
    }
}
