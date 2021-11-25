using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Simple fake tea filling up effect
//issue position are not accessing correctly
public class Tea : MonoBehaviour
{
    SpriteRenderer teasprite;
    public GameObject tea;
    public Color teaColor;
    public float maxHeight, minHeight, almostHeight;  //the cup max min , almostHeight is the level where liquid its almost at the bottom
    //Vector3 bottleTop;  //the target
    //float currentHeight;
    //float targetHeight;
    public float minSize, middleSize, maxSize;
    public float speed;
    public GameObject OriginalPos;
    public GameObject TopPos;
    void Start()
    {
        teasprite = this.GetComponent<SpriteRenderer>();
        teasprite.color = teaColor;//new Color (79f, 130f, 96f, 1);
        this.transform.localScale = new Vector3(minSize,minSize,minSize);
    }

    void Update()
    {
        teasprite.color = teaColor;//new Color (79f, 130f, 96f, 1);
        FillingUP();
    }
    void TeaBottom(){
        teaColor.a = 0f;
    }
    void FillingUP(){
        if(SpillingDetector.Instance.inCup&& Input.GetMouseButton(0)){
            float step = speed * Time.deltaTime;
            OriginalPos.transform.position = Vector3.MoveTowards(OriginalPos.transform.position, TopPos.transform.position, step);
        }
    }
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "L1"){
            print("yeh");
            //this.transform.localScale = new Vector3(middleSize,middleSize,middleSize);
            this.transform.localScale = new Vector3(maxSize,maxSize,maxSize);
        }
        if(col.gameObject.tag == "L2"){
            print("bigger");
            this.transform.localScale = new Vector3(maxSize,maxSize,maxSize);
        }
        
    }
}
