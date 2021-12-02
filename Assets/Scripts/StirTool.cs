using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Control: player pick it up to certain distance, clickhold to dip down a bit, and stir inthe cup, if release click go back up, right clcik to release 
public class StirTool : MonoBehaviour
{
    public static StirTool Instance;
    public Outline otsc;
    public bool pickedUP = false;
    public bool clicked = false;
    public bool canRelease = false;
    public int speed;
    public int state = 0;  //down
    private GameObject thisParent;
    public GameObject toolTrigger;
    public GameObject OriginalToolPos;
    Vector3 dipPos;  
    Vector3 pickUPDes; //1.076
    Rigidbody rb;
    Vector3 prevMousePos;
    Vector3 deltaMousePos;
    Vector3 deltaMousePosRot;
    Vector3 deltaMousePosMove;
    Vector3 mousePosPrePour;
    float tiltHStrength = 0.7f;   //0.1
    float tiltVStrength = 0.2f;  //0.2
    public float followHStrength = 0.0025f;  //0.0025f
    public float followVStrength = 0.005f; //0.005f
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        otsc.enabled = false;
        rb = this.GetComponent<Rigidbody>();
        dipPos = new Vector3(this.transform.position.x, 0.97f, this.transform.position.z); //0.653f
        //thisParent = this.transform.parent.gameObject;
        pickUPDes = new Vector3(this.transform.position.x, 1.6f, this.transform.position.z);
        prevMousePos = Input.mousePosition;
        toolTrigger.SetActive(false);
    }
    
    void Update()
    {
        dipPos = new Vector3(this.transform.position.x, 0.97f, this.transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 1.6f, this.transform.position.z);
        //CLick and pick it up
        if(state==0&&clicked){
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
            this.transform.rotation = Quaternion.Euler(180f, 0f, 0f); 
        }
        if(this.transform.position==pickUPDes){  //player picked it up
            otsc.enabled = false;
            state = 1;  //up
            rb.isKinematic = true;
            Invoke("PickedUP",.5f);
        }
        //Movement
        if (pickedUP && !Input.GetMouseButton(0))  //moving the tool
        {
            this.transform.position += deltaMousePosMove;
        }
        //RElease it
        if(state==1&&Input.GetMouseButton(1)&&canRelease){  //&&canRelease//later add canRelease boo
            state = 0;
            clicked = false;
            pickedUP = false;
            rb.isKinematic = false;  //its being turned on constantly
            this.transform.rotation = Quaternion.Euler(90f, 0f, 0f); 
        }
        //Dip it & snap back
        if (pickedUP && Input.GetMouseButton(0)){ 
            this.transform.position = dipPos;
            this.transform.position += deltaMousePosMove;
        }else if(pickedUP&&Input.GetMouseButtonUp(0)){
            this.transform.position = pickUPDes;
        }

    }
    private void LateUpdate()
    {
        deltaMousePos = Input.mousePosition - prevMousePos;
        //deltaMousePosRot = new Vector3(deltaMousePos.x*tiltHStrength, 0f, 0f);  
        deltaMousePosMove = new Vector3(deltaMousePos.x*followHStrength,0f, deltaMousePos.y * followVStrength);  //last one was y
        prevMousePos = Input.mousePosition;
    }
    void PickedUP(){
        pickedUP  = true;
        toolTrigger.SetActive(true);
    }
    void OnMouseDown() {
        clicked = true;
    }
    void OnMouseOver() {
        if(!clicked){
            otsc.enabled = true;
        }
    }
    void OnMouseExit() {
        if(!clicked){
            otsc.enabled = false;
        }
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Table"){
            pickedUP = false; 
            toolTrigger.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col) {
        // if(col.gameObject.tag == "Table"){
        //     pickedUP = false;  //why have to wait a while
        //     toolTrigger.SetActive(false);
        // }
        if(col.gameObject.tag == "ToolZone"){
            canRelease  = true;
        }
        if(col.gameObject.tag == "ToolTrigger"){
            this.transform.position = OriginalToolPos.transform.position;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "ToolZone"){
            canRelease  = false;
        }
    }
}
