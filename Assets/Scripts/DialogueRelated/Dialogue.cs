using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public float typingSpeed;
    private int index = 0;
    public Text textDisplay;
    public GameObject NextB;
    public Customer customerdata;

    void Awake() {
        //dialougueList = customerdata.dialogue;
    }
    void Start()
    {
        index = 0;
        textDisplay.text = "";
        StartCoroutine(Type());
        NextB.SetActive(false);
        print(customerdata.dialogue[0].stringvalue);   //how to get the stuff inside 
        // for (int i = 0; i<customerdata.dialogue.Count;i++){
        //     Debug.Log(customerdata.dialogue[i].stringvalue);   //this finally get the inside of the list
        // }
    }
    void Update()
    {
        if(textDisplay.text == customerdata.dialogue[index].stringvalue)   //have to convery the type to string
        {
            NextB.SetActive(true);
            NextB.GetComponent<Button>().interactable = true;
        }
        else
        {
            NextB.SetActive(false);
        }
    }
    IEnumerator Type(){
        foreach(char letter in customerdata.dialogue[index].stringvalue.ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentences(){  //assign in button
        //BGAnim.SetTrigger("Next");
        NextB.GetComponent<Button>().interactable = false;
        if(index < customerdata.dialogue.Count - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }else{
            textDisplay.text = "";
        }
    }
}
