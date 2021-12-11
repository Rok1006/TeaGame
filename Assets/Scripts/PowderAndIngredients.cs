using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on powder object and ingredients that will fall into cup
public class PowderAndIngredients : MonoBehaviour
{
    public bool isAsh;
    public bool isBomb;
    public bool isLeaf;
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
                Tea.Instance.powderList.Add(gameObject);
                Destroy(this.gameObject, 60f);

            }
            if(col.gameObject.tag == "Table"){
                MatchaBox.Instance.havePowder = false;
                Destroy(this.gameObject, .75f);
            }
        }
        //MatchaBox.Instance.havePowder = false;
        if(this.gameObject.tag == "Ingredients"){
            if(col.gameObject.tag == "Cup"){
                Tea.Instance.numOfIngredients+=1;
                Tea.Instance.toMeltList.Add(gameObject);
                //Destroy(this.gameObject, .5f);
            }
            if(col.gameObject.tag == "Table"){
                Destroy(this.gameObject, .75f);
            }
        }
    }
}
