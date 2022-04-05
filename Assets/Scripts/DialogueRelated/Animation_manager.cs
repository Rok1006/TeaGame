using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_manager : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public static string animation_clip;
    public static bool play;
    public List<GameObject> ghost;
    public static int index;
    void Start()
    {
        play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            anim = ghost[index].GetComponent<Animator>();

            anim.Play(animation_clip);
            play = false;
        }
      
    }
}
