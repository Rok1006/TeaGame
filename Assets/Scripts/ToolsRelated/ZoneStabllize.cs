using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script is in SceneManager
//Content of the script:
//particles effect
//different ghost have different negative energy and it will damage the tea house protective zone
//watering the plant help reviving it
//hot water damage hp hard, only cold water
//animation of the plant: plant healthy normal, middle healthy, warning health, being water
public class ZoneStabllize : MonoBehaviour
{
    public static ZoneStabllize Instance;
    public Animator FruitPlant;
    public int plantHP = 100;
    public bool isHotWater = false;
    public float hydration = 100; //link with waterbar
    public GameObject WaterBar;
    public Slider wb;
    public int UnsafeTimeCount = 0;  //after zone turn unsafe, start counting down before warning> fail

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        WaterBar.SetActive(false);
        
    }
    void Update()
    {
        DetermineTempofWater();
        wb.value = hydration;
        if(!SpillingDetector.Instance.watering){
            PlantHydrationReduce();
            WaterBar.SetActive(false);
        }else if(SpillingDetector.Instance.watering&&TeaPot.Instance.pickedUP){
            PlantWateringPlus();
            WaterBar.SetActive(true);
        }
        if(hydration<=40){
            Effects.Instance.BadZoneEffect();
        }else if(hydration>40){
            Effects.Instance.GoodZoneEffect();
        }
    }
    void DetermineTempofWater(){
        if(Tea.Instance.temp>40){
            isHotWater = true;
        }else if(Tea.Instance.temp<40){
            isHotWater = false;
        }
    }
    public void PlantWateringPlus(){ //gain of hp by watering it
        hydration+=3*Time.deltaTime;
    }

    public void PlantHydrationReduce(){ //plant hp harmed according to different ghost 
        switch(GameManager.Instance.ghostIndex){
            case 0: //sensei
                hydration-=0;
            break;
            case 1: //student
                hydration-=2*Time.deltaTime;
            break;
            case 2: //laika
                hydration-=1*Time.deltaTime;
            break;
            case 3: //capitalist

            break;
        }
    }
    void PlantStatusAnim(){ //plant animation control accored to hydration

    }
}
