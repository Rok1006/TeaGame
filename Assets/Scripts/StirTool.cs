using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirTool : MonoBehaviour
{
    public Outline otsc;
    void Start()
    {
        otsc.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver() {
        otsc.enabled = true;
    }
    void OnMouseExit() {
        otsc.enabled = false;
    }
}
