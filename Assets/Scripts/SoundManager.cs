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
    AudioSource itemDown;
    AudioSource powder;
    public GameObject SelectAndOtherSound;
    AudioSource poof;
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
        itemDown = toolAudios2[2];
        powder = toolAudios2[3];
        AudioSource[] selectOther = SelectAndOtherSound.GetComponents<AudioSource>();
        poof = selectOther[0];
    }
    public void PourTea(){teaPour.Play();}
    public void StopPourTea(){teaPour.Stop();}
    public void PlaceTeaPot(){teapotDown.Play();}
    public void PickUpTeaPot(){teapotPickUp.Play();}
    public void PlaceToolDown(){toolDown.Play();}
    public void PickToolUp(){toolUP.Play();}
    //ToolSound2
    public void OpenDrawer(){toDrawer.Play();}
    public void Stirring(){stirTea.Play();}
    public void ReleaseItem(){itemDown.Play();}
    public void PowderDown(){powder.Play();}
    //Select & Other
    public void Poof(){poof.Play();}

}
