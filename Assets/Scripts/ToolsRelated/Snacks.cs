using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snacks : MonoBehaviour
{
    public Outline oc;
    public string SnackName;
    void Start()
    {
        oc.enabled = false;
        //sText = "";
    }

    void Update()
    {
    }
    void OnMouseOver() {
        //go up a little
        oc.enabled = true;
        SnackOffer.Instance.skText = SnackName;
        //snackText.SetActive(true);
    }

    void OnMouseExit(){
        oc.enabled = false;
        SnackOffer.Instance.skText = "";
        //snackText.SetActive(false);
    }
}
