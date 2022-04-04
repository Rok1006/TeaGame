using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//script of the pickuptool object
//control: u pick up with mouse, move with mouse when click once u dip the tool once into the box, when tool and box meet 
//Done: pickup and move
//Missing: drop it on table, dip to take powder
public class MatchaBox : MonoBehaviour
{
    public static MatchaBox Instance;
    public string toolName;
    public GameObject lid;
    public GameObject matchaBox;
    public GameObject powderPt;
    public GameObject powderPrefab;
    public GameObject currenPowder;
    public GameObject toolTrigger;
    public GameObject OriginalToolPos;
    public GameObject powdernumUI;
    public GameObject placementZone;
    public Text powdernum;
    public Outline otsc;
    public bool pickedUP = false;
    public bool clicked = false;
    public bool inPowderZone =false;
    public bool havePowder = false;
    public bool canRelease = false;
    public int speed;
    public int state = 0;  //down
    Animator mbAnim;
    Vector3 originalPos;  //0.282
    Vector3 pickUPDes; //1.076
    Vector3 dipPos;
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
    Vector3 mPos;
    public GameObject TableCollider;
    public GameObject toolFirststep;
    public SoundManager sc;
    public GameObject MatchaToolIndicate;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        otsc.enabled = false;
        mbAnim = matchaBox.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(1.44f, 0.282f, -2.261f); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 1.076f, this.transform.position.z);
        dipPos = new Vector3(this.transform.position.x, 0.97f, this.transform.position.z); //0.653f
        prevMousePos = Input.mousePosition;
        toolTrigger.SetActive(false);
        TableCollider.SetActive(false);
        powdernumUI.SetActive(false);
        toolFirststep.SetActive(false);
        OriginalToolPos.transform.position = transform.position;
        OriginalToolPos.transform.rotation = transform.rotation;

        placementZone.SetActive(false);
        MatchaToolIndicate.SetActive(false);
    }
    void Update()
    {
        //dipPos = new Vector3(this.transform.position.x, 0.97f, this.transform.position.z); //0.653f
//        Debug.Log(this.transform.rotation.x);
        powdernum.text = "X"+Tea.Instance.numOfPowder.ToString();
        //originalPos = new Vector3(this.transform.position.x, 0.282f, transform.position.z); //0.653f
        //pickUPDes = new Vector3(this.transform.position.x, 1.076f, transform.position.z);
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE&&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UsePowderTool||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay){
        if(state==0&&clicked){
            toolFirststep.SetActive(false); //tutorial
            //Tutorial.Instance.PTsteps[Tutorial.Instance.stepIndex].SetActive(true);
            if(Input.GetMouseButtonDown(0)&&!toolFirststep.activeSelf){ //did only once why keep appearing
                Tutorial.Instance.PTsteps[0].SetActive(true);  //release click to move
                GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            }
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
           
            mbAnim.SetBool("Open", true);
            mbAnim.SetBool("Close", false);
            //sc.OpenLid();
            sc.PickToolUp();
        }
        }
        if(this.transform.position==pickUPDes){  //player picked it up
            //otsc.enabled = false;
            state = 1;  //up
            this.transform.rotation = Quaternion.Euler(-90, 0, -90);
            rb.isKinematic = true;
            if(Input.GetMouseButtonUp(0)){  //Fixed changed pos when hold pot and drag without release in the middle
              //Tutorial.Instance.NextStep();
              Invoke("PickedUP",0.01f);  //,.5f 
            }
        }
        //MOvement
        if (pickedUP && !Input.GetMouseButton(0))  //moving the tool
        {
            //this.transform.position += deltaMousePosMove;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
//                Debug.Log (hit.transform.name);
                mPos = hit.point;
                mPos.y = 1.076f;
              
            }
            this.transform.position = mPos;
        }
        if(pickedUP){  //also: make it when hovering outside of original pos, player cant release
            MatchaToolIndicate.SetActive(true); //off
            Vector3 pos = this.transform.position;
            pos.y = 0.228f;  //table height
            pos.z = this.transform.position.z+0.15f;
            MatchaToolIndicate.transform.position = pos; //off
        }
        //Release it
        if(state==1&&Input.GetMouseButton(1)&&canRelease&&!havePowder){  //later add canRelease bool
            MatchaToolIndicate.SetActive(false);
            state = 0;
            clicked = false;
            pickedUP = false;
            rb.isKinematic = false;
            TableCollider.SetActive(false);
            powdernumUI.SetActive(false);
            transform.rotation = OriginalToolPos.transform.rotation;
            //Tutorial.Instance.PTsteps[Tutorial.Instance.stepIndex].SetActive(false); //tutorial
            Tutorial.Instance.PTsteps[1].SetActive(false);
            Tutorial.Instance.ResetSteps(); //tutorial
            Tutorial.Instance.usedPowderT = true; //GameManager
            GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            if(!Tutorial.Instance.tutorialComplete){GameManager.Instance.arrowAnim.SetTrigger("ingredients");} //this
        }
        //Dip it & snap back
        if (pickedUP && Input.GetMouseButton(0)){    //Mouse Distance based Tilt Pouring here  
           //dip it
           //this.transform.position = dipPos;
         //if (this.transform.rotation.x>-0.51f) {
                this.transform.Rotate(deltaMousePosRot); 
          //}
            if(havePowder){  //if have powder and drag
                Invoke("ReleasePowder",0f);
            }
            // currenPowder.transform.parent = null;
        }else if (pickedUP&&Input.GetMouseButtonUp(0)){ //when pick up and release right click
            havePowder = false;
            Vector3 tempZ = this.transform.rotation * Vector3.forward; //Im trying to make the direction stay the same but failed....
            this.transform.rotation = Quaternion.Euler(-90f, 0f, -90f);  //tempZ.zsnap to this rotation, but keep the z rotation
            Vector3 posFix = -mousePosPrePour + Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {
                Debug.Log (hit.transform.name);
                mPos = hit.point;
                mPos.y = 1.076f;
                Debug.Log (mPos);
            }
            //this.transform.position += new Vector3(0f, 0f, 0f); //snap to mouse new position   posFix.y * followVStrength
            this.transform.position = mPos;
            if(inPowderZone&&havePowder==false){
                powderON();
            }
            inPowderZone = false;
            // if(havePowder){  //if have powder and drag
            //     Invoke("ReleasePowder",5f);
            // }
            // canClick =true;
            // canRelease = true;
        }
        Vector3 centerPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);

    }
    private void LateUpdate()
    {
        deltaMousePos = Input.mousePosition - prevMousePos;
        deltaMousePosRot = new Vector3(deltaMousePos.x*tiltHStrength, 0f, 0f);  
        deltaMousePosMove = new Vector3(deltaMousePos.x*followHStrength,0f, deltaMousePos.y * followVStrength);  //last one was y
        prevMousePos = Input.mousePosition;
    }
    void PickedUP(){
        pickedUP  = true;
        toolTrigger.SetActive(true);
        TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.POWDERTOOL;
        TableCollider.SetActive(true);
        powdernumUI.SetActive(true);
        Tutorial.Instance.PTsteps[1].SetActive(true);
        Tutorial.Instance.PTsteps[0].SetActive(false);
        placementZone.SetActive(true);
        //Tutorial.Instance.PTsteps[Tutorial.Instance.stepIndex].SetActive(true);
    }
    void OnMouseDown() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
        clicked = true;
        }
    }
    void OnMouseOver() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE
            &&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.UsePowderTool
            ||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay&&!pickedUP&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
            if(!clicked){
                otsc.enabled = true;
                mbAnim.SetBool("Open", true);
                mbAnim.SetBool("Close", false);
                //sc.OpenLid();
                //toolFirststep.SetActive(true);
                if(!Tutorial.Instance.PTsteps[0].activeSelf){
                toolFirststep.SetActive(true);
                }
                TeaCeremonyManager.Instance.tText = toolName;
            }
        }
    }
    void OnMouseExit() {
        otsc.enabled = false;
        if(!clicked){
            otsc.enabled = false;
            mbAnim.SetBool("Open", false);
            mbAnim.SetBool("Close", true);
            toolFirststep.SetActive(false);
            TeaCeremonyManager.Instance.tText = "";
        }
    }
    void powderON(){
        currenPowder = Instantiate(powderPrefab, powderPt.transform.position, Quaternion.identity) as GameObject;
        currenPowder.transform.parent = powderPt.transform;
        currenPowder.GetComponent<Rigidbody>().isKinematic = true;
        havePowder = true;  //if fell off cup turn it false
        Invoke("ReleasePowder",1.5f); //if dont hurry up and put in cup it fell off
        
    }
    void ReleasePowder(){
        currenPowder.transform.parent = null;
        currenPowder.GetComponent<Rigidbody>().isKinematic = false;
        //havePowder = false;
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Table"){
            TeaCeremonyManager.Instance.ts.PTsteps[0].SetActive(false);
            // GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            // //if(!Tutorial.Instance.tutorialComplete){GameManager.Instance.arrowAnim.SetTrigger("ingredients");} //this
            mbAnim.SetBool("Open", false);
            mbAnim.SetBool("Close", true);
            pickedUP = false;
            toolTrigger.SetActive(false);
            sc.PlaceToolDown();
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Powder"){
            inPowderZone = true;
        }
        if(col.gameObject.tag == "PtToolZone"){
            canRelease  = true;  //here
        }
        if(col.gameObject.tag == "ToolTrigger"){
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            this.transform.position = OriginalToolPos.transform.position;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "PtToolZone"){
            canRelease  = false;
        }
    }
}
