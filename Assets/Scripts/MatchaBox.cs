using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script of the pickuptool object
//control: u pick up with mouse, move with mouse when click once u dip the tool once into the box, when tool and box meet 
//Done: pickup and move
//Missing: drop it on table, dip to take powder
public class MatchaBox : MonoBehaviour
{
    public GameObject lid;
    public GameObject matchaBox;
    public Outline otsc;
    public bool pickedUP = false;
    public bool clicked = false;
    public int speed;
    public int state = 0;  //down
    Animator mbAnim;
    Vector3 originalPos;  //0.282
    Vector3 pickUPDes; //1.076
    Rigidbody rb;
    Vector3 prevMousePos;
    Vector3 deltaMousePos;
    Vector3 deltaMousePosRot;
    Vector3 deltaMousePosMove;
    Vector3 mousePosPrePour;
    float tiltHStrength = 0.1f;   //0.1
    float tiltVStrength = 0.2f;  //0.2
    float followHStrength = 0.0025f;  //0.0025f
    float followVStrength = 0.005f; //0.005f
    void Start()
    {
        otsc.enabled = false;
        mbAnim = matchaBox.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(this.transform.position.x, 0.282f, transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 1.076f, transform.position.z);
        prevMousePos = Input.mousePosition;
    }
    void Update()
    {
        if(state==0&&clicked){
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
        }
        if(this.transform.position==pickUPDes){  //player picked it up
            otsc.enabled = false;
            state = 1;  //up
            rb.isKinematic = true;
            Invoke("PickedUP",.5f); 
        }
        //MOvement
        if (pickedUP && !Input.GetMouseButton(0))  //moving the tool
        {
            this.transform.position += deltaMousePosMove;
        }
        //Release it
        if(state==1&&Input.GetMouseButton(1)){  //later add canRelease bool
            state = 0;
            clicked = false;
            pickedUP = false;
            rb.isKinematic = false;
        }
    }
    private void LateUpdate()
    {
        deltaMousePos = Input.mousePosition - prevMousePos;
        deltaMousePosRot = new Vector3(0f, deltaMousePos.x*tiltHStrength, 0f);  //the pouring deltaMousePos.y*tiltVStrength
        deltaMousePosMove = new Vector3(deltaMousePos.x*followHStrength,0f, deltaMousePos.y * followVStrength);  //last one was y
        prevMousePos = Input.mousePosition;
    }
    void PickedUP(){
        pickedUP  = true;
    }
    void OnMouseDown() {
        clicked = true;
        //tool go up/being pick up literally
    }
    void OnMouseOver() {
        if(!clicked){
            otsc.enabled = true;
            mbAnim.SetBool("Open", true);
            mbAnim.SetBool("Close", false);
        }
    }
    void OnMouseExit() {
        if(!clicked){
            otsc.enabled = false;
            mbAnim.SetBool("Open", false);
            mbAnim.SetBool("Close", true);
        }
    }

}
