using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script is for check the state of the tea and its condition, whether it can be serve or not
//this script is on TeaManager
public class TeaState : MonoBehaviour
{
    public GameObject TCMObj; //TeaCeremonyMAnager
    private Tea tcs;
    public Tutorial tutorialSc;
    void Start()
    {
        tcs = TCMObj.GetComponent<Tea>();
    }

    void Update()
    {
        ServableCheck();
    }

    public void ServableCheck(){  //except sensei , if cup have at least sth u can serve
        if (tutorialSc.tutorialComplete)
        {
            if ((tcs.numOfIngredients > 0 || tcs.numOfPowder > 0 || tcs.liquidLevel>0)&&Tea.Instance.toMeltList.Count==0)
            {
                TeaCeremonyManager.Instance.discardButton.GetComponent<Button>().interactable = true;
                TeaCup.Instance.canServe = true;
            }
            else
            {
                TeaCeremonyManager.Instance.discardButton.GetComponent<Button>().interactable = false;
                TeaCup.Instance.canServe = false;
            }
        }
    }
}
