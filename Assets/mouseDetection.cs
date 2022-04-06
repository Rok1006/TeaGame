using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDetection : MonoBehaviour
{
    public bool mouseOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver()
    {
        mouseOver = true;
    }

    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
