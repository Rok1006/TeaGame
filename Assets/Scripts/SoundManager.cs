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
    public GameObject EffectSound;
    public AudioSource EffectIn;
    public AudioSource EffectStay;
    public AudioSource EffectOut;
    public GameObject MomentoSound;
    public AudioSource boxBling;
    public AudioSource boxOpen;
    public AudioSource boxClose;
    public AudioSource boxAppear;
    public AudioSource fadeTransition;
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
        AudioSource[] es = EffectSound.GetComponents<AudioSource>();
        EffectIn = es[0];
        EffectStay = es[1];
        EffectOut = es[2];
        AudioSource[] ms = MomentoSound.GetComponents<AudioSource>();
        boxBling = ms[0];
        boxOpen = ms[1];
        boxClose = ms[2];
        boxAppear = ms[3];
    }

    private void Update()
    {
        teaPour.volume = PourDetector.current_emission_rate / 70;
    }
    public void PourTea(){teaPour.Play();}
    public void StopPourTea(){ FadeOut(teaPour, 1.0f); }
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
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        Debug.Log(audioSource.volume);
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

}
