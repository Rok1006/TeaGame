using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*COntent of the script (In ZOne/Effects MAnager)
- Particles effect used in Zone Stabilize.cs
- Lighting animation used with ghost reaction, hand wrong tea
- display scroll from tutorial part
*/
public class Effects : MonoBehaviour
{
    public static Effects Instance;
    public Animator TableLighting;
    public GameObject good;
    public GameObject bad;
    ParticleSystem goodp;
    ParticleSystem badp;
    public GameObject scrollObject;
    Animator scrollAnim;
    public GameObject arrows;
    Animator arrowAnim;
    void Awake() {
        Instance = this;
    }
    void Start()
    {
        goodp = good.GetComponent<ParticleSystem>();
        badp = bad.GetComponent<ParticleSystem>();
        good.SetActive(true);
        bad.SetActive(true);
        goodp.emissionRate = 1.45f;
        badp.emissionRate = 1.45f;

        scrollAnim = scrollObject.GetComponent<Animator>();
        arrowAnim = arrows.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            Scroll_Show();
        }
    }
    public void GoodZoneEffect(){
        // good.SetActive(true);
        // bad.SetActive(false);
        goodp.enableEmission = true;
        badp.enableEmission = false;
    }
    public void BadZoneEffect(){
        // good.SetActive(false);
        // bad.SetActive(true);
        badp.enableEmission = true;
        goodp.enableEmission = false;
    }
    public void Scroll_Show(){  //link this to after sensei mention scroll, this is Effect.cs
        StartCoroutine(ScrollShow());
    }
    IEnumerator ScrollShow() {
        CamSwitch.Instance.ChoiceCamOn();
        yield return new WaitForSeconds(.5f);
        scrollAnim.SetTrigger("show");
        yield return new WaitForSeconds(4f);
        scrollAnim.SetTrigger("back");
        yield return new WaitForSeconds(1f);
        CamSwitch.Instance.ConversationCamOn();
        scrollAnim.SetTrigger("idle");
    }
    public void PlantArrow_Show(){  //this is effect.cs, link this to right before plant dialogue
        StartCoroutine(PlantArrowShow());
    }
    IEnumerator PlantArrowShow(){
        arrowAnim.SetTrigger("plant");
        yield return new WaitForSeconds(1.5f);
        arrowAnim.SetTrigger("Deactivate");
    }
    public void GhostAngry(){
        TableLighting.SetTrigger("ghostAngry");
    }
}
