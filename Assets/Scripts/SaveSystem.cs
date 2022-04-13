using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private void Start()
    {
        if (ES3.KeyExists("biggestIndex"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().ghostIndex = ES3.Load<int>("biggestIndex");
        }
    }
    // Start is called before the first frame update
    public void toSave(int index)
    {
        ES3.Save("ghostIndex", index);
        int theIndex = ES3.Load<int>("ghostIndex");
        Debug.Log(theIndex + " saved");
    }

    public int toLoad()
    {
        int theIndex = 0;
        if (ES3.KeyExists("ghostIndex"))
        {
            theIndex = ES3.Load<int>("ghostIndex");
        }
        return theIndex;
    }

    public void saveBiggest(int index)
    {
        int theIndex = 0;
        if (ES3.KeyExists("ghostIndex"))
        {
            theIndex = ES3.Load<int>("ghostIndex");
        }
        if (index > theIndex)
        {
            theIndex = index;
        }
        ES3.Save("biggestIndex", theIndex);
    }

    public int loadBiggest()
    {
        int theIndex = 0;
        if (ES3.KeyExists("biggestIndex"))
        {
            theIndex = ES3.Load<int>("biggestIndex");
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
