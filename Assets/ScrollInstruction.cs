using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollInstruction : MonoBehaviour
{
    public bool hovering;
    public Outline theOutline;
    public GameObject scrollMenu;
    public bool menuOpened;
    // Start is called before the first frame update
    void Start()
    {
        theOutline = GetComponent<Outline>();
        theOutline.enabled = false;
        scrollMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hovering)
        {
            if (!menuOpened)
            {
                theOutline.enabled = true;
            }
            else
            {
                theOutline.enabled = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                menuOpened = true;
            }
        }
        else
        {
            theOutline.enabled = false;
        }


        if (menuOpened)
        {
            scrollMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            scrollMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }


    

    private void OnMouseOver()
    {
        hovering = true;
    }

    private void OnMouseExit()
    {
        hovering = false;
    }


    public void closeMenu()
    {
        menuOpened = false;
    }
}
