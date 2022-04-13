using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//This script is on the scroll gameobject
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
    public GameObject[] pages;
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
                DisableAllPage();
                StartCoroutine(ScrollEntry());
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
    IEnumerator ScrollEntry(){ 
       yield return new WaitForSeconds(.5f);
       PageContent();
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
                DisableAllPage();
                pages[0].SetActive(true);
            break;
            case 1:
                contentText.text = "Tea Types";
                DisableAllPage();
                pages[1].SetActive(true);
            break;
            case 2:
                contentText.text = "Tea Types";
                DisableAllPage();
                pages[2].SetActive(true);
            break;
        }
    }
    void DisableAllPage(){
        pages[0].SetActive(false);
        pages[1].SetActive(false);
        pages[2].SetActive(false);
    }
}
