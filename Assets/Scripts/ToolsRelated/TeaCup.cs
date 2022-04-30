using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Function:
//player pick it up and if the speed is too fast, some tea can spill
//each spill reduce the amount of tea in it
public class TeaCup : MonoBehaviour
{
    public static TeaCup Instance;
    public GameObject HandsIndicator;
    public GameObject guide;
    public bool occupied = false;
    public bool canServe = false;
    public SoundManager sc;
    //Pick UP related
    public bool pickedUP = false;
    public bool clicked = false;
    public bool canRelease = false;
    public int speed;
    public int state = 0;  //down
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
    public GameObject TrayZ;
    public GameObject CupZ;
    public GameObject TrayZTri;
    public GameObject CupZTri;
    public GameObject PlayerTrayIndicate;
    public GameObject ServeTrayIndicate;
    public GameObject CupFollowIndicate;
    public bool mouseOver;
    //public GameObject toolFirstStep; //for tutorial
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        HandsIndicator.SetActive(false);
        guide.SetActive(false);
        rb = this.GetComponent<Rigidbody>();
        pickUPDes = new Vector3(this.transform.position.x, 1.6f, this.transform.position.z);
        prevMousePos = Input.mousePosition;
        TrayZ.SetActive(false);
        CupZ.SetActive(false);
        TrayZTri.SetActive(false);
        CupZTri.SetActive(false);
        PlayerTrayIndicate.SetActive(false);
        ServeTrayIndicate.SetActive(false);
        CupFollowIndicate.SetActive(false);
    }
    void Update()
    {
        pickUPDes = new Vector3(this.transform.position.x, 1.6f, this.transform.position.z);
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE && !ServeTray.Instance.occupied && canServe) {
            if(state==0&&clicked){
                float step = speed * Time.deltaTime;
                this.transform.position = Vector3.MoveTowards(this.transform.position, pickUPDes, step);
                HandsIndicator.SetActive(false);
                guide.SetActive(false); 
                Tutorial.Instance.CTsteps[0].SetActive(false);
                if(!Tutorial.Instance.tutorialComplete){ //!Tutorial.Instance.tutorialComplete
                    GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
                    GameManager.Instance.arrowAnim.SetTrigger("tray");
                    Tutorial.Instance.CTsteps[1].SetActive(true);
                }
            }
        }
        if(this.transform.position==pickUPDes){  //player picked it up
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
            Plane plane = new Plane(Vector3.up, new Vector3(0, 2, 0));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance);
            }
        }
        if(pickedUP){  //also: make it when hovering outside of original pos, player cant release
            CupFollowIndicate.SetActive(true); //off
            Vector3 pos = this.transform.position;
            pos.y = 0.19f;  //table height
            pos.z = this.transform.position.z+0.15f;
            CupFollowIndicate.transform.position = pos; //off
        } 
        //RElease it
        if(state==1&&Input.GetMouseButton(1)&&canRelease){  //&&canRelease//later add canRelease boo
            CupFollowIndicate.SetActive(false);
            state = 0;
            clicked = false;
            pickedUP = false;
            rb.isKinematic = false;  //its being turned on constantly
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
        TrayZ.SetActive(true);
        CupZ.SetActive(true);
        TrayZTri.SetActive(true);
        CupZTri.SetActive(true);
    }
    void OnMouseOver() {
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE && !ServeTray.Instance.occupied && canServe) {
            //&&TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.ServeOK){
            if(!clicked){
                Tutorial.Instance.CTsteps[0].SetActive(true);
                HandsIndicator.SetActive(true);
                TeaCeremonyManager.Instance.CurrentToolName = "TeaCup";
                //guide.SetActive(true); 
            }
        }
        mouseOver = true;
    }

    void OnMouseExit(){
        if(!clicked){
            Tutorial.Instance.CTsteps[0].SetActive(false);
            HandsIndicator.SetActive(false);
            guide.SetActive(false); 
        }
        mouseOver = false;
    }
    void OnMouseDown() {
        if(!TeaCeremonyManager.Instance.served&&TeaCeremonyManager.Instance.currentTool == TeaCeremonyManager.TeaTool.NONE && !ServeTray.Instance.occupied && canServe) {
            clicked = true;
            //TeaCeremonyManager.Instance.ServeTea();
            GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "TrayZone"){
            canRelease  = true;
            ServeTrayIndicate.SetActive(true);
        }
        if(col.gameObject.tag == "TrayTrigger"){
            TeaCeremonyManager.Instance.ServeTea();
            TeaCeremonyManager.Instance.canProceed = true;
            TrayZTri.SetActive(false);
            Tutorial.Instance.CTsteps[1].SetActive(false);
            if(!Tutorial.Instance.tutorialComplete){GameManager.Instance.arrowAnim.SetTrigger("Deactivate");}
            //TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            //this.transform.position = OriginalToolPos.transform.position;
        }
        if(col.gameObject.tag == "CupZone"){
            canRelease  = true;
            PlayerTrayIndicate.SetActive(true);
        }
        if(col.gameObject.tag == "CupPlacementTrigger"){
            TeaCeremonyManager.Instance.currentTool = TeaCeremonyManager.TeaTool.NONE;
            TeaCeremonyManager.Instance.served = false;
            canServe = true;
            TeaCeremonyManager.Instance.mainCup.transform.position = TeaCeremonyManager.Instance.OriginalCupPos.transform.position;
            rb.isKinematic = true; //new
            TrayZ.SetActive(false);
            CupZ.SetActive(false);
            CupZTri.SetActive(false);
            Tutorial.Instance.CTsteps[1].SetActive(false);
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "TrayZone"||col.gameObject.tag == "CupZone"){
            canRelease  = false;
            PlayerTrayIndicate.SetActive(false);
            ServeTrayIndicate.SetActive(false);
        }
        
    }


}
