using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Snacks : MonoBehaviour
{
    public Outline oc;
    public string SnackName;
    public GameObject servePos;
    public GameObject snackPrefab;
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
    void OnMouseDown() {
        if(!ServeTray.Instance.occupied){
            CamSwitch.Instance.ConversationCamOn();
            if(this.gameObject.name=="Snack_Buns")
            {
                Vector3 newPos = new Vector3(servePos.transform.position.x+0.23f,servePos.transform.position.y,servePos.transform.position.z);
                GameObject s = Instantiate(snackPrefab, newPos,Quaternion.identity) as GameObject;
            }
            else
            {
                GameObject s = Instantiate(snackPrefab, servePos.transform.position,Quaternion.identity) as GameObject;
            }
            GameManager.Instance.currGhost.EatSnack();
        }
    }

    void OnMouseExit(){
        oc.enabled = false;
        SnackOffer.Instance.skText = "";
        //snackText.SetActive(false);
    }
}
