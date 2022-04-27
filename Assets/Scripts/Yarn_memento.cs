using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarn_memento : MonoBehaviour
{
    // Start is called before the first frame update
    [YarnCommand("Momento")]

    // Update is called once per frame
    public static void Momento() {
        MomentoDisplay.Instance.GiveMomento();
    }
}
