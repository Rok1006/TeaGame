using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public string sceneName;
    public GameObject FadeIn;
    public GameObject Camera;
    Animator camAnim;
    public GameObject ControlPanel;
    void Start()
    {
        camAnim = Camera.GetComponent<Animator>();
        FadeIn.SetActive(false);
        ControlPanel.SetActive(false);
    }

    void Update()
    {
        
    }
    public void StartButton(){
        StartCoroutine(IntoPlayScene());
    }
    IEnumerator IntoPlayScene(){
        camAnim.SetTrigger("start");
        yield return new WaitForSeconds(1.3f);
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LoadScene();


    }
    void LoadScene(){
        SceneManager.LoadScene(sceneName);
    }
    public void ControlButton(){
        ControlPanel.SetActive(true);
    }
    public void BackButton(){
        ControlPanel.SetActive(false);
    }
}