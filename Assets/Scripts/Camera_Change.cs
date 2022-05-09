using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Camera_Change 
{
    [YarnCommand("Camera_Change")]  
      public static void Camera_Change_1(){
        ZoneStabllize.Instance.zoneHarm = true;
          CamSwitch.Instance.TeaCamOn();
        TeaCeremonyManager.Instance.currentTutorialState = TeaCeremonyManager.TutorialState.FreePlay;
        Debug.Log("change");
    }
}
