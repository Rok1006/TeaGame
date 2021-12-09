using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    public string ingredientName;
    public Outline oc;
    public GameObject IngredientPrefab;
    void Start()
    {
        oc.enabled = false;
    }

    void Update()
    {
        
    }
    void OnMouseOver() {
        //go up a little
        oc.enabled = true;
        GoToDrawer.Instance.IGText = ingredientName;
    }

    void OnMouseExit(){
        oc.enabled = false;
        GoToDrawer.Instance.IGText = "";
    }
    void OnMouseDown() {
        TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab);
        Tea.Instance.ChangeIngredientType(ingredientName);
    }
}
