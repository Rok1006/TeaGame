using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataLoad : MonoBehaviour
{
    void Awake() {
        print("Day "+LevelData.Instance.dayNum+" - "+LevelData.Instance.customerName);
        //get data from LevelData and proceed to load character and stuff
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
