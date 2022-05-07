using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Yarn_create : MonoBehaviour
{
    [YarnFunction("Read")]
   
    public static bool Read(string bool_name)
    {
        Debug.Log("test");
        
        if (bool_name.Equals("hot")) { return JudgeTea.hot; }
        else if (bool_name.Equals("cold")) { return JudgeTea.cold;} 
        else if (bool_name.Equals("bitter")) { return JudgeTea.bitter; }
        else if (bool_name.Equals("light")) { return JudgeTea.light; }
        else if (bool_name.Equals("little")) { return JudgeTea.little; } 
        else if (bool_name.Equals("much")) { return JudgeTea.much; }
        else if (bool_name.Equals("ingre")) { return JudgeTea.ingre; }
        else { return false; }
    }
}
