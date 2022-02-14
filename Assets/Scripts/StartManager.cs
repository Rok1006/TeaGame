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
<<<<<<< Updated upstream:Assets/Scripts/StartManager.cs
=======
    public GameObject Collection_UI;
    // [Header("LevelSelection")]
>>>>>>> Stashed changes:Assets/Scripts/StartScene/StartManager.cs
    void Start()
    {
        camAnim = Camera.GetComponent<Animator>();
        FadeIn.SetActive(false);
        ControlPanel.SetActive(false);
<<<<<<< Updated upstream:Assets/Scripts/StartManager.cs
    }

    void Update()
=======
        Collection_UI.SetActive(false);

}
void Update()
>>>>>>> Stashed changes:Assets/Scripts/StartScene/StartManager.cs
    {
        
    }
    public void StartButton(){
        StartCoroutine(IntoPlayScene());
    }
    public void CollectionButton() {
        Collection_UI.SetActive(true);
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
<<<<<<< Updated upstream:Assets/Scripts/StartManager.cs
=======

    public void BackButton_2()
    {
        Collection_UI.SetActive(false);
    }

    //LevelSelection Function
    public void GetDayNum(int num){ //assign the num back in inspector add to button
       LevelData.Instance.ghostNum = num;
    }
    public void GetCustomerName(string t){ //assign the num back in inspector add to button
       LevelData.Instance.title = t;
    }
>>>>>>> Stashed changes:Assets/Scripts/StartScene/StartManager.cs
}
