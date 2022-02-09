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
        titleText.text = LevelData.Instance.title.ToString();//display title
        GameManager.Instance.ghostIndex = LevelData.Instance.ghostNum; //get the num from levelindex
        //load everything here
        print("Day "+LevelData.Instance.ghostNum+" - "+LevelData.Instance.title);
        //get data from LevelData and proceed to load character and stuff
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
