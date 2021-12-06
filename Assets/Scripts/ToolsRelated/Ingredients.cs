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
    }

    void OnMouseExit(){
        oc.enabled = false;
    }
    void OnMouseDown() {
        TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab);
        Tea.Instance.ChangeIngredientType(ingredientName);
    }
}
