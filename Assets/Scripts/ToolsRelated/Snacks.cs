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
    public SoundManager sc;
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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
                sc.PickToolUp();
            }
            else
            {
                GameObject s = Instantiate(snackPrefab, servePos.transform.position,Quaternion.identity) as GameObject;
                currentSnack = s;
                sc.PickToolUp();
            }
            StartCoroutine(SenseiEat());
        }
    }
    IEnumerator SenseiEat(){
        yield return new WaitForSeconds(2f);
        sc.Poof();
        SnackOffer.Instance.snackParticles.SetActive(true);
        if(GameManager.Instance.ghostIndex == 0)
            GameManager.Instance.currGhost.EatSnack();
        else
            GameManager.Instance.stuGhost.EatSnack();
        Destroy(currentSnack.gameObject);
        ServeTray.Instance.occupied = false;
        yield return new WaitForSeconds(3.5f);
        SnackOffer.Instance.snackParticles.SetActive(false);
    }

    void OnMouseExit(){
        oc.enabled = false;
        SnackOffer.Instance.skText = "";
        //snackText.SetActive(false);
    }
}
