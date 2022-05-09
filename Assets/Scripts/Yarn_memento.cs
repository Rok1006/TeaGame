using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarn_memento : MonoBehaviour
{
    [YarnCommand("Momento")]
    public static void Momento() {
        MomentoDisplay.Instance.MomentoBox.SetActive(true);
        MomentoDisplay.Instance.GiveMomento();
    }
}
