using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harvesting : MonoBehaviour
{
    public Outline otsc;
    void Start()
    {
        otsc.enabled = false;
    }

    void Update()
    {
        
    }
    void OnMouseOver() {
        otsc.enabled = true;

    }
    void OnMouseExit(){
        otsc.enabled = false;
    }
    void OnMouseDown(){

    }
    

}
