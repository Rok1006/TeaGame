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

    public int toLoad()
    {
        int theIndex = 0;
        if (ES3.KeyExists("ghostIndex"))
        {
            theIndex = ES3.Load<int>("myInt");
        }
        return theIndex;
    }

    public List<string> collectionLoad()
    {
        List<string> collection = new List<string>();
        if (ES3.KeyExists("collection"))
        {
            collection = ES3.Load<List<string>>("collection");
        }
        return collection;
    }

    public void collectionSave(string itemName)
    {
        List<string> collection = new List<string>();
        if (ES3.KeyExists("collection"))
        {
            collection = ES3.Load<List<string>>("collection");
        }
        collection.Add(itemName);
        ES3.Save("collection", collection);
    }

}
