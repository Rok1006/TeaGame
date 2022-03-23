using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SceneDataLoad : MonoBehaviour
{
    public static SceneDataLoad Instance;
    public GameObject TitleScreen; //is nt on orginally
    public TextMeshProUGUI titleText;
    void Awake() {
        if(GameObject.Find("LevelData(DoNtDestroy)") != null){
            titleText.text = LevelData.Instance.title.ToString();//display title
            GameManager.Instance.ghostIndex = LevelData.Instance.ghostNum; //get the num from levelindex
            //load everything here
            print("Day "+LevelData.Instance.ghostNum+" - "+LevelData.Instance.title);
        }
        //get data from LevelData and proceed to load character and stuff
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        GhostInfoUpdate();
    }
    void GhostInfoUpdate(){
        // if(GameManager.Instance.ghostIndex==0){

        // }
        switch(GameManager.Instance.ghostIndex){
            case 0: 
                titleText.text = "SENSEI";
                GameManager.Instance.YarnDialogueSys.SetActive(false);
            break;
            case 1: 
                titleText.text = "Overwork Student";
                GameManager.Instance.YarnDialogueSys.SetActive(true);
            break;
            case 2: 
                titleText.text = "The SPace traveler";
                GameManager.Instance.YarnDialogueSys.SetActive(true);
            break;
        }
    }
}
