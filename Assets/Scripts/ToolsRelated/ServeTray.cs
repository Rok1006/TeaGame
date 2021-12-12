using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeTray : MonoBehaviour
{
    public static ServeTray Instance;
    public Outline oc;
    public GameObject guide;
    public bool occupied = false;
    public bool canServe = false;
    public SoundManager sc;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        oc.enabled = false;
        guide.SetActive(false);
    }

    void Update()
    {
        
    }
    void OnMouseOver() {
        //go up a little
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&!occupied&&canServe){
            oc.enabled = true;
            guide.SetActive(true);
        }
    }

    void OnMouseExit(){
        oc.enabled = false;
        guide.SetActive(false);
    }
    void OnMouseDown() {
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&!occupied&&canServe){
            TeaCeremonyManager.Instance.ServeTea();
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag=="Cup"||col.gameObject.tag=="Snacks"){
            occupied = true;
            sc.ReleaseItem();
        }
        if(col.gameObject.tag=="Cup"){
            
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag=="Cup"||col.gameObject.tag=="Snacks"){
            occupied = false;
        }
        
    }
}
