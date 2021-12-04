using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Sprite[] sprArr;
    public Image img;
    int sprIndex = 0;

    public static Tutorial Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    public void NextImage()
    {
        sprIndex++;
        img.sprite = sprArr[sprIndex];
    }
}
