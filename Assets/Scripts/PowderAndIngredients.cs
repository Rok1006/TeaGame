using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on powder object and ingredients that will fall into cup
public class PowderAndIngredients : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col) {
        if(this.gameObject.tag == "Powder"){
            if(col.gameObject.tag == "Cup"){
                Tea.Instance.numOfPowder+=1;
                MatchaBox.Instance.havePowder = false;
                Destroy(this.gameObject, .5f);

            }
            if(col.gameObject.tag == "Table"){
                MatchaBox.Instance.havePowder = false;
                Destroy(this.gameObject, .5f);
            }
        }
        //MatchaBox.Instance.havePowder = false;
        if(this.gameObject.tag == "Ingredients"){
            if(col.gameObject.tag == "Cup"){
                Tea.Instance.numOfIngredients+=1;
                Destroy(this.gameObject, .5f);
            }
            if(col.gameObject.tag == "Table"){
                Destroy(this.gameObject, .5f);
            }
        }
    }
}
