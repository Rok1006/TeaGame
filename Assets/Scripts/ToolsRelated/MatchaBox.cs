using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//script of the pickuptool object
//control: u pick up with mouse, move with mouse when click once u dip the tool once into the box, when tool and box meet 
//Done: pickup and move
//Missing: drop it on table, dip to take powder
public class MatchaBox : MonoBehaviour
{
    public static MatchaBox Instance;
    public GameObject lid;
    public GameObject matchaBox;
    public GameObject powderPt;
    public GameObject powderPrefab;
    public GameObject currenPowder;
    public GameObject toolTrigger;
    public GameObject OriginalToolPos;
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
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        otsc.enabled = false;
        mbAnim = matchaBox.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        originalPos = new Vector3(1.44f, 0.282f, -2.261f); //0.653f
        pickUPDes = new Vector3(this.transform.position.x, 1.076f, this.transform.position.z);
        prevMousePos = Input.mousePosition;
        toolTrigger.SetActive(false);
        TableCollider.SetActive(false);
    }
    void Update()
    {
        //originalPos = new Vector3(this.transform.position.x, 0.282f, transform.position.z); //0.653f
        //pickUPDes = new Vector3(this.transform.position.x, 1.076f, transform.position.z);
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
        if(state==0&&clicked){
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
            mbAnim.SetBool("Open", true);
            mbAnim.SetBool("Close", false);
        }
        }
        if(this.transform.position==pickUPDes){  //player picked it up
            otsc.enabled = false;
            state = 1;  //up
            rb.isKinematic = true;
            if(Input.GetMouseButtonUp(0)){  //Fixed changed pos when hold pot and drag without release in the middle
              Invoke("PickedUP",0.01f);  //,.5f 
            }
        }
        //MOvement
        if (pickedUP && !Input.GetMouseButton(0))  //moving the tool
        {
            this.transform.position += deltaMousePosMove;
        }
        //Release it
        if(state==1&&Input.GetMouseButton(1)&&canRelease&&!havePowder){  //later add canRelease bool
            state = 0;
            clicked = false;
            pickedUP = false;
            rb.isKinematic = false;
            TableCollider.SetActive(false);
        }
        //Dip it & snap back
        if (pickedUP && Input.GetMouseButton(0)){    //Mouse Distance based Tilt Pouring here  
            this.transform.Rotate(deltaMousePosRot); 
            if(havePowder){  //if have powder and drag
                Invoke("ReleasePowder",0f);
            }
            // currenPowder.transform.parent = null;
        }else if (pickedUP&&Input.GetMouseButtonUp(0)){ //when pick up and release right click
            havePowder = false;
            Vector3 tempZ = this.transform.rotation * Vector3.forward; //Im trying to make the direction stay the same but failed....
            this.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);  //tempZ.zsnap to this rotation, but keep the z rotation
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
    }
    void OnMouseDown() {
        if(TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
        clicked = true;
        }
    }
    void OnMouseOver() {
        if(!clicked&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE){
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
    void powderON(){
        currenPowder = Instantiate(powderPrefab, powderPt.transform.position, Quaternion.identity) as GameObject;
        currenPowder.transform.parent = powderPt.transform;
        havePowder = true;  //if fell off cup turn it false
        //currenPowder = j;
    }
    void ReleasePowder(){
        currenPowder.transform.parent = null;
        //havePowder = false;
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Table"){
            mbAnim.SetBool("Open", false);
            mbAnim.SetBool("Close", true);
            pickedUP = false;
            toolTrigger.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Powder"){
            inPowderZone = true;
        }
        if(col.gameObject.tag == "ToolZone"){
            canRelease  = true;
        }
        if(col.gameObject.tag == "ToolTrigger"){
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            this.transform.position = OriginalToolPos.transform.position;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "ToolZone"){
            canRelease  = false;
        }
    }
}
