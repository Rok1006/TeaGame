using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class next_level
{

    [YarnCommand("next")]
  
    public static void next_ghost()
    {
        GameManager.Instance.GhostLeave();
    }
}
