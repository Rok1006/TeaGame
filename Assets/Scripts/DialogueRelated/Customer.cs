using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Customer", menuName = "ScriptableObject/Customer")]
public class Customer : ScriptableObject
{
    //[HideInInspector]
    public string name;
    public float satisfaction; //temp variable
    [HideInInspector]
    public List<ListItemExample> dialogue;
}
