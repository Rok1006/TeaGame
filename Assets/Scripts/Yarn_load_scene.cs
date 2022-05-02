using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class Yarn_load_scene : MonoBehaviour
{
    [YarnCommand("Load")]

    // Update is called once per frame
    public static void Load()
    {
        SceneManager.LoadScene("StartScene");
    }
}
