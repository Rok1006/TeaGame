using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button_SE : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource UI_SE;
    public AudioClip hover;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void OnPointerEnter()
    {
        UI_SE.PlayOneShot(hover);
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        Debug.Log("Cursor Entering " + name + " GameObject");
    }
}
