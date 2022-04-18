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
    public float hydration = 100; //link with waterbar, animations,plants anim
    public GameObject WaterBar;
    public Slider wb;
    public int UnsafeTimeCount = 0;  //after zone turn unsafe, start counting down before warning> fail
    public GameObject plantEmit;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        WaterBar.SetActive(false);
        plantEmit.SetActive(true);
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
            FruitPlant.SetTrigger("watering");
            WaterBar.SetActive(true);
        }
        PlantStatusAnim();
        if(hydration<1){//start counting 
            //INvoke gameover scene
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
                if(hydration>0){
                    hydration-=0; //5*Time.deltaTime
                }
            break;
            case 1: //student
                if(hydration>0){
                    hydration-=2*Time.deltaTime;  //default: 2
                }
            break;
            case 2: //laika
                if(hydration>0){
                    hydration-=1*Time.deltaTime;
                }
            break;
            case 3: //capitalist

            break;
        }
    }
    void PlantStatusAnim(){ //plant animation control accored to hydration
        if(hydration<=45){
            Effects.Instance.BadZoneEffect();
            plantEmit.SetActive(false);
        }else if(hydration>45){
            Effects.Instance.GoodZoneEffect();
            plantEmit.SetActive(true);
        }
        //Anim
        if(hydration<=50){
            FruitPlant.SetBool("weak1", true);
        }
        if(hydration<=35){
            FruitPlant.SetBool("weak2", true);
            FruitPlant.SetBool("weak1", false);
            Effects.Instance.TableLighting.SetTrigger("sickening");
        }
        if(hydration>=60&&hydration<=100){
            FruitPlant.SetBool("weak2", false);
            FruitPlant.SetBool("weak1", false);
            FruitPlant.SetTrigger("reviving");
        }
    }
}
