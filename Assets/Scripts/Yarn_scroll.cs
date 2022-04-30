using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarn_scroll : MonoBehaviour
{
    [YarnCommand("Scroll")]
    public static void Scroll()
    {Effects.Instance.Scroll_Show();
    }
}
