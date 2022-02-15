using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUIGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int row;
    [SerializeField] int collumn;
    [SerializeField] float xGap, yGap, xStart, yStart;
    public GameObject background;

    [Header("Unit"), SerializeField] GameObject Unit;

    private float XXGap, YYGap;
    void Start()
    {
        xGap = Screen.width / 8.5f;
        yGap = Screen.height / 4f;
        xStart = xGap *1.75f;
        yStart = Screen.height - 1.2f* yGap;
        Unit.transform.localScale = new Vector3(Screen.width / 700, Screen.width / 700, Screen.width / 700);

        for (int i = 0; i < collumn; i++)
        {
            for (int n = 0; n < row; n++)
            {
                if (Unit != null)
                {
                    GameObject g = Instantiate(Unit, new Vector2(xStart + (n * xGap), yStart - (i * yGap)), Unit.transform.rotation);
                    g.GetComponent<RectTransform>().SetParent(background.GetComponent<RectTransform>());
                    
                }
                else
                {
                    Debug.LogWarning("no Backpack Unit object");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
