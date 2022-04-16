using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*COntent of the script (In ZOne/Effects MAnager)
- Particles effect used in Zone Stabilize.cs
- Lighting animation used with ghost reaction, hand wrong tea
*/
public class Effects : MonoBehaviour
{
    public static Effects Instance;
    public Animator TableLighting;
    public GameObject good;
    public GameObject bad;
    ParticleSystem goodp;
    ParticleSystem badp;
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
    }

    void Update()
    {
        
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
}
