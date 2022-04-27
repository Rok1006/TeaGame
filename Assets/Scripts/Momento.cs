using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//attach this script to each momento
public class Momento : MonoBehaviour
{
    public string name;
    [TextArea(15,20)]
    public string Description;
    public Outline OC;
    public Vector3 rotation;
    void Start()
    {
        OC.enabled = false;
    }

    void Update()
    {
        this.transform.eulerAngles = rotation;
        //this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0.013f);
    }
    void OnMouseOver() {
        OC.enabled = true;
    }
    void OnMouseDown(){
        //put into collection in start screen tgt with name and description
        MomentoDisplay.Instance.momentoAnim.SetTrigger("close");
        Destroy(this.gameObject);
        GameObject.Find("SceneManager").GetComponent<SaveSystem>().collectionSave(name);
    }
    void OnMouseExit(){
        OC.enabled = false;
    }
}
