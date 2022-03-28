using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject ControGuide;
    public bool paused;
    void Start()
    {
        pauseScreen.SetActive(false);
        ControGuide.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    void showPaused()
    {
        paused = true;
        pauseScreen.SetActive(true);
    }

    void hidePaused()
    {
        paused = false;
        pauseScreen.SetActive(false);
    }
    public void HoverApp(GameObject triangle){
        triangle.SetActive(true);
    }
    public void HoverDpp(GameObject triangle){
        triangle.SetActive(false);
    }
    public void ControlClick(){
        ControGuide.SetActive(true);
    }
    public void ControlBackCLick(){
        ControGuide.SetActive(false);
    }
 }
