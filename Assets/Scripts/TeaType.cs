using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Customer", menuName = "ScriptableObject/Customer")]
//this script is to determine each tea type and check if the tea player serve is the same with it
//and offer punishment: lower satsfaction, sensei poof the tea and make u make a new one
//if satisfaction low to 0 u game over
public class TeaType : MonoBehaviour
{
    //Rules to tea
    //1. amt of liquid
    //2. heatness of water: have to be heated
    //3. amt of powder
    //4. type of ingredient
    //5. stirred

    //About tutorial tea, no matter how player is making it, as long as there is water and powder in it sensei will be okay
    public static TeaType Instance;
    public int teaType; //use switch
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void CheckCurrentTea(){  //after tutorial is done, check if the tea serve is correct

    }
}
