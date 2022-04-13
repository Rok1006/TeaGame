using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameManager gm;
    public GameObject pauseScreen;
    public GameObject ControGuide;
    public GameObject FadeIn;
    public bool paused;
    public bool canGoMenu = false;
    public TeaPot teapostScr;
    public GoToDrawer gotoDrawScr;
    public ScrollInstruction scrollScr;
    public List<Ingredients> ingreScr;
    public StirTool toolScr1;
    public MatchaBox toolScr2;
    public SnackOffer toolScr3;
    public List<Snacks> snackScr;



    public void saveGhostIndex()
    {
        SaveSystem ss = GetComponent<SaveSystem>();
        ss.toSave(gm.ghostIndex);
    }

    public void saveBiggestIndex()
    {
        SaveSystem ss = GetComponent<SaveSystem>();
        ss.saveBiggest(gm.ghostIndex);
    }

    public void goToMainMenu()
    {
        // StartCoroutine(SceneTransition()); //this one not working
        // if(canGoMenu){
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
        Time.timeScale = 1;
        // canGoMenu = false;
        //}
    }
    // public void Dotransition(){
    //     StartCoroutine(SceneTransition());
    // }
    IEnumerator SceneTransition(){
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(2f);
        canGoMenu = true;
    }

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
                pauseObjs(false);
            }
            else
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
                pauseObjs(true);

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
    public void pauseObjs(bool b)
    {
        teapostScr.enabled = b;
        gotoDrawScr.enabled = b;
        scrollScr.enabled = b;
        toolScr1.enabled = b;
        toolScr2.enabled = b;
        toolScr3.enabled = b;
        foreach (Ingredients ingre in ingreScr)
        {
            ingre.enabled = b;
        }
        foreach (Snacks snack in snackScr)
        {
            snack.enabled = b;
        }
    }

 }
