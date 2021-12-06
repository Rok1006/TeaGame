using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillingDetector : MonoBehaviour
{
    public static SpillingDetector Instance;
    public bool inCup = false;
    public GameObject stain;
    bool leaveStain = false;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        TeaCeremonyManager.Instance.steamParticles.emissionRate = 0;
    }

    void Update()
    {
        //print(Time.frameCount);
        // if(Time.frameCount%4==0){
        //     print("yeh");
        // }
        if(Time.frameCount%2==0){
        if(leaveStain){
            Vector3 stainPos = new Vector3(this.gameObject.transform.position.x,0.201f, this.gameObject.transform.position.z);
            GameObject j = Instantiate(stain, stainPos, Quaternion.identity) as GameObject;
            float size = Random.Range(0.05f,0.8f);  //0.5
            j.transform.localScale = new Vector3(size,size,size);
            j.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Destroy(j.gameObject,.5f);
            leaveStain=false;
               //later can have a variation of size
        }
        }
    }
    void OnParticleCollision(GameObject col)   
    {
        print("sth");
        if(col.gameObject.tag == "Table"){
            leaveStain = true;
        }else{
            leaveStain = false;
        }
        //make in instanciate only once
        if(col.gameObject.tag == "Cup"){
            print("Hit");
            inCup = true;
            if(TeaPot.Instance.heatness>=0.7f){TeaCeremonyManager.Instance.steamParticles.emissionRate = 2;}
            Invoke("SteamEmitStop",2f);
        }else{
            print("notHit");
            inCup = false;
        }
    }
    void OnParticleTrigger(){

    }
    void SteamEmitStop(){
        //TeaCeremonyManager.Instance.sP.SetActive(false);
        TeaCeremonyManager.Instance.steamParticles.emissionRate = 0;
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Bottom"){
            print("this");
        }
    }
}
