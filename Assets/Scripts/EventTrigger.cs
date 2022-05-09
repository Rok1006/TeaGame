using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTrigger : MonoBehaviour
{
    public static EventTrigger Instance;
    public GameObject GameOverScreen;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        GameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.wrongCount>3){   //if ghost give wrog time more than 3 times, gameover
            //if have time give some angry anim and lighting switch
            GameOver();
        }
    }
    public void GameOver(){
        GameOverScreen.SetActive(true);
        //Time.timeScale = 0;
    }
    public void ClickMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }
    public void ClickRestart(){ //restart this current ghost
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
