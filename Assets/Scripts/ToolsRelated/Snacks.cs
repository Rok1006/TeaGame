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
    public GameObject currentSnack;
    void Start()
    {
        oc.enabled = false;
        //sText = "";
        SnackOffer.Instance.snackParticles.SetActive(false);
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
                currentSnack = s;
            }
            else
            {
                GameObject s = Instantiate(snackPrefab, servePos.transform.position,Quaternion.identity) as GameObject;
                currentSnack = s;
            }
            StartCoroutine(SenseiEat());
        }
    }
    IEnumerator SenseiEat(){
        yield return new WaitForSeconds(2f);
        SnackOffer.Instance.snackParticles.SetActive(true);
        GameManager.Instance.currGhost.EatSnack();
        Destroy(currentSnack.gameObject);
        yield return new WaitForSeconds(3f);
        SnackOffer.Instance.snackParticles.SetActive(false);
    }

    void OnMouseExit(){
        oc.enabled = false;
        SnackOffer.Instance.skText = "";
        //snackText.SetActive(false);
    }
}
