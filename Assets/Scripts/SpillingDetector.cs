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
        
    }

    void Update()
    {
        if(leaveStain){
            Vector3 stainPos = new Vector3(this.gameObject.transform.position.x,0.201f, this.gameObject.transform.position.z);
            GameObject j = Instantiate(stain, stainPos, Quaternion.identity) as GameObject;
            j.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            Destroy(j.gameObject,1f);
            leaveStain=false;
               //later can have a variation of size
        }
    }
    void OnParticleCollision(GameObject col)   
    {
        print("sth");
        //print(this.gameObject.transform.position.x);
        leaveStain = true;
        //make in instanciate only once
        if(col.gameObject.tag == "Cup"){
            print("Hit");
            inCup = true;
        }else{
            print("notHit");
            inCup = false;
        }
    }
    void OnParticleTrigger(){

    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Bottom"){
            print("this");
        }
    }
}
