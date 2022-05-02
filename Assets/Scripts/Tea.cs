using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//This script is put on the fake tea object under Main Cup
//Simple fake tea filling up effect
//issue position are not accessing correctly
//heatness bar, degree
public class Tea : MonoBehaviour
{
    public static Tea Instance;
    public SpriteRenderer teasprite;
    public GameObject tea;
    public Tutorial ts;
    public GameObject tutorialObj;
    //public Color teaColor;
    public float minSize, middleSize, maxSize;   //max original: 0.003516069
    public float speed; //speed of filling up, 0.03
    public GameObject OriginalPos;
    public GameObject TopPos;
    public Vector3 resetPos;
    public float distance;
    public float initialDistance;
    private float originalDistance;  //static
    public GameObject teaPattern; //when stirring
    public Color32 targetColor;
    public Color32 currentColor;
    public Color32 originalColor;
    [Header("UI")]
    public GameObject stirBar;
    private Image sb;
    public GameObject cupCapacity;
    public Image cc;
    public GameObject heatBar;
    public Slider hb;
    public TextMeshProUGUI degree;
    public int maxD, minD; 
    public float temp = 20;
    
    [Header("Tea Status")]
    public bool stirring = false;
    public float liquidLevel;  //max 130, acceptable range: 90
    public int numOfPowder;
    public int numOfIngredients;
    public int teastate;
    public string ingredientType;
    public bool melted;
    public List<GameObject> toMeltList = new List<GameObject>();
    public List<GameObject> powderList = new List<GameObject>();
    //heatness of the tea
    public SoundManager sc;
    public GameObject floodParticles;
    public ParticleSystem FP;
    public bool isPouring = false;
    public Vector2 mousePos;
    void Awake() {
        Instance = this;
        ts = tutorialObj.GetComponent<Tutorial>();
    }
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        teasprite = this.GetComponent<SpriteRenderer>();
        //teasprite.color = teaColor;//new Color (79f, 130f, 96f, 1);
        this.transform.localScale = new Vector3(minSize,minSize,minSize);
        currentColor = TeaCeremonyManager.Instance.TeaColors[0];  //blue water color
        teasprite.color = currentColor;   //color
        heatBar.SetActive(false);
        sb = stirBar.GetComponent<Image>();
        sb.fillAmount = 0;
        stirBar.SetActive(false);
        cc.fillAmount = 0;
        cupCapacity.SetActive(false);  //on when player holding up pot
        
        teaPattern.SetActive(false);
        initialDistance = TopPos.transform.position.y-OriginalPos.transform.position.y;
        originalDistance = TopPos.transform.position.y-OriginalPos.transform.position.y;
        //Reset Related
        resetPos = OriginalPos.transform.position; //this is where original pos will go back to when reset

        originalColor = teasprite.color;
        floodParticles.SetActive(false);
        maxD = 100;
        minD = 20;
        temp = minD;
        mousePos = Input.mousePosition;
    }
    void Update()
    {
        teasprite.color = currentColor;
        degree.text = Mathf.Round(temp).ToString();
//FillUp Tea
        // liquidLevel = cc.fillAmount;  //originally uncheck
        distance = TopPos.transform.position.y-OriginalPos.transform.position.y;
        if(distance<initialDistance){
            cc.fillAmount +=0.0012f;
            initialDistance = distance; //setting it to every current
        }
        if(distance==0&&PourDetector.Instance.isPouring&&SpillingDetector.Instance.inCup){  //Determine Flooding
            floodParticles.SetActive(true);
            FP.emissionRate=40;
        }else{
            FP.emissionRate=0;
            //Invoke("OFFFloodParticles",3f);
        }
        FillingUP(); 
//Stirring Tea
        if(stirring){   //the bar
            Vector2 thisMousePos = Input.mousePosition;
            if (Mathf.Abs(thisMousePos.x-mousePos.x)>0|| Mathf.Abs(thisMousePos.y - mousePos.y)>0)
            {
                teaPattern.SetActive(true);
            }
            else
            {
                teaPattern.SetActive(false);
            }
            mousePos = thisMousePos;
            stirBar.SetActive(true);
            sb.fillAmount+=0.7f*Time.deltaTime;  //Default: 0.008f
            GradualColorChange();
        }else{
            stirBar.SetActive(false);
            teaPattern.SetActive(false);
        }
        if(sb.fillAmount==1){  //every stirr
            //RestartStirBar(); //put this on when new things is added
            // TeaState();//affect tea//change tea color  //move it to before stirring, for instance when pick up tool
        }
//Change of state
        if(numOfPowder==3){  //right amt
            teastate = 1;
        }else if(numOfPowder<3&&numOfPowder>0){  //too less
            teastate = 2;
        }else if(numOfPowder>3){   //too much
            teastate = 4;
        }else if(numOfPowder<1){  //&& if have other ingredients
            teastate = 0;
        }
//Destroy special ingredients if have liquid
        if (liquidLevel >= 30f)  //om3
        {
            if(toMeltList.Count > 0)
                MeltIngred();
            if (powderList.Count > 0) //melt powder
            {
                foreach (GameObject powder in powderList)
                    Destroy(powder);
                powderList.Clear();
            }
        }
    }
    void OFFFloodParticles(){
        floodParticles.SetActive(false);
    }
    void MeltIngred() //for some reason it auto get call this when second tea
    {
        Debug.Log("Melting");
        if (toMeltList.Contains(Ingredients.ashObj))
        {
            toMeltList.Remove(Ingredients.ashObj);
            Destroy(Ingredients.ashObj);
            Ingredients.haveAsh = false;
        }
        if (toMeltList.Contains(Ingredients.bombObj))
        {
            toMeltList.Remove(Ingredients.bombObj);
            Destroy(Ingredients.bombObj);
            Ingredients.haveBomb = false;
        }
        if (toMeltList.Contains(Ingredients.leafObj))
        {
            toMeltList.Remove(Ingredients.leafObj);
            Destroy(Ingredients.leafObj);
            Ingredients.haveLeaf = false;
        }
        if (toMeltList.Contains(Ingredients.chiliObj))
        {
            toMeltList.Remove(Ingredients.chiliObj);
            Destroy(Ingredients.chiliObj);
            Ingredients.haveChili = false;
        }
        //melted = true; commented this out bc what is the player adds after melting?
        //If find good way to optimize, don't have this method run in update.
    }
    void TeaBottom(){
        //teaColor.a = 0f;
    }
    void FillingUP(){   //Filling UP tea
        if(SpillingDetector.Instance.inCup&& Input.GetMouseButton(0)&&TeaPot.Instance.degree<TeaPot.Instance.pouringDegree){
            //cc.fillAmount +=0.0018f;  //0.0023f, change this to according to the distance between top and original pos
            liquidLevel += 10* Time.deltaTime;
            JudgeTea.Instance.heatnessOfWater = (int)Tea.Instance.temp;
            float step = speed * Time.deltaTime;
            OriginalPos.transform.position = Vector3.MoveTowards(OriginalPos.transform.position, TopPos.transform.position, step);
        }
    }
    public void RestartStirBar(){
        sb.fillAmount = 0;
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "L1"){
            //this.transform.localScale = new Vector3(middleSize,middleSize,middleSize);
            this.transform.localScale = new Vector3(maxSize,maxSize,maxSize);
            //Debug.Log("enlarge");
        }
        if(col.gameObject.tag == "L2"){
            this.transform.localScale = new Vector3(maxSize,maxSize,maxSize);
        }
        if(col.gameObject.tag == "StirrTool"){
            sc.Stirring();
            stirring = true;
        } 
    }
    void OnTriggerStay(Collider col) {
        if(col.gameObject.tag == "L1"){ //solved small circle issue
            //this.transform.localScale = new Vector3(middleSize,middleSize,middleSize);
            this.transform.localScale = new Vector3(maxSize,maxSize,maxSize);
            //Debug.Log("enlarge");
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "StirrTool"){
            stirring = false;
        } 
    }
    public void TeaState(){   //Change tea color
        switch(teastate){
            case 0:  //water
                //teasprite.color = TeaCeremonyManager.Instance.TeaColors[0];
                targetColor=TeaCeremonyManager.Instance.TeaColors[0];
            break;
            case 1:  //greentea
                //teasprite.color = TeaCeremonyManager.Instance.TeaColors[1];
                targetColor = TeaCeremonyManager.Instance.TeaColors[1];
            break;
            case 2:  //tea with too less powder
                //teasprite.color = TeaCeremonyManager.Instance.TeaColors[2];
                targetColor = TeaCeremonyManager.Instance.TeaColors[2];
            break;
            case 3:  //water with weird element
                //teasprite.color = TeaCeremonyManager.Instance.TeaColors[3];
                targetColor = TeaCeremonyManager.Instance.TeaColors[3];
            break;
            case 4:  //tea with too much powder
                //teasprite.color = TeaCeremonyManager.Instance.TeaColors[4];
                targetColor = TeaCeremonyManager.Instance.TeaColors[4];
            break;
        }
    }
    void GradualColorChange(){
        if(currentColor.r<targetColor.r){
            currentColor.r+=1;
        }else{
            currentColor.r-=1;
        }
        if(currentColor.g<targetColor.g){
            currentColor.g+=1;
        }else{
            currentColor.g-=1;
        }
        if(currentColor.b<targetColor.b){
            currentColor.b+=1;
        }else{
            currentColor.b-=1;
        }
    }
    public void ChangeIngredientType(string type){
        ingredientType = type;
        Invoke("IngredientType", 3f); //wait time in effect
    }
    void IngredientType(){ //meed the cup to have water inorder to have effect
        switch(ingredientType){
            case "FlowerTeaBomb":
                //how it affect the tea, may be sth float up
            break;
        }
    }

}
