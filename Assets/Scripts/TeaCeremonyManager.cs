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
    public static TeaCeremonyManager Instance;
    public GameObject candleBar;
    private Image cb;
    public GameObject potBar;
    private Image pb;
    bool candlelighting = false;
    bool potHeating = false;
    public GameObject torchLight;
    Light tl;
    public GameObject sideLight;
    Light sl;
    private float initialTLtBrightness = 2.36f;
    private float initialSLtBrightness = 0.62f;
    public GameObject candleFire;
    float fireSize;
    public ParticleSystem steamParticles;
    public ParticleSystem tpSteamParticles;
    public enum TeaTool{NONE,POWDERTOOL,TEAPOT,STIRTOOL};
    public TeaTool currentTool = TeaTool.NONE;
    public Color[] TeaColors; //1=Water, 2=GreenTea, 3=TeawithTooMuchWater, 4=Water with weirdElement
    public GameObject Stain;
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
        tl.intensity = initialTLtBrightness;
        sl = sideLight.GetComponent<Light>();
        sl.intensity = initialSLtBrightness;
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
            tl.intensity = initialTLtBrightness;
            sl.intensity = initialSLtBrightness;
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
