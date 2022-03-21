using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        otsc.enabled = false;

    }
    void OnMouseExit(){
        otsc.enabled = false;
    }
    void OnMouseDown(){

    }
    

}
