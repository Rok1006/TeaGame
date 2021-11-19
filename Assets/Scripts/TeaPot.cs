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
    // private Vector3 moffset;
    // private float mZCoord;
    void Start()
    {
        thisParent = this.transform.parent.gameObject;
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(this.transform.position.x, 0.653f, transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 1.7f, transform.position.z);
        targetPos = new Vector3(cup.transform.position.x,cup.transform.position.y,cup.transform.position.z);
        thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        potAnim = this.GetComponent<Animator>();
        print(thisParent.transform.forward.y* Mathf.Rad2Deg);
        //target.transform.
    }
    //control: left click to pick pot up, right click to rotate> left click to release, wasd to move
    //second option: have three icons on top pf pot for player to toggle on right click, left click to take action
    //or two icons to toggle: rotate or tilt
    //another option remove the ability to rotate
    void Update()
    {
        originalPos = new Vector3(this.transform.position.x, 0.653f, transform.position.z); //update the location constantly
        pickUPDes = new Vector3(this.transform.position.x, 1.7f, transform.position.z);
        if(canClick&&OnteaPot && Input.GetMouseButton(0)&&state==1){
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
            // canRelease = true;
            //rb.isKinematic = true;
            pickedUP = true;
        }
        if(this.transform.position==pickUPDes){  //when the pot arrived at the top
            state=0;
            //OnteaPot=false;
            //canRelease = true;
            pickedUP = true;
            Invoke("CanRelease",.25f);  //allow player to release it
            rb.isKinematic = true;
        }else{
            state = 1;
            canRelease = false;
        }
        if(state==0&&canRelease&&Input.GetMouseButton(0)){  //release it
            rb.isKinematic = false;
            pickedUP = false;
            canRelease = false;
            //state=0;
        }
        degree = thisParent.transform.forward.y* Mathf.Rad2Deg;
        if(pickedUP && Input.GetMouseButton(1)){  //rightClick to rotate/tilt
            canClick=false;
            print(thisParent.transform.forward.y* Mathf.Rad2Deg);
            //float degree = thisParent.transform.forward.y* Mathf.Rad2Deg;
            if(degree>42){
                thisParent.transform.Rotate(-Vector3.up*30* rotatespeed * Time.deltaTime);
            }
        }else if (pickedUP&&Input.GetMouseButtonUp(1)){ //it did only once      //pickedupbool is not very accurate
            thisParent.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            canClick=true;
        }
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
        if(Input.GetKeyDown(KeyCode.E)){ //to tilt
            //transform.rotation = lookAtSlowly(transform , new Vector3(-136f,0,0) , 1);
            //degree+=1f;
        }
        if(state==0 && Input.GetKey(KeyCode.W)){  //up in game
           thisParent.transform.Translate(Vector3.down*movespeed*Time.deltaTime);
        }
        if(state==0 && Input.GetKey(KeyCode.S)){ //down in game
           thisParent.transform.Translate(Vector3.up*movespeed*Time.deltaTime);
        }
        if(state==0 && Input.GetKey(KeyCode.A)){  //up in game
           thisParent.transform.Translate(Vector3.left*movespeed*Time.deltaTime);
        }
        if(state==0 && Input.GetKey(KeyCode.D)){ //down in game
           thisParent.transform.Translate(Vector3.right*movespeed*Time.deltaTime);
        }
        //thisParent.transform.rotation = Quaternion.Euler(-89.98f, 0f, 0f);
        //RotateTowardsCup();
        
    }
    // Quaternion lookAtSlowly(Transform t , Vector3 target , float speed){
    //     Vector3 relativePos = target - t.position;
    //     Quaternion toRotation = Quaternion.LookRotation(relativePos);
    //     return Quaternion.Lerp(t.rotation, toRotation, speed * Time.deltaTime);
    // }
    void CanRelease(){
        canRelease = true;
    }
    void OnMouseDown(){
        //pickUP = true;
        // mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        // moffset = thisParent.transform.position - GetMouseWorldPos();
        // float step = speed * Time.deltaTime;
        // this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
        //this.transform.Translate(new Vector3(transform.position.x, 1.7f, transform.position.z) * speed * Time.deltaTime);
    }
    void OnMouseEnter(){
        OnteaPot = true;
    }
    void OnMouseExit(){
        //OnteaPot = false;
    }
    //LANDFILL
    // void RotateTowardsCup(){
    //     float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
    //     Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatespeed * Time.deltaTime);
    // }
    // private Vector3 GetMouseWorldPos(){
    //     Vector3 mousePoint = Input.mousePosition;
    //     mousePoint.z = mZCoord;
    //     return Camera.main.ScreenToWorldPoint(mousePoint);
    // }
                // if(thisParent.transform.rotation.x!=-140f){ //it instantly stop because of line 61 it changes state to 1 preventing this part to happen
            //     //this.transform.Rotate(Vector3.up * -50* rotatespeed * Time.deltaTime);
            //     thisParent.transform.Rotate(-Vector3.up*30* rotatespeed * Time.deltaTime);
            // }
}
