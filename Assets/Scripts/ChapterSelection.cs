using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelection : MonoBehaviour
{
    public List<GameObject> chapters;
    public GameObject chapterParent;
    public bool opened;
    public int theIndex = -1;
    public int selectedIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (ES3.KeyExists("biggestIndex"))
        {
            theIndex = ES3.Load<int>("biggestIndex");
        }
        ES3.Save("selectedIndex", selectedIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (chapterParent.activeSelf)
        {
            opened = true;
        }

        if (opened)
        {
            if (theIndex >= 0)
            {
                for (int i = 0; i < chapters.Count; i++)
                {
                    if (i <= theIndex)
                    {
                        chapters[i].SetActive(true);
                    }
                    else
                    {
                        chapters[i].SetActive(false);
                    }
                }
            }
            else
            {
                foreach (GameObject chapter in chapters)
                {
                    chapter.SetActive(false);
                }
            }
            opened = false;
        }
    }


    public void selectGhost(int idx)
    {
        ES3.Save("selectedIndex", idx);
    }

}
