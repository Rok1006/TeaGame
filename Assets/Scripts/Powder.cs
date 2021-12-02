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
            //do sth
            MatchaBox.Instance.havePowder = false;

        }
        if(col.gameObject.tag == "Table"){
            MatchaBox.Instance.havePowder = false;
            //Destroy(this.gameObject, .5f);
        }
    }
}
