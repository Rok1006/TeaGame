using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public bool enableSave = false;
    private void Start()
    {
        if (enableSave)
        {
            if (ES3.KeyExists("biggestIndex"))
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ghostIndex = ES3.Load<int>("biggestIndex");
            }
        }
    }
    // Start is called before the first frame update
    public void toSave(int index)
    {
        if (enableSave)
        {
            ES3.Save("ghostIndex", index);
            int theIndex = ES3.Load<int>("ghostIndex");
            Debug.Log(theIndex + " saved");
        }
    }

    public int toLoad()
    {
        int theIndex = 0;
        if (enableSave)
        {
            
            if (ES3.KeyExists("ghostIndex"))
            {
                theIndex = ES3.Load<int>("ghostIndex");
            }
            
        }
        return theIndex;
    }

    public void saveBiggest(int index)
    {
        if (enableSave)
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
    }

    public int loadBiggest()
    {
        int theIndex = 0;
        if (enableSave)
        {

            if (ES3.KeyExists("biggestIndex"))
            {
                theIndex = ES3.Load<int>("biggestIndex");
            }
            
        }
        return theIndex;
    }

    public List<string> collectionLoad()
    {
        List<string> collection = new List<string>();
        if (enableSave)
        {
            if (ES3.KeyExists("collection"))
            {
                collection = ES3.Load<List<string>>("collection");
            }
        }
        return collection;
    }

    public void collectionSave(string itemName)
    {
        if (enableSave)
        {
            List<string> collection = new List<string>();
            if (ES3.KeyExists("collection"))
            {
                collection = ES3.Load<List<string>>("collection");
            }
            bool nameExists = false;
            foreach (string s in collection)
            {
                if (s == itemName)
                {
                    nameExists = true;
                    break;
                }
            }
            if (!nameExists)
            {
                collection.Add(itemName);
            }
            Debug.Log(itemName + " saved");
            ES3.Save("collection", collection);
        }
    }

}
