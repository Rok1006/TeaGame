using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on powder object
public class Powder : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Cup"){
            Tea.Instance.numOfPowder+=1;
            MatchaBox.Instance.havePowder = false;
            Destroy(this.gameObject, .5f);

        }
        if(col.gameObject.tag == "Table"){
            MatchaBox.Instance.havePowder = false;
            Destroy(this.gameObject, .5f);
        }
        //MatchaBox.Instance.havePowder = false;
    }
}
