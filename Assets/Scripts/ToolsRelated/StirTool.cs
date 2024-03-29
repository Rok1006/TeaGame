using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Control: player pick it up to certain distance, clickhold to dip down a bit, and stir inthe cup, if release click go back up, right clcik to release 
public class StirTool : MonoBehaviour
{
    public static StirTool Instance;
    public string toolName;
    public Outline otsc;
    public bool pickedUP = false;
    public bool clicked = false;
    public bool canRelease = false;
    public int speed;
    public int state = 0;  //down
    private GameObject thisParent;
    public GameObject toolTrigger;
    public GameObject OriginalToolPos;
    public GameObject toolFirststep;
    Vector3 dipPos;  
    Vector3 pickUPDes; //1.076
    Vector3 mPos;
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
    public SoundManager sc;
    public GameObject StirToolIndicate;
    //public mouseDetection teaCup;
    bool above = false;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        otsc.enabled = false;
        rb = this.GetComponent<Rigidbody>();
        dipPos = new Vector3(this.transform.position.x, 0.97f, this.transform.position.z); //0.653f
        //thisParent = this.transform.parent.gameObject;
        pickUPDes = new Vector3(this.transform.position.x, 1.6f, this.transform.position.z);
        prevMousePos = Input.mousePosition;
        toolTrigger.SetActive(false);
        toolFirststep.SetActive(false);
        MatchaBox.Instance.placementZone.SetActive(false);
        StirToolIndicate.SetActive(false);
        
    }
    
    void Update()
    {
        dipPos = new Vector3(this.transform.position.x, 0.97f, this.transform.position.z); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 1.6f, this.transform.position.z);
        //CLick and pick it up
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseStirTool||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay){
        if(state==0&&clicked&&TeaCeremonyManager.Instance.CurrentToolName == "StirTool"){
            toolFirststep.SetActive(false); //tutorial
            // Tutorial.Instance.STsteps[Tutorial.Instance.stepIndex].SetActive(true);
            if(Input.GetMouseButtonDown(0)&&!toolFirststep.activeSelf){ //did only once why keep appearing
                Tutorial.Instance.STsteps[0].SetActive(true);  //release click to move
                GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            }
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
            this.transform.rotation = Quaternion.Euler(180f, 0f, 0f); 
            sc.PickToolUp();
        }
        }
        if(this.transform.position==pickUPDes){  //player picked it up
            otsc.enabled = false;
            state = 1;  //up
            rb.isKinematic = true;
            if(Input.GetMouseButtonUp(0)){  //Fixed changed pos when hold pot and drag without release in the middle
              //Tutorial.Instance.NextStep();
              Invoke("PickedUP",0.01f);   //,.5f
            }
        }
        //Movement
        if (pickedUP && !Input.GetMouseButton(0))  //moving the tool
        {
            // this.transform.position += deltaMousePosMove;
            Plane plane = new Plane(Vector3.up, new Vector3(0, 2, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance);
            }
        }
        if(pickedUP){  //also: make it when hovering outside of original pos, player cant release
            StirToolIndicate.SetActive(true); //off
            Vector3 pos = this.transform.position;
            pos.y = 0.228f;  //table height , .19
            pos.z = this.transform.position.z+0.15f;
            StirToolIndicate.transform.position = pos; //off
        }
        //RElease it
        if(state==1&&Input.GetMouseButton(1)&&canRelease){  //&&canRelease//later add canRelease boo
            StirToolIndicate.SetActive(false);
            state = 0;
            clicked = false;
            pickedUP = false;
            rb.isKinematic = false;  //its being turned on constantly
            this.transform.rotation = Quaternion.Euler(90f, 0f, 0f); 
            Tutorial.Instance.STsteps[1].SetActive(false);
            Tutorial.Instance.ResetSteps(); //tutorial
            Tutorial.Instance.usedStirT = true; //GameManager
        }
        //Dip it & snap back
        // if (teaCup.mouseOver)
        // {
            if (pickedUP && Input.GetMouseButton(0))
            {
                this.transform.position = dipPos;
                this.transform.position += deltaMousePosMove;
                //cannot go out of cup
                //this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -0.296f,0.296f),transform.position.y, Mathf.Clamp(transform.position.z, -1.964f,-1.23f));
                //if(above){
                Vector3 circleCenter = new Vector3(0, 0, -1.657f);
                Vector3 v = this.transform.position - circleCenter;
                v = Vector3.ClampMagnitude(v, .9f);
                this.transform.position = circleCenter + v;
                //}
            }
            else if (pickedUP && Input.GetMouseButtonUp(0))
            {
                //this.transform.position = pickUPDes;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Debug.Log(hit.transform.name);
                    mPos = hit.point;
                    mPos.y = 1.6f;
                }
                this.transform.position = mPos;
            }
       // }

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
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.STIRTOOL;
        Tea.Instance.TeaState();
        Tutorial.Instance.STsteps[1].SetActive(true);
        Tutorial.Instance.STsteps[0].SetActive(false);
         MatchaBox.Instance.placementZone.SetActive(true);
    }
    void OnMouseDown() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
        clicked = true;
        }
    }
    void OnMouseOver() {
        if(!clicked&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UseStirTool||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay&&!clicked&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            otsc.enabled = true;
            TeaCeremonyManager.Instance.CurrentToolName = "StirTool";
            //toolFirststep.SetActive(true);
            if(!Tutorial.Instance.STsteps[0].activeSelf){
            toolFirststep.SetActive(true);
            }
            TeaCeremonyManager.Instance.tText = toolName;
        }
    }
    void OnMouseExit() {
        if(!clicked){
            otsc.enabled = false;
            toolFirststep.SetActive(false);
            TeaCeremonyManager.Instance.tText = "";
        }
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Table"){
            TeaCeremonyManager.Instance.ts.STsteps[0].SetActive(false);
            pickedUP = false; 
            toolTrigger.SetActive(false);
            sc.PlaceToolDown();
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "ToolZone"){
            canRelease  = true;
        }
        if(col.gameObject.tag == "ToolTrigger"){
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            this.transform.position = OriginalToolPos.transform.position;
        }
        if(col.gameObject.tag == "StirrTool"){
            above = true;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "ToolZone"){
            canRelease  = false;
        }
        if(col.gameObject.tag == "StirrTool"){
            above = false;
        }
    }
}
