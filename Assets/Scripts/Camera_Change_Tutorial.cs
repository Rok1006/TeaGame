using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Camera_Change_Tutorial : MonoBehaviour
{
    [YarnCommand("Camera_Tutorial")]
    public static void Camera_Tutorial()
    {
        GameManager.changeToTeaCam = true;
        Debug.Log("change");
    }
}
