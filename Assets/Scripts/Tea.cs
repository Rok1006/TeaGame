﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//This script is put on the fake tea object under Main Cup
//Simple fake tea filling up effect
//issue position are not accessing correctly
public class Tea : MonoBehaviour
{
    public static Tea Instance;
    SpriteRenderer teasprite;
    public GameObject tea;
    public Color teaColor;
    public float minSize, middleSize, maxSize;   //max original: 0.003516069
    public float speed;
    public GameObject OriginalPos;
    public GameObject TopPos;
    public float distance;
    public float initialDistance;
    [Header("UI")]
    public GameObject stirBar;
    private Image sb;
    public GameObject cupCapacity;
    public Image cc;
    
    [Header("Tea Status")]
    public bool stirring = false;
    public float liquidLevel;  //amt of liquid in the cup  Decent amt is 0.80
    public int numOfPowder;
    public int teastate;
    //heatness of the tea
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        teasprite = this.GetComponent<SpriteRenderer>();
        teasprite.color = teaColor;//new Color (79f, 130f, 96f, 1);
        this.transform.localScale = new Vector3(minSize,minSize,minSize);
        teasprite.color = TeaCeremonyManager.Instance.TeaColors[0];  //blue water color
        sb = stirBar.GetComponent<Image>();
        sb.fillAmount = 0;
        stirBar.SetActive(false);
        cc.fillAmount = 0;
        cupCapacity.SetActive(false);  //on when player holding up pot
        initialDistance = TopPos.transform.position.y-OriginalPos.transform.position.y;
    }
    void Update()
    {
        distance = TopPos.transform.position.y-OriginalPos.transform.position.y;
        //print(distance); 
        if(distance<initialDistance){
            cc.fillAmount +=0.0012f;
            initialDistance = distance; //setting it to every current
        }

        FillingUP(); 
        if(stirring){
            stirBar.SetActive(true);
            sb.fillAmount+=0.008f;
        }else{
            stirBar.SetActive(false);
        }
        if(sb.fillAmount==1){  //every stirr
            RestartStirBar();
            TeaState();//affect tea//change tea color
        }
        //Change of state
        if(numOfPowder==3){  //right amt
            teastate = 1;
        }else if(numOfPowder<3&&numOfPowder>0){  //too less
            teastate = 2;
        }else if(numOfPowder>3){   //too much
            teastate = 4;
        }else if(numOfPowder<1){  //&& if have other ingredients
            teastate = 0;
        }

    }
    void TeaBottom(){
        teaColor.a = 0f;
    }
    void FillingUP(){
        if(SpillingDetector.Instance.inCup&& Input.GetMouseButton(0)){
            //cc.fillAmount +=0.0018f;  //0.0023f, change this to according to the distance between top and original pos
            float step = speed * Time.deltaTime;
            OriginalPos.transform.position = Vector3.MoveTowards(OriginalPos.transform.position, TopPos.transform.position, step);
        }
    }
    void RestartStirBar(){
        sb.fillAmount = 0;
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
        if(col.gameObject.tag == "StirrTool"){
            stirring = true;
        } 
    }
    void OnTriggerExit(Collider col) {
        if(col.gameObject.tag == "StirrTool"){
            stirring = false;
        } 
    }

    void TeaState(){   //Change tea color
        switch(teastate){
            case 0:  //water
                teasprite.color = TeaCeremonyManager.Instance.TeaColors[0];
            break;
            case 1:  //greentea
                teasprite.color = TeaCeremonyManager.Instance.TeaColors[1];
            break;
            case 2:  //tea with too less powder
                teasprite.color = TeaCeremonyManager.Instance.TeaColors[2];
            break;
            case 3:  //water with weird element
                teasprite.color = TeaCeremonyManager.Instance.TeaColors[3];
            break;
            case 4:  //tea with too much powder
                teasprite.color = TeaCeremonyManager.Instance.TeaColors[4];
            break;
        }
    }

}
