using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentoUI : MonoBehaviour
{
    public List<string> momentos;
    public List<GameObject> children;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        if (ES3.KeyExists("collection"))
        {
            momentos = ES3.Load<List<string>>("collection");
        }
        foreach (string s in momentos)
        {
            if (s == "Dog Collar")
            {
                children[1].SetActive(true);
            }
            if (s == "Clover")
            {
                children[0].SetActive(true);
                Debug.Log("Clover loaded");
            }
            if (s == "Engagement Ring")
            {
                children[2].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
