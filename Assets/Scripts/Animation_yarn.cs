using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Animation_yarn : MonoBehaviour
{
    [YarnCommand("Anime")]
    

    public static void Anime(int ghostname,string clip)
    {
        Animation_manager.index = ghostname;
        Animation_manager.animation_clip = clip;
        Animation_manager.play = true;

    }
}
