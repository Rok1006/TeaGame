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
    void Start()
    {
        thisParent = this.transform.parent.gameObject;
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(this.transform.position.x, 0.653f, transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 2f, transform.position.z);
        targetPos = new Vector3(cup.transform.position.x,cup.transform.position.y,cup.transform.position.z);
        thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        potAnim = this.GetComponent<Animator>();
        print(thisParent.transform.forward.y* Mathf.Rad2Deg);
        indicator.SetActive(false);
        otsc.enabled = false;
    }
    //control: left click to pick pot up, right click to rotate> left click to release, wasd to move
    //second option: have three icons on top pf pot for player to toggle on right click, left click to take action
    //or two icons to toggle: rotate or tilt
    //another option remove the ability to rotate
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
            //state=0;
        }
        // else if(state==0&&Input.GetMouseButton(1)){
        //     pickedUP = false;
        // }
        //Rotating the pot to pour
        degree = thisParent.transform.forward.y* Mathf.Rad2Deg;
        if(pickedUP && Input.GetMouseButton(0)){  //leftClick to rotate/tilt
            // canClick=false;
            // if(degree>42){
            //     thisParent.transform.Rotate(-Vector3.up*30* rotatespeed * Time.deltaTime);
            // }
        }else if (pickedUP&&Input.GetMouseButtonUp(0)){ //it did only once      //original mouse 1   only have to click on the pot to pour
            // this.transform.position = pickUPDes;
            thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            //canRelease = true; //
            canClick=true;
        }
        indicator.transform.position= new Vector3(indicatorPt.transform.position.x,indicator.transform.position.y, indicatorPt.transform.position.z);
        //Triangle.transform.position.z = indicatorPt.transform.position.z;
        // if(poured){
        //     print(degree);
        //     if(degree<-59.29579f){
        //         thisParent.transform.Rotate(Vector3.up*30* rotatespeed * Time.deltaTime);
        //     }
        // }
        // if(degree>59){
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
        if(state==0&& Input.GetKey(KeyCode.W)){  //up in game
           thisParent.transform.Translate(Vector3.down*movespeed*Time.deltaTime);
        }
        if(state==0&& Input.GetKey(KeyCode.S)){ //down in game
           thisParent.transform.Translate(Vector3.up*movespeed*Time.deltaTime);
        }
        if(state==0&& Input.GetKey(KeyCode.A)){  //up in game
           thisParent.transform.Translate(Vector3.left*movespeed*Time.deltaTime);
        }
        if(state==0&& Input.GetKey(KeyCode.D)){ //down in game
           thisParent.transform.Translate(Vector3.right*movespeed*Time.deltaTime);
        }
        //thisParent.transform.rotation = Quaternion.Euler(-89.98f, 0f, 0f);
        
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
                thisParent.transform.Rotate(-Vector3.up*20* rotatespeed * Time.deltaTime);
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
