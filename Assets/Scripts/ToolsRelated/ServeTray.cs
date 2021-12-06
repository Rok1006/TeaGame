using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTray : MonoBehaviour
{
    public Outline oc;
    void Start()
    {
        oc.enabled = false;
    }

    void Update()
    {
        
    }
    void OnMouseOver() {
        //go up a little
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            oc.enabled = true;
        }
    }

    void OnMouseExit(){
        oc.enabled = false;
    }
    void OnMouseDown() {
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            TeaCeremonyManager.Instance.ServeTea();
        }
    }
}
