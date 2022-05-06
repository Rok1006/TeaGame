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
            int selectedIndex = -1;
            if (ES3.KeyExists("selectedIndex"))
            {
                selectedIndex = ES3.Load<int>("selectedIndex");
            }
            if (selectedIndex >= 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().ghostIndex = selectedIndex;
            }
            else if (ES3.KeyExists("biggestIndex"))
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
            if (!ES3.KeyExists("biggestIndex"))
            {
                ES3.Save("ghostIndex", index);
            }
            ES3.Save("selectedIndex", -1);
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
