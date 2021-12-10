using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class TeaCeremonyManager : MonoBehaviour
{
    //Hi! I'm TeaCeremonyManager! I'm glad you are here with me.
    //This is what I do:
    //1.Managing UI
    //2.Managing Lighting
    //3.Object State(like fire)
    //4.Particles(Maybe this shouldn't be here?)
    //5.Tool State(currHolding)
    //6.TeaColor
    //7.Ingredients Insertion
    //8.TeaServing
    public static TeaCeremonyManager Instance; //For easy access this is smart

    #region UI
    public GameObject candleBar;
    private Image cb;
    bool candlelighting = false;

    public GameObject potBar;
    private Image pb;
    bool potHeating = false;
    public GameObject discardButton;
    public bool canDiscard = false;
    public GameObject toolText;
    public string tText;
    //public GameObject toolFirststep;
    #endregion

    #region Lights
    public GameObject torchLight;
    private float initTorchBrightness = 2.36f;
    Light tl;
    public GameObject sideLight;
    private float initSideBrightness = 0.62f;
    Light sl;
    public bool startDiming = false;
    public GameObject candleFire;
    float fireSize;
    #endregion

    #region Particles
    public ParticleSystem steamParticles;
    public ParticleSystem tpSteamParticles;
    public GameObject cloudParticles;
    #endregion

    #region Tools
    public enum TeaTool{NONE,POWDERTOOL,TEAPOT,STIRTOOL,NOTOOL,INGRED};  //None is prestate
    public TeaTool currentTool = TeaTool.NONE;
    #endregion
    public enum TutorialState{Nothing,FreePlay,UseTeapot,UseStirTool,UsePowderTool,GetIngredient};
    public TutorialState currentTutorialState = TutorialState.Nothing;  
    #region TeaColor
    public Color[] TeaColors; //0=Water, 1=GreenTea, 2=TeawithToolesspowder, 3=Water with weirdElement, 4=GreenTeaWtoomucPowder
    public GameObject Stain;
    #endregion
    #region Ingredients
    public GameObject InsertSpot;
    #endregion
    #region Serving
    public GameObject OriginalCupPos;
    public GameObject ServeCupPos;
    public GameObject mainCup;
    public bool served = false;
    public GameObject tea;
    #endregion
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        fireSize = 100;
        cb = candleBar.GetComponent<Image>();
        pb = potBar.GetComponent<Image>();
        candleBar.SetActive(false);
        potBar.SetActive(false);
        cb.fillAmount = 0;
        pb.fillAmount = 0;
        //DimAnim = DimEffect.GetComponent<Animator>();
        tl = torchLight.GetComponent<Light>();
        tl.intensity = initTorchBrightness;
        sl = sideLight.GetComponent<Light>();
        sl.intensity = initSideBrightness;
        tpSteamParticles.emissionRate = 0;
        cloudParticles.SetActive(false);
        Stain.GetComponent<SpriteRenderer>().color = TeaColors[0];
        //Color32 c = Stain.GetComponent<SpriteRenderer>().color;
        discardButton.SetActive(false);
        toolText.SetActive(false);
    }
    void Update()
    {
        toolText.GetComponent<Text>().text =  tText.ToString();
        if(currentTool == TeaTool.NOTOOL||currentTool == TeaTool.NONE){
            discardButton.SetActive(true);
        }else{
            discardButton.SetActive(false);
        }
        if(startDiming){
          LightDiming();  
        }
        candleFire.transform.localScale = new Vector3(fireSize,fireSize,fireSize);
        //Candle
        if(cb.fillAmount == 1){
            fireSize = 100;
            tl.intensity = initTorchBrightness;
            sl.intensity = initSideBrightness;
            candlelighting = false;
            candleBar.SetActive(false);
            cb.fillAmount = 0; //reset it
        }
        if(candlelighting){
            candleBar.SetActive(true);
            cb.fillAmount+=0.008f;
        }
        //Teapot
        if(pb.fillAmount == 1){
            TeaPot.Instance.canClick = true;
            tpSteamParticles.emissionRate = 2;
            Invoke("StopTeaPotSteam",2.5f);
            ResetStove();
        }
        if(potHeating){
            potBar.SetActive(true);
            pb.fillAmount+=0.005f;
            if(pb.fillAmount!=1&&TeaPot.Instance.heatness<1){
                TeaPot.Instance.heatness+=0.005f; //keep tracking
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) //Button stoped working help
        {
            CandleLighting();
        }
        //Testing
        if(Input.GetKeyDown(KeyCode.G)){ //if teacup back to player reset tea state
            TeaReturn();
        }
    }
    //TeaPot & Heating
    public void ResetStove(){
        potHeating = false;
        potBar.SetActive(false);
        pb.fillAmount = 0;
        //pb.fillAmount = TeaPot.Instance.heatness; //reset it
    }
    void StopTeaPotSteam(){
        tpSteamParticles.emissionRate = 0;
    }
    public void TeaPotHeating(){
        potHeating = true;
    }
    //Lighting
    public void CandleLighting(){
        candlelighting = true;
    }
    void LightDiming(){
        tl.intensity-=0.0007f;
        sl.intensity-=0.0007f;
        if(fireSize>30){
            fireSize-= 0.1f; 
        }
    }
    //Ingredients Insertion
    public void IngredientsAdd(GameObject Ingredients){
        GameObject i = Instantiate(Ingredients, InsertSpot.transform.position, Quaternion.identity) as GameObject;
    }
    public void ServeTea(){
        mainCup.transform.position = ServeCupPos.transform.position;
        served = true;
        //opposite person with do sth to the tea
    }
    public void TeaReturn(){
        mainCup.transform.position = OriginalCupPos.transform.position;
        served = false;
        //reset tea state
        Tea.Instance.OriginalPos.transform.position = Tea.Instance.resetPos; //rest tea to initial pos
        Tea.Instance.initialDistance = Tea.Instance.TopPos.transform.position.y-Tea.Instance.OriginalPos.transform.position.y; //reset initial distance for cup capacity
        tea.transform.localScale = new Vector3(Tea.Instance.minSize,Tea.Instance.minSize,Tea.Instance.minSize); //reset scale
        Tea.Instance.teastate = 0; 
        Tea.Instance.cc.fillAmount = 0;
        Tea.Instance.numOfPowder = 0;
        Tea.Instance.numOfIngredients = 0;
    }
    public void DiscardTea(){ //whe sensei want to discard it , it will happened, cus there is stuff in it
        // if(canDiscard){
        cloudParticles.SetActive(true);
        Invoke("StopCloud",2.5f);
        Tea.Instance.OriginalPos.transform.position = Tea.Instance.resetPos; //rest tea to initial pos
        Tea.Instance.initialDistance = Tea.Instance.TopPos.transform.position.y-Tea.Instance.OriginalPos.transform.position.y; //reset initial distance for cup capacity
        tea.transform.localScale = new Vector3(Tea.Instance.minSize,Tea.Instance.minSize,Tea.Instance.minSize); //reset scale
        Tea.Instance.teastate = 0; 
        Tea.Instance.cc.fillAmount = 0;
        Tea.Instance.numOfPowder = 0;
        Tea.Instance.numOfIngredients = 0;
        //}
    }
    void StopCloud(){
        cloudParticles.SetActive(false);
        //canDiscard = false;
    }
}
