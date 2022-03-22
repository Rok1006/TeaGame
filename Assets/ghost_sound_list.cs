using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_sound_list : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> clips;
    public static bool play;
    AudioSource aud;
    public static string clip_name;
    void Start()
    {
        aud = GetComponent<AudioSource>();
        play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (play) {
            foreach (AudioClip clip in clips)
            {
                if (clip.name == clip_name)
                {
                    aud.PlayOneShot(clip);
                    play = false;
                }
               
            }
        }
    }
}
