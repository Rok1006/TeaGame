using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//assign this script at some manager or sth
public class MomentoDisplay : MonoBehaviour
{
    public static MomentoDisplay Instance;
    public GameManager GM;
    public GameObject[] momentoPrefab; //1 = student 
    int currentMomento;
    public GameObject currentObj;
    public GameObject appearPt;
    public GameObject MomentoBox;
    public Animator momentoAnim;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        momentoAnim = MomentoBox.GetComponent<Animator>();  //give
    }

    void Update()
    {
        switch(GM.ghostIndex){
            case 0:
                currentMomento = 0;
            break;
            case 1: 
                currentMomento = 1;
            break;
            case 2: 
                currentMomento = 2;
            break;
            case 3: //capitalist
                currentMomento = 3;
            break;
        }

        if(Input.GetKeyDown(KeyCode.P)){ //Testing
            GiveMomento();  //later add this baak in gamemanager at the right line for each ghost
        }
        // if(currentMomento ==null){
        //     momentoAnim.SetTrigger("Close");
        // }
    }
    public void PlaceInMomento(){
        GameObject momento = Instantiate(momentoPrefab[currentMomento], appearPt.transform.position, Quaternion.identity) as GameObject;
        currentObj = momento;
        //var sc = currentObj.GetComponent<Momento>();
        currentObj.SetActive(false);
        currentObj.transform.parent = appearPt.transform;
        //currentObj.transform.eulerAngles = new Vector3(30, 100, -50);
    }
    public void GiveMomento(){
        StartCoroutine(DisplayMomento());
    }
    IEnumerator DisplayMomento(){
        PlaceInMomento();
        yield return new WaitForSeconds(0.3f);
        momentoAnim.SetTrigger("give");
        yield return new WaitForSeconds(0.3f);
        momentoAnim.SetTrigger("open");
        yield return new WaitForSeconds(.3f);
        currentObj.SetActive(true);
    }
}
