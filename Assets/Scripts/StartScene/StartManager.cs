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
    public GameObject CollectionPanel;
    public GameObject ChapterPanel;
    public AudioSource UI_SE;
    public AudioClip clicked;

    // [Header("LevelSelection")]
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
        UI_SE.PlayOneShot(clicked);
    }
    public void ChaptersButton(){
        ChapterPanel.SetActive(true);
        UI_SE.PlayOneShot(clicked);
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
        UI_SE.PlayOneShot(clicked);
    }
    public void ControlButton(){
        ControlPanel.SetActive(true);
        UI_SE.PlayOneShot(clicked);
    }
    public void BackButton(){
        ControlPanel.SetActive(false);
        ChapterPanel.SetActive(false);
        UI_SE.PlayOneShot(clicked);
    }
    public void BackButton_2()
    {
        CollectionPanel.SetActive(false);
        UI_SE.PlayOneShot(clicked);
    }
    public void CollectionButton()
    {
        CollectionPanel.SetActive(true);
        UI_SE.PlayOneShot(clicked);
    }
    //LevelSelection Function
    public void GetDayNum(int num){ //assign the num back in inspector add to button
       LevelData.Instance.ghostNum = num;
    }
    public void GetCustomerName(string t){ //assign the num back in inspector add to button
       LevelData.Instance.title = t;
    }
 
    }
