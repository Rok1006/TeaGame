using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public void toSave(int index)
    {
        ES3.Save("ghostIndex", index);
    }

    public int toLoad(int index)
    {
        int theIndex = 0;
        if (ES3.KeyExists("ghostIndex"))
        {
            theIndex = ES3.Load<int>("myInt");
        }
        return theIndex;
    }
}
