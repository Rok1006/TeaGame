using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollInstruction : MonoBehaviour
{
    public bool hovering;
    public Outline theOutline;
    public GameObject scrollMenu;
    public bool menuOpened;
    public int pageNum = 0;  //ingredient page
    int maxPage = 3;
    public Animator scrollAnim;
    public TextMeshProUGUI contentText;
    void Start()
    {
        theOutline = GetComponent<Outline>();
        theOutline.enabled = false;
        scrollMenu.SetActive(false);
    }
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
            //Time.timeScale = 0;
        }
        else
        {
            scrollMenu.SetActive(false);
            //Time.timeScale = 1;
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
    public void FlipLeft()
    {
        if(pageNum>0){
          scrollAnim.SetTrigger("flip");  
          pageNum-=1;
          PageContent();
        }  
    }
    public void FlipRight()
    {
        if(pageNum<maxPage){
          scrollAnim.SetTrigger("flip");  
          pageNum+=1;
          PageContent();
        }  
    }
    public void PageContent(){
        switch(pageNum){
            case 0:
                //setactive
                contentText.text = "Ingredients";
            break;
            case 1:
                contentText.text = "Tea Types";
            break;
        }
    }
}
