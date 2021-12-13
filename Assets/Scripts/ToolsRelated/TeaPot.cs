﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot : MonoBehaviour
{
    public static TeaPot Instance;
    public string toolName;
    private GameObject thisParent;
    public GameObject mainHolder;
    public GameObject cup;
    public GameObject stovePos;
    public GameObject tpPos;
    public GameObject TPindicator;
    public GameObject Originalindicator;
    public GameObject Stoveindicator;
    public GameObject StovePlaceholderObj;
    [Header("Status")]
    public bool OnteaPot = false;
    public bool canRelease = false;
    public bool pickedUP = false;
    public int state = 0;
    public bool poured = false;
    public bool canClick = true;
    public bool inOriginalPlace;
    public bool canMove =true;
    public bool onStove = false;
    Rigidbody rb;
    [Header("Assignments")]
    public float rotatespeed;
    public float movespeed;
    
    public int speed;
    Vector3 originalPos;
    Vector3 pickUPDes;
    Vector3 targetPos;
    Animator potAnim;
    float degree;
    private GameObject target;
    public GameObject indicatorPt;
    public GameObject indicator;
    public Outline otsc;  //the outline script
    public GameObject toolFirststep;
    Vector3 prevMousePos;
    Vector3 deltaMousePos;
    Vector3 deltaMousePosRot;
    Vector3 deltaMousePosMove;
    Vector3 mousePosPrePour;
    Vector3 mouseStartPos;

    float tiltHStrength = 0.1f;   //0.1
    float tiltVStrength = 0.2f;  //0.2
    float followHStrength = 0.0025f;  //0.0025f
    float followVStrength = 0.005f; //0.005f
    float pX;
    float pY;
    public float heatness = 0;  //current heatness of the pot  will decrease constantly
    Vector3 mPos;
    public SoundManager sc;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        thisParent = this.transform.parent.gameObject;
        mainHolder = thisParent.transform.parent.gameObject;
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(this.transform.position.x, 0.653f, transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 2f, transform.position.z);
        targetPos = new Vector3(cup.transform.position.x,cup.transform.position.y,cup.transform.position.z);
        thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        potAnim = this.GetComponent<Animator>();
        print(thisParent.transform.forward.y* Mathf.Rad2Deg);
        indicator.SetActive(false);
        otsc.enabled = false;
        TPindicator.SetActive(false);
        Stoveindicator.SetActive(false);
        prevMousePos = Input.mousePosition;
        Originalindicator.SetActive(false);
        StovePlaceholderObj.SetActive(true);
        toolFirststep.SetActive(false);
    }
    void Update()
    {
        originalPos = new Vector3(this.transform.position.x, 0.653f, transform.position.z); //update the location constantly
        pickUPDes = new Vector3(this.transform.position.x, 2f, transform.position.z);
        if(TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseTeapot&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT){ //added stuff
        if(canClick&&OnteaPot && Input.GetMouseButton(0)&&state==0){   //pick up pot
            //mouseStartPos = Input.mousePosition;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            toolFirststep.SetActive(false); //tutorial
            Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(true); //tutorial
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
            sc.PickUpTeaPot();
            // canMove = true;  //this canmove to after player completely release after pick up
        }
        }
        if(this.transform.position==pickUPDes){  //when the pot arrived at the top
            canMove = true; 
            state=1;
            if(Input.GetMouseButtonUp(0)){  //Fixed changed pos when hold pot and drag without release in the middle
              //Cursor.lockState = CursorLockMode.None;
              //Cursor.visible = true;
              //InputState.Change(Mouse.current.position, mouseStartPos);
              Tutorial.Instance.NextStep();
              Invoke("PickedUP",0.01f);  //,.5f 
            }
            //Invoke("CanRelease",.25f);  //allow player to release it Originall on
            rb.isKinematic = true;
        }else{
            Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(false); //tutorial: To fix tea pot tutorial glitch
            state = 0;
            //canRelease = false;
            //rb.isKinematic = false;
            //pickedUP = false;
        }
        // if(state==1){
        //     //pickedUP = false;
        // }
        //releasing it
        if(state==1&&canRelease&&Input.GetMouseButton(1)){  //release it change to right click
            canMove = false;   //not to be pushed when release
            indicator.SetActive(false);
            pickedUP = false;  //not turning false at the end
            rb.isKinematic = false;
            canRelease = false;
            Tea.Instance.cupCapacity.SetActive(false);
            Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(false); //tutorial
            Tutorial.Instance.ResetSteps(); //tutorial
            Tutorial.Instance.usedTeaPot = true; //GameManager
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
        }
        if (pickedUP && Input.GetMouseButtonDown(0))
        {
            mousePosPrePour = Input.mousePosition; //get pos before pouring so we can snap to new pos after pouring
        }
        degree = thisParent.transform.forward.y* Mathf.Rad2Deg;
        if (pickedUP && Input.GetMouseButton(0)){    //Mouse Distance based Tilt Pouring here
            //thisParent.transform.rotation *= Quaternion.Euler(deltaMousePos);    
            thisParent.transform.Rotate(deltaMousePosRot); 
            StovePlaceholderObj.SetActive(false);
            if(degree>42){  //teapot pouring sound
                sc.PourTea();
            }
            // sc.PourTea();
        }
        else if (pickedUP&&Input.GetMouseButtonUp(0)){ //when pick up and release right click
            sc.StopPourTea();
            Vector3 tempZ = thisParent.transform.rotation * Vector3.forward; //Im trying to make the direction stay the same but failed....
            thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);  //tempZ.zsnap to this rotation, but keep the z rotation
            Vector3 posFix = -mousePosPrePour + Input.mousePosition;
            thisParent.transform.position += new Vector3(posFix.x * followHStrength, 0f, posFix.y * followVStrength ); //snap to mouse new position    posFix.y * followVStrength why rotating pot changes z pos
            //this.transform.position = pickUPDes; //this line fixed the a bit higer issue
            canClick =true;
            //canRelease = true;
            StovePlaceholderObj.SetActive(true);
            //pickedUP = false;
            
        }
        indicator.transform.position= new Vector3(indicatorPt.transform.position.x,indicator.transform.position.y, indicatorPt.transform.position.z);
        //Mouse Pickup and movement
        if (pickedUP && !Input.GetMouseButton(0))  //added canRelease to resolve issue: get pushed when release
        {
            if(canMove){
                thisParent.transform.position += deltaMousePosMove;
            }
        }
        if(pickedUP){  //also: make it when hovering outside of original pos, player cant release
            TPindicator.SetActive(true);
            Vector3 pos = this.transform.position;
            pos.y = 0.224f;  //table height
            pos.z = this.transform.position.z+0.15f;
            TPindicator.transform.position = pos;
            //TPindicator.transform.position+= deltaMousePosMove;
        }else{
            TPindicator.SetActive(false);
        } 
        //TeaPot heatness
        Tea.Instance.hb.value = heatness;
        if(!onStove&&heatness>0){ //&& offstove
            HeatnessReduce();   
        }
       
    }
    private void LateUpdate()
    {
        deltaMousePos = Input.mousePosition - prevMousePos;
        deltaMousePosRot = new Vector3(0f, deltaMousePos.x*tiltHStrength, 0f);  //the pouring deltaMousePos.y*tiltVStrength
        deltaMousePosMove = new Vector3(deltaMousePos.x*followHStrength,0f, deltaMousePos.y * followVStrength);
        //Change axis so the rotation and move make sense. Also apply multiplier to damp the movement
        prevMousePos = Input.mousePosition;
        //Debug.Log(deltaMousePos);
    }
    void CanRelease(){
        canRelease = true;
    }
    void PickedUP(){
        pickedUP  = true;
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.TEAPOT;
        Tea.Instance.cupCapacity.SetActive(true);
        Tea.Instance.heatBar.SetActive(true);
        Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(true); //tutorial
        //MatchaBox.Instance.TableCollider.SetActive(true);
    }
    void NtonPot(){
        OnteaPot = false;
    }
    void HeatnessReduce(){
        heatness-=0.0005f;
    }
    void OnMouseEnter(){
        OnteaPot = true;
    }
    void OnMouseOver() {
        OnteaPot = true;
        if(!pickedUP&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseTeapot||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay&&!pickedUP){  //originally have: ||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT
            if(canClick){
            otsc.enabled = true;
            toolFirststep.SetActive(true);
            TeaCeremonyManager.Instance.tText = toolName;
            //Tutorial.Instance.currentStepsDisplay = Tutorial.Instance.TPsteps;
            }
        }
    }
    void OnMouseDrag(){  //this or uncomment the part above  //here u have to  click pot and drag to pour
        if(pickedUP){
            indicator.SetActive(false);
            canClick=false;
            if(degree>42){
                //thisParent.transform.Rotate(-Vector3.up*20* rotatespeed * Time.deltaTime);
                //this.transform.Rotate(-Vector3.left*20* rotatespeed * Time.deltaTime);
            }
        }
    }
    void OnMouseExit(){
        otsc.enabled = false;
        toolFirststep.SetActive(false);
        Invoke("NtonPot",.5f);
        TeaCeremonyManager.Instance.tText = "";
        // OnteaPot = false;
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Table"){
            sc.PlaceTeaPot();
            Tea.Instance.heatBar.SetActive(false);
            pickedUP = false;  //not working after using stove is there sth that get turn on again not sensitive?
            canClick =true;
        }
        if(col.gameObject.tag == "Stove"){
            sc.PlaceTeaPot();
            //rb.isKinematic = true;
            onStove = true;
            pickedUP = false;
            Stoveindicator.SetActive(false);
            if(heatness<1){  //if not heated
                canClick =false;   //originally true
                Debug.Log("heating");
                TeaCeremonyManager.Instance.TeaPotHeating();
            }
            if (heatness>=1){
                canClick =true; 
            }
            //thisParent.transform.position = stovePos.transform.position;
            Tutorial.Instance.usedStove = true; //GameManager
        }
    }
    void OnCollisionExit(Collision col) {
        if(col.gameObject.tag == "Stove"){
            onStove = false;
        //     // TeaCeremonyManager.Instance.ResetStove();
        }
    }
    void OnTriggerEnter(Collider col) {
        // if(col.gameObject.tag == "Table"){
        //     print("table");
        //     pickedUP = false;  //not working after using stove
        //     canClick =true;
        //     //TPindicator.SetActive(false);
        // }
        if(col.gameObject.tag == "Stove"){
            thisParent.transform.position = stovePos.transform.position;
            //Stoveindicator.SetActive(true);
        }
        if(col.gameObject.tag == "StoveZone"){
            Stoveindicator.SetActive(true);
            //canRelease = true;
        }
        if(col.gameObject.tag == "TPTrigger"){
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            thisParent.transform.position = tpPos.transform.position;
            //mainHolder.transform.position = tpPos.transform.position;
            TPindicator.transform.position = new Vector3(Originalindicator.transform.position.x,TPindicator.transform.position.y,Originalindicator.transform.position.z);
        }
        if(col.gameObject.tag == "TableZone"){
            Originalindicator.SetActive(true);
            //canRelease = true;
        }
        if(col.gameObject.tag == "Release"){
            canRelease = true;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "TableZone"){
            Originalindicator.SetActive(false);
            //canRelease = false;
        }
        if(col.gameObject.tag == "StoveZone"){
            Stoveindicator.SetActive(false);
            //canRelease = false;
        }
        if(col.gameObject.tag == "Release"){
            canRelease = false;
        }
    }
}
