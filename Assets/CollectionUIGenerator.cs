using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUIGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Unit"), SerializeField] GameObject Unit;
    [SerializeField] int row;
    [SerializeField] int collumn;
    [SerializeField] float xGap, yGap, xStart, yStart;
    private int level_unlocked;
    public GameObject BG;
    void Start()
    {
        xGap = Screen.width / 4f;
        yGap = Screen.height / 4f;
        xStart = xGap;
        yStart = Screen.height - 1.5f*yGap;
        Unit.transform.localScale = new Vector3(Screen.width / 700, Screen.width / 700, Screen.width / 700);
        for (int i = 0; i < collumn; i++)
        {
            for (int n = 0; n < row; n++)
            {
                if (Unit != null)
                {
                    GameObject g = Instantiate(Unit, new Vector2(xStart + (n * xGap), yStart - (i * yGap)), Unit.transform.rotation);
                    g.GetComponent<RectTransform>().SetParent(BG.GetComponent<RectTransform>());

                }
                else
                {
                   
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
