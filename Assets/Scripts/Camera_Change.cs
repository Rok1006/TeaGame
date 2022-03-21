using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Camera_Change
{
    [YarnCommand("Camera_Change")]
    public static void Camera_Change_1()
    {
        CamSwitch.Instance.TeaCamOn();
    }
}

