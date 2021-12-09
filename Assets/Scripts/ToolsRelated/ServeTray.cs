using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTray : MonoBehaviour
{
    public Outline oc;
    public GameObject guide;
    void Start()
    {
        oc.enabled = false;
        guide.SetActive(false);
    }

    void Update()
    {
        
    }
    void OnMouseOver() {
        //go up a little
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            oc.enabled = true;
            guide.SetActive(true);
        }
    }

    void OnMouseExit(){
        oc.enabled = false;
        guide.SetActive(false);
    }
    void OnMouseDown() {
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            TeaCeremonyManager.Instance.ServeTea();
        }
    }
}
