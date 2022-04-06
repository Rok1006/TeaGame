using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public int plantHP = 100;
    public bool isHotWater = false;
    public ParticleSystem goodZone;
    public ParticleSystem badZone;

    void Awake() {
        Instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        DetermineTempofWater();
    }
    void DetermineTempofWater(){
        if(Tea.Instance.temp>40){
            isHotWater = true;
        }else if(Tea.Instance.temp<40){
            isHotWater = false;
        }
    }
    void PlantWateringPlus(){ //gain of hp by watering it

    }

    void PlantHpReduce(){ //plant hp harmed according to different ghost 

    }
}
