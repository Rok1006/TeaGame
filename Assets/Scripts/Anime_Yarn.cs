using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Anime_Yarn : MonoBehaviour
{
    [YarnCommand("Anime")]
    public static void Anime(int index, string clip)
    {
        Debug.Log("test");
        Animation_manager.play = true;
        Animation_manager.animation_clip = clip;

        Animation_manager.index = index;
    
        // walk the character to 'position'
    }
}
