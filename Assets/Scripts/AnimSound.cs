using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSound : MonoBehaviour
{
    public SoundManager SM;
    public void Effect_In(){
        SM.EffectIn.Play();
    }
    public void Effect_Stay(){
        SM.EffectStay.Play();
    }
    public void Effect_Out(){
        SM.EffectOut.Play();
    }
    public void Effect_End(){
        SM.EffectOut.Stop();
    }
}
