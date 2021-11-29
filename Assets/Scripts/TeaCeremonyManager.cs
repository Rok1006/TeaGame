using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
//Included:
//1. Candle lighting
//2. TeaPot Heating
public class TeaCeremonyManager : MonoBehaviour
{
    public static TeaCeremonyManager Instance;
    public GameObject candleBar;
    private Image cb;
    public GameObject potBar;
    private Image pb;
    bool candlelighting = false;
    public GameObject torchLight;
    Light tl;
    public GameObject sideLight;
    Light sl;
    private float initialTLtBrightness = 2.36f;
    private float initialSLtBrightness = 0.62f;
    public GameObject candleFire;
    float fireSize;
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
            potBar.SetActive(false);
        }
        
    }
    public void CandleLighting(){
        candlelighting = true;
    }
    public void TeaPotHeating(){
        potBar.SetActive(true);
        pb.fillAmount+=0.005f;
    }
    void LightDiming(){
        tl.intensity-=0.0007f;
        sl.intensity-=0.0007f;
        if(fireSize>30){
            fireSize-= 0.1f;
        }
        
    }
}
