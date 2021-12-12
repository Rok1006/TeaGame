using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    public string ingredientName;
    public Outline oc;
    public GameObject IngredientPrefab;

    public bool isAsh;
    public bool isBomb;
    public bool isLeaf;

    public static bool haveAsh;
    public static bool haveBomb;
    public static bool haveLeaf;

    public static GameObject ashObj;
    public static GameObject bombObj;
    public static GameObject leafObj;
    public SoundManager sc;
    void Start()
    {
        oc.enabled = false;
        sc = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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
        if (isAsh)
        {
            if (!haveAsh)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Ash");
                haveAsh = true;
                sc.PickToolUp();
            }
        }
        else if (isBomb)
        {
            if (!haveBomb)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Bomb");
                haveBomb = true;
                sc.PickToolUp();
            }
        }
        else if (isLeaf)
        {
            if (!haveLeaf)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Leaf");
                haveLeaf = true;
                sc.PickToolUp();
            }
        }
        Tea.Instance.ChangeIngredientType(ingredientName);
    }
}
