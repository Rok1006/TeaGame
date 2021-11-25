using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot : MonoBehaviour
{
    public int speed;
    Vector3 originalPos;
    Vector3 pickUPDes;
    Vector3 targetPos;
    private GameObject thisParent;
    public GameObject mainHolder;
    [Header("Status")]
    public bool OnteaPot = false;
    public bool canRelease = false;
    public bool pickedUP = false;
    public int state = 0;
    public bool poured = false;
    public bool canClick = true;
    Rigidbody rb;
    [Header("Assignments")]
    public float rotatespeed;
    public float movespeed;
    public GameObject cup;
    //public float degree = 0f;
    Animator potAnim;
    float degree;
    private GameObject target;
    public GameObject indicatorPt;
    public GameObject indicator;
    public Outline otsc;  //the outline script

    Vector3 prevMousePos;
    Vector3 deltaMousePos;
    float tiltHStrength = 0.1f;
    float tiltVStrength = 0.2f;
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

        prevMousePos = Input.mousePosition;
    }
    void Update()
    {
        originalPos = new Vector3(this.transform.position.x, 0.653f, transform.position.z); //update the location constantly
        pickUPDes = new Vector3(this.transform.position.x, 2f, transform.position.z);
        if(canClick&&OnteaPot && Input.GetMouseButton(0)&&state==1){   //pick up pot
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
            // canRelease = true;
            //rb.isKinematic = true;
            //pickedUP = true;
        }
        if(this.transform.position==pickUPDes){  //when the pot arrived at the top
            state=0;
            //OnteaPot=false;
            //canRelease = true;
            //pickedUP = true;
            Invoke("PickedUP",.5f); 
            Invoke("CanRelease",.25f);  //allow player to release it
            rb.isKinematic = true;
            //indicator.SetActive(true);   //uncomment this if need it
        }else{
            state = 1;
            canRelease = false;
            //pickedUP = false;
        }
        if(state==1){
            //pickedUP = false;
        }
        //releasing it
        if(state==0&&canRelease&&Input.GetMouseButton(1)){  //release it change to right click
            indicator.SetActive(false);
            pickedUP = false;  //not turning false at the end
            rb.isKinematic = false;
            canRelease = false;
        }
        //Rotating the pot to pour
        //degree = thisParent.transform.forward.y* Mathf.Rad2Deg;
        //print(degree);
        if(pickedUP && Input.GetMouseButton(0)){    //Mouse Distance Tilt Pouring here
            //thisParent.transform.rotation *= Quaternion.Euler(deltaMousePos);    
            thisParent.transform.Rotate(deltaMousePos);
        }
        else if (pickedUP&&Input.GetMouseButtonUp(0)){ //original mouse 1   only have to click on the pot to pour
            // this.transform.position = pickUPDes;
            // if(degree<57){
            //     thisParent.transform.Rotate(-Vector3.up*20* rotatespeed * Time.deltaTime);
            // }
            Vector3 tempZ = thisParent.transform.rotation * Vector3.forward;
            thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, tempZ.z);  //snap to this rotation, but keep the y rotation
            //thisParent.transform.Rotate(0f,0f,tempZ.z*360);
            canClick =true;
        }
        indicator.transform.position= new Vector3(indicatorPt.transform.position.x,indicator.transform.position.y, indicatorPt.transform.position.z);
        //Triangle.transform.position.z = indicatorPt.transform.position.z;
        
        if(pickedUP&& Input.GetKey(KeyCode.W)){  //up in game   state==0
           mainHolder.transform.Translate(Vector3.down*movespeed*Time.deltaTime);
        }
        if(pickedUP&& Input.GetKey(KeyCode.S)){ //down in game
           mainHolder.transform.Translate(Vector3.up*movespeed*Time.deltaTime);
        }
        if(pickedUP&& Input.GetKey(KeyCode.A)){  //up in game
           mainHolder.transform.Translate(Vector3.left*movespeed*Time.deltaTime);
        }
        if(pickedUP&& Input.GetKey(KeyCode.D)){ //down in game
           mainHolder.transform.Translate(Vector3.right*movespeed*Time.deltaTime);  //thisParent
        }
        //thisParent.transform.rotation = Quaternion.Euler(-89.98f, 0f, 0f);
    }

    private void LateUpdate()
    {
        deltaMousePos = Input.mousePosition - prevMousePos;
        deltaMousePos = new Vector3(0f, deltaMousePos.x*tiltHStrength, deltaMousePos.y*tiltVStrength); 
        //Change axis so so the rotation make sense. Also apply multiplier to damp the movement
        prevMousePos = Input.mousePosition;
        Debug.Log(deltaMousePos);
    }

    void CanRelease(){
        canRelease = true;
    }
    void PickedUP(){
        pickedUP  = true;
    }
    void OnMouseDown(){
    }
    void OnMouseEnter(){
        OnteaPot = true;
    }
    void OnMouseOver() {
        if(!pickedUP){
            otsc.enabled = true;
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
        // if(pickedUP){
        //     this.transform.position = pickUPDes;
        // }
        //OnteaPot = false;
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Table"){
            pickedUP = false;
        }
    }
}
//// if(poured){
        //         thisParent.transform.Rotate(-Vector3.up*20* rotatespeed * Time.deltaTime);
        // }
        // if(degree<57){
        //     poured = false;
        // }
        // if(pickedUP && Input.GetMouseButtonUp(1)){
        //     float degree = thisParent.transform.forward.y* Mathf.Rad2Deg;
        //     if(degree<59.29579f){
        //         thisParent.transform.Rotate(Vector3.up*30* rotatespeed * Time.deltaTime);
        //     }
        //     //poured = false;
        //     //print("not pouring");
        // }
        // if(Input.GetKeyDown(KeyCode.E)){ //to tilt
        //     //transform.rotation = lookAtSlowly(transform , new Vector3(-136f,0,0) , 1);
        //     //degree+=1f;
        // }