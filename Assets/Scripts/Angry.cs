using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Angry : MonoBehaviour
{
    [YarnCommand("Angry")]
    public static void Angry_1()
    {
        GameManager.count = true;
        //GameManager.Instance.wrongCount+=1;
    }
}
