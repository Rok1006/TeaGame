using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject ToolSound;
    AudioSource teaPour;
    AudioSource teapotDown;
    AudioSource teapotPickUp;
    AudioSource toolDown;
    AudioSource toolUP;
    public GameObject ToolSound2;
    AudioSource toDrawer;
    AudioSource stirTea;
    void Start()
    {
        AudioSource[] toolAudios = ToolSound.GetComponents<AudioSource>();
        teaPour = toolAudios[0];
        teapotDown = toolAudios[1];
        teapotPickUp = toolAudios[2];
        toolDown = toolAudios[3];
        toolUP = toolAudios[4];
        AudioSource[] toolAudios2 = ToolSound2.GetComponents<AudioSource>();
        toDrawer = toolAudios2[0];
        stirTea = toolAudios2[1];
    }
    public void PourTea(){teaPour.Play();}
    public void PlaceTeaPot(){teapotDown.Play();}
    public void PickUpTeaPot(){teapotPickUp.Play();}
    public void PlaceToolDown(){toolDown.Play();}
    public void PickToolUp(){toolUP.Play();}
    //ToolSound2
    public void OpenDrawer(){toDrawer.Play();}
    public void Stirring(){stirTea.Play();}
}
