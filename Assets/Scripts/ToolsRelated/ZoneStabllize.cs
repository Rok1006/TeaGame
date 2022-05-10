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
    public bool warning = false;
    public bool zoneHarm = false;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        WaterBar.SetActive(false);
        plantEmit.SetActive(true);
        //warning = true;
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
            Invoke("GameOver", 10);
        }
    }
    public void GameOver(){
        EventTrigger.Instance.GameOverScreen.SetActive(true);
        //Time.timeScale = 0;
    }
    void DetermineTempofWater(){
        if(Tea.Instance.temp>40){
            isHotWater = true;
        }else if(Tea.Instance.temp<40){
            isHotWater = false;
        }
    }
    public void PlantWateringPlus(){ //gain of hp by watering it
        if(!isHotWater){
            hydration+=5*Time.deltaTime;
        }else{
            hydration-=.8f*Time.deltaTime;  //player using hotwater
        }
    }

    public void PlantHydrationReduce(){ //plant hp harmed according to different ghost 
        if(zoneHarm){
            switch(GameManager.Instance.ghostIndex){
                case 0: //sensei
                    if(hydration>0){
                        hydration-=0; //5*Time.deltaTime
                    }
                break;
                case 1: //student
                    if(hydration>0){
                        hydration-=1*Time.deltaTime;  //default: 2
                    }
                break;
                case 2: //laika
                    if(hydration>0){
                        hydration-=.5f*Time.deltaTime;
                    }
                break;
                case 3: //capitalist
                    if(hydration>0){
                        hydration-=1.5f*Time.deltaTime;
                    }
                break;
            }
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
        if(hydration<=35){  //Warning, zone unsafe
            warning = true;
            FruitPlant.SetBool("weak2", true);
            FruitPlant.SetBool("weak1", false);
            Effects.Instance.TableLighting.SetTrigger("sickening");
            CamSwitch.Instance.DetermineAngryEffectPos();
            Effects.Instance.AngryEAnim.SetBool("IN", true);
            Effects.Instance.AngryEAnim.SetBool("OUT", false);
        }
        if(hydration>=35&&hydration<=50){
            warning = false;
            FruitPlant.SetBool("weak1", true);
            CamSwitch.Instance.DetermineAngryEffectPos();
            Effects.Instance.AngryEAnim.SetBool("IN", false);
            Effects.Instance.AngryEAnim.SetBool("OUT", true);
        }
        if(hydration>=50&&hydration<=100){
            warning = false;
            FruitPlant.SetBool("weak2", false);
            FruitPlant.SetBool("weak1", false);
            FruitPlant.SetTrigger("reviving");
            Effects.Instance.TableLighting.SetTrigger("normal");
            CamSwitch.Instance.DetermineAngryEffectPos();
            Effects.Instance.AngryEAnim.SetBool("IN", false);
            Effects.Instance.AngryEAnim.SetBool("OUT", true);
            //Effects.Instance.AngryEAnim.SetBool("IN", false);
        }
    }
    public void ResetPlantStatus(){
        hydration = 100;
    }
    // IEnumerator AngryEffect(){
    //     Effects.Instance.AngryEAnim.SetTrigger("OUT");
    //     yield return new WaitForSeconds(1f);
    //     Effects.Instance.AngryEffect.SetActive(false);
    //     Effects.Instance.AngryEffect.SetActive(true);
    //     Effects.Instance.AngryEAnim.SetTrigger("IN");
    // }
}
