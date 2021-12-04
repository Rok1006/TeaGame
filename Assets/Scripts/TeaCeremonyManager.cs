using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
//Included:
//1. Candle lighting
//2. TeaPot Heating
//3.TeaColor
public class TeaCeremonyManager : MonoBehaviour
{
    //Hi! I'm TeaCeremonyManager! I'm glad you are here with me.
    //This is what I do:
    //1.Managing UI
    //2.Managing Lighting
    //3.Object State(like fire)
    //4.Particles(Maybe this shouldn't be here?)
    //5.Tool State(currHolding)
    //6.Stain
    public static TeaCeremonyManager Instance; //For easy access this is smart

    #region UI
    public GameObject candleBar;
    private Image cb;
    bool candlelighting = false;

    public GameObject potBar;
    private Image pb;
    bool potHeating = false;
    #endregion

    #region Lights
    public GameObject torchLight;
    private float initTorchBrightness = 2.36f;
    Light tl;

    public GameObject sideLight;
    private float initSideBrightness = 0.62f;
    Light sl;
    #endregion

    #region Objects
    public GameObject candleFire;
    float fireSize;
    #endregion

    #region Parti
    public ParticleSystem steamParticles;
    public ParticleSystem tpSteamParticles;
    #endregion

    #region Tools
    public enum TeaTool{NONE,POWDERTOOL,TEAPOT,STIRTOOL,NOTOOL};
    public TeaTool currentTool = TeaTool.NONE;
    #endregion

    #region Stain
    public Color[] TeaColors; //0=Water, 1=GreenTea, 2=TeawithToolesspowder, 3=Water with weirdElement, 4=GreenTeaWtoomucPowder
    public GameObject Stain;
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
        Stain.GetComponent<SpriteRenderer>().color = TeaColors[0];
    }
    void Update()
    {
        LightDiming();
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
            TeaPot.Instance.heatness+=0.005f; //keep tracking
        }

        if (Input.GetKeyDown(KeyCode.F)) //Button stoped working help
        {
            CandleLighting();
        }
    }

    public void ResetStove(){
        potHeating = false;
        potBar.SetActive(false);
        pb.fillAmount = 0;
        //pb.fillAmount = TeaPot.Instance.heatness; //reset it
    }
    void StopTeaPotSteam(){
        tpSteamParticles.emissionRate = 0;
    }
    public void CandleLighting(){
        candlelighting = true;
    }
    public void TeaPotHeating(){
        potHeating = true;
    }
    void LightDiming(){
        tl.intensity-=0.0007f;
        sl.intensity-=0.0007f;
        if(fireSize>30){
            fireSize-= 0.1f; 
        }
    }
}
