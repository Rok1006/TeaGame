using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Dialogue_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public LineView lineView;
    public DialogueRunner runner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            runner.StartDialogue("Student_2nd_Phase");
        }
    }
}
