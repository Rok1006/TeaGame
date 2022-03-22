using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Audio_yarn : MonoBehaviour
{
    // Start is called before the first frame update

    [YarnCommand("SE")]

    public static void SE(string destinationName)
    {
        ghost_sound_list.play = true;
        ghost_sound_list.clip_name = destinationName;
    }
}
