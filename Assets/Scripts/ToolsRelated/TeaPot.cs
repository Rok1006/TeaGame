using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaPot : MonoBehaviour
{
    public static TeaPot Instance;
    public string toolName;


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
    public int upState = 0;
    Vector3 mousePos;
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
    float originalHeight = 0.653f;
    float destHeight = 2f;
    public float degree;
    public int pouringDegree = 42; //degree when it is pouring
    private GameObject target;
    public GameObject indicatorPt;
    public GameObject indicator;
    public Outline otsc;  //the outline script
    public GameObject toolFirststep;
    public GameObject TPZone;
    public GameObject TPTrigger;
    public GameObject StoveZone;
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
    bool moveup;
    
    public float heatness = 0;  //current heatness of the pot  will decrease constantly
    Vector3 mPos;
    public SoundManager sc;
    Quaternion original_rotation;
    void Awake() {
        Instance = this;
    }
    void Start()
    {

        moveup = false;
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(this.transform.position.x, originalHeight, transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, destHeight, transform.position.z);
        targetPos = new Vector3(cup.transform.position.x,cup.transform.position.y,cup.transform.position.z);
        original_rotation = this.transform.rotation;
        this.transform.rotation = original_rotation;
        potAnim = this.GetComponent<Animator>();
        print(this.transform.forward.y* Mathf.Rad2Deg);
        indicator.SetActive(false);
        otsc.enabled = false;
        TPindicator.SetActive(false);
        Stoveindicator.SetActive(false);
        prevMousePos = Input.mousePosition;
        Originalindicator.SetActive(false);
        StovePlaceholderObj.SetActive(true);
        toolFirststep.SetActive(false);
        TPZone.SetActive(false);
        TPTrigger.SetActive(false);
        //StoveZone.SetActive(false);
       
    }
    void FixedUpdate()
    {
        /*
          originalPos = transform.position; //update the location constantly
       */
       pickUPDes = new Vector3(this.transform.position.x, destHeight, transform.position.z);
        
        if (TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseTeapot&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT){ //added stuff
        if(canClick && OnteaPot && Input.GetMouseButton(0)&&upState==0)
        {   //pick up pot
                rb.isKinematic = true;
                //rb.useGravity = false;
                canClick = false;
                toolFirststep.SetActive(false); //tutorial
                moveup = true;
                //Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(true); //tutorial
              if(toolFirststep.activeSelf)
                { 
                GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
                Tutorial.Instance.TPsteps[0].SetActive(true);  //release click to move
                }
       
            sc.PickUpTeaPot();
        }
        }

        if(transform.position==pickUPDes){  //when the pot arrived at the top
            moveup = false;
            pickedUP = true;
            canMove = true; 
            
            upState=1; //upState refers to the upState of being picked up or not
            if(Input.GetMouseButtonUp(0)){  //Fixed changed pos when hold pot and drag without release in the middle
              //Tutorial.Instance.NextStep();  turn on next toool step here originally
              Invoke("PickedUP",0.01f);  //,.5f 
              GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
              if(!Tutorial.Instance.tutorialComplete){GameManager.Instance.arrowAnim.SetTrigger("stove");}
            }
            //rb.isKinematic = true;
        }else if(transform.position != pickUPDes&&moveup)
        {
            //Debug.Log("not yet");
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pickUPDes, step);
            //Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(false); //tutorial: To fix tea pot tutorial glitch
          //  upState = 0; //not yet to the top
        }
        //releasing it
       
        if(pickedUP&&canRelease&&Input.GetMouseButton(1)){  //release it change to right click
           
            canMove = false;   //not to be pushed when release
            indicator.SetActive(false);
            transform.rotation = original_rotation;
            pickedUP = false;
            canClick = true;
            rb.isKinematic = false;
            canRelease = false;
            Tea.Instance.cupCapacity.SetActive(false);
            //Tutorial.Instance.TPsteps[Tutorial.Instance.stepIndex].SetActive(false); //tutorial
            Tutorial.Instance.TPsteps[1].SetActive(false);
            Tutorial.Instance.ResetSteps(); //tutorial
            Tutorial.Instance.usedTeaPot = true; //GameManager
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;

        }
        
        degree = this.transform.forward.y* Mathf.Rad2Deg;
       
        if (pickedUP && Input.GetMouseButton(0)){    //Mouse Distance based Tilt Pouring here
            mousePosPrePour = Input.mousePosition;
           transform.RotateAround(transform.position,Vector3.forward, deltaMousePos.x * tiltHStrength); 
            StovePlaceholderObj.SetActive(false);
            if(degree>pouringDegree){  //teapot pouring sound
                sc.PourTea();
                //Tea.Instance.isPouring = true;
            }
            else
            {
                //Tea.Instance.isPouring = false;
            }
        }else if (pickedUP&&Input.GetMouseButtonUp(0)){ //when pick up and release left click
            sc.StopPourTea();
            //  Vector3 tempZ = thisParent.transform.rotation * Vector3.forward; //Im trying to make the direction stay the same but failed....
            //tempZ.zsnap to this rotation, but keep the z rotation
            transform.rotation = original_rotation;
         
            // this.transform.rotation= Quaternion.Euler(0,0,0);
            Vector3 posFix = -mousePosPrePour + Input.mousePosition;
           // thisParent.transform.position += new Vector3(posFix.x * followHStrength, 0f, posFix.y * followVStrength ); //snap to mouse new position    posFix.y * followVStrength why rotating pot changes z pos
            canClick =true;
            StovePlaceholderObj.SetActive(true);
            
        }
        indicator.transform.position= new Vector3(indicatorPt.transform.position.x,indicator.transform.position.y, indicatorPt.transform.position.z);
        //Mouse Pickup and movement
    if (pickedUP && !Input.GetMouseButton(0))  //added canRelease to resolve issue: get pushed when release
        {
            if(canMove){
                Plane plane = new Plane(Vector3.up, new Vector3(0, 2, 0));
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float distance;
                if (plane.Raycast(ray, out distance))
                {
                    transform.position = ray.GetPoint(distance);
                }
            }
        }
        if(pickedUP){  //also: make it when hovering outside of original pos, player cant release
            //TPindicator.SetActive(true); off
            Vector3 pos = this.transform.position;
            pos.y = 0.224f;  //table height
            pos.z = this.transform.position.z+0.15f;
            //TPindicator.transform.position = pos; off
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
    }
    void CanRelease(){
        canRelease = true;
    }
    void PickedUP(){
        pickedUP  = true;
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.TEAPOT;
        Tea.Instance.cupCapacity.SetActive(false);
        Tea.Instance.heatBar.SetActive(true);
        Tutorial.Instance.TPsteps[1].SetActive(true);
        Tutorial.Instance.TPsteps[0].SetActive(false);
        TPZone.SetActive(true);
        TPTrigger.SetActive(true);
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
        if(!pickedUP&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseTeapot||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay&&!pickedUP&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){  //originally have: ||TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.TEAPOT
            if(canClick){
            otsc.enabled = true;
            if(!Tutorial.Instance.TPsteps[0].activeSelf){
            toolFirststep.SetActive(true);
            }
            TeaCeremonyManager.Instance.tText = toolName;
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
    }
    void OnCollisionEnter(Collision col) {
        Debug.Log("collider:"+col);
        if(col.gameObject.tag == "Table"){
            TPZone.SetActive(false);
            TPTrigger.SetActive(false);
            Tutorial.Instance.TPsteps[0].SetActive(false);
            upState=0;
            sc.PlaceTeaPot();
            Tea.Instance.heatBar.SetActive(false);
            pickedUP = false; 
            canClick =true;
        }
        if(col.gameObject.tag == "Stove"){
            GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            if(!Tutorial.Instance.tutorialComplete){GameManager.Instance.arrowAnim.SetTrigger("powder");}
            Tutorial.Instance.TPsteps[0].SetActive(false);
            sc.PlaceTeaPot();
            upState=0;
            onStove = true;
            pickedUP = false;
            //StoveZone.SetActive(false); // //new
            Stoveindicator.SetActive(false);
            if(heatness<1){  //if not heated
           // upState=0;
                canClick =false;  
                Debug.Log("heating");
                TeaCeremonyManager.Instance.TeaPotHeating();
            }
            if (heatness>=1){
                canClick =true; 
               // upState=0;
            }
            //thisParent.transform.position = stovePos.transform.position;
            Tutorial.Instance.usedStove = true; //GameManager
        }
    }
    void OnCollisionExit(Collision col) {
        if(col.gameObject.tag == "Stove"){
            TeaCeremonyManager.Instance.StopTeaPotSteam();
            GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            onStove = false;
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Stove"){
            this.transform.position = stovePos.transform.position;
        }
        if(col.gameObject.tag == "StoveZone"){
            Stoveindicator.SetActive(true);

        }
        if(col.gameObject.tag == "TPTrigger"){
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            this.transform.position = tpPos.transform.position;
            //TPindicator.transform.position = new Vector3(Originalindicator.transform.position.x,TPindicator.transform.position.y,Originalindicator.transform.position.z); off
        }
        if(col.gameObject.tag == "TableZone"){
            Originalindicator.SetActive(true);
        }
        if(col.gameObject.tag == "Release"){
            canRelease = true;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "TableZone"){
            Originalindicator.SetActive(false);
        }
        if(col.gameObject.tag == "StoveZone"){
            Stoveindicator.SetActive(false);
        }
        if(col.gameObject.tag == "Release"){
            canRelease = false;
        }
   
    }
}
