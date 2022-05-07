using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarn_plant : MonoBehaviour
{
    [YarnCommand("plant")]
    public static void plant()
    {
        Debug.Log("plant");
        Effects.Instance.PlantArrow_Show();
    }
}
