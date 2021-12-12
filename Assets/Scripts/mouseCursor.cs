using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseCursor : MonoBehaviour
{
    private Image sr;
    public Sprite c1;
    public Sprite c2;
    void Start()
    {
        Cursor.visible = false;
        sr = GetComponent<Image>();
    }
    private void Update() {
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     // transform.position = cursorPos;
    //     if(Physics.Raycast(ray, out RaycastHit raycastHit)){
    //         transform.position = raycastHit.point;
    //     } 
    Vector3 mousePos = Input.mousePosition;
    transform.position = mousePos;     
        if(Input.GetMouseButton(0)){
            sr.sprite = c2;
        }else if(Input.GetMouseButtonUp(0)){
            sr.sprite = c1;
        }
    }
    // void Update()
    // {
    //     // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     // // transform.position = cursorPos;
    //     // if(Physics.Raycast(ray, out RaycastHit raycastHit)){
    //     //     transform.position = raycastHit.point;
    //     // }

    //     if(Input.GetMouseButton(0)){
    //         sr.sprite = c2;
    //     }else if(Input.GetMouseButtonUp(0)){
    //         sr.sprite = c1;
    //     }
    // }
}
