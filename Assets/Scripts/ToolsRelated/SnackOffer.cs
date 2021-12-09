using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackOffer : MonoBehaviour
{
    public Outline otsc;
    public GameObject tutorial;
    void Start()
    {
        otsc.enabled = false;
        tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            otsc.enabled = true;
            tutorial.SetActive(true);
        }
    }
    void OnMouseExit() {
        otsc.enabled = false;
        tutorial.SetActive(false);
    }
    void OnMouseDown() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            tutorial.SetActive(false);
            CamSwitch.Instance.CupboardCamOn();
        }
    }
    public void GoBack(){

    }
}
