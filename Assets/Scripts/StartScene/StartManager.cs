using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public GameObject MomentoPanel;
    public Image momentoImage;
    public TextMeshProUGUI momentoName;
    public TextMeshProUGUI description;
    //public int SavedNum;
    void Awake() {
        FadeIn.SetActive(true);
    }
    // [Header("LevelSelection")]
    void Start()
    {
        camAnim = Camera.GetComponent<Animator>();
        FadeIn.SetActive(false);
        ControlPanel.SetActive(false);
        MomentoPanel.SetActive(false);
    }
    void Update()
    {
        
    }
    public void StartButton(){ //player start where they left off
        StartCoroutine(IntoPlayScene());
        UI_SE.PlayOneShot(clicked);
    }
    public void FirstPlay(){
        LevelData.Instance.ghostNum = 0; //sample
        LevelData.Instance.title = "SENSEI"; //sample
    }
    public void ChaptersButton(){
        ChapterPanel.SetActive(true);
        UI_SE.PlayOneShot(clicked);
    }
    IEnumerator IntoPlayScene(){
        FadeIn.SetActive(false); //new
        camAnim.SetTrigger("start");
        yield return new WaitForSeconds(1.3f);
        FadeIn.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        LoadScene();
        // LevelData.Instance.ghostNum = 0; //sample
        // LevelData.Instance.title = "SENSEI"; //sample
        //get saved ghost index and title and bring it into play scene
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
        UI_SE.PlayOneShot(clicked);
    }
    public void BackButton_2()
    {
        CollectionPanel.SetActive(false);
        UI_SE.PlayOneShot(clicked);
    }
    public void BackButton_3()
    {
        ChapterPanel.SetActive(false);
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
 //Collection Panel-------
    public void ClickMomento(){
        MomentoPanel.SetActive(true);
    }
    public void CloseMomento(){
        MomentoPanel.SetActive(false);
    }
    public void MomentoNameShow(string name){
        momentoName.text = name;
    }
    public void MomentoImgShow(Sprite img){
        momentoImage.sprite = img;
    }
    public void MomentoDesShow(string des){
        description.text = des;
    }
    }

