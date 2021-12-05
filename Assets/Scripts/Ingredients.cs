using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
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
        oc.enabled = true;
    }

    void OnMouseExit(){
        oc.enabled = false;
    }
}
