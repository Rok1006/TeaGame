using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this on powder object and ingredients that will fall into cup
public class PowderAndIngredients : MonoBehaviour
{
    public bool isAsh;
    public bool isBomb;
    public bool isLeaf;
    public bool isChili;
    public SoundManager sc;
    void Start()
    {
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision col) {
        if(this.gameObject.tag == "Powder"){
            if(col.gameObject.tag == "Cup"){  //Cup
                MatchaBox.Instance.havePowder = false;
                sc.PowderDown();
                Destroy(this.gameObject, .75f);

            }
            if(col.gameObject.tag == "Table"){
                sc.PowderDown();
                MatchaBox.Instance.havePowder = false;
                Destroy(this.gameObject, .75f);
            }
        }
        if(this.gameObject.tag == "Ingredients"){
            if(col.gameObject.tag == "Cup"){
                Tea.Instance.RestartStirBar();
                Tea.Instance.numOfIngredients+=1;
                Tea.Instance.toMeltList.Add(gameObject);
            }
            if(col.gameObject.tag == "Table"){
                //Destroy(this.gameObject, .75f);
            }
        }
    }
    void OnTriggerEnter(Collider col) {
        if(this.gameObject.tag == "Powder"){
            if(col.gameObject.tag == "Bottom"){  //Cup
            Tea.Instance.RestartStirBar();
                sc.PowderDown();
                Tea.Instance.numOfPowder+=1;
                MatchaBox.Instance.havePowder = false;
                Tea.Instance.powderList.Add(gameObject);
                Destroy(this.gameObject, 3f);

            }
        }
    }
}
