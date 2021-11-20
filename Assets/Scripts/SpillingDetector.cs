using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpillingDetector : MonoBehaviour
{
    public static SpillingDetector Instance;
    public bool inCup = false;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnParticleCollision(GameObject col)   
    {
        //print("sth");
        if(col.gameObject.tag == "Cup"){
            print("Hit");
            inCup = true;
        }else{
            inCup = false;
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Cup"){
            print("Hit");
        }
    }
}
