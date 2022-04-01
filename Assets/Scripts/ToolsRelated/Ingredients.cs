using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    [Header("Information")]
    public string ingredientName;
    public string category;
    [Header("Others")]
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
        if(!Tutorial.Instance.tutorialComplete){
            GameManager.Instance.arrowAnim.SetTrigger("ingredients");   
        }
       
        if (isAsh)
        {
            if (!haveAsh)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Ash");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category);
                haveAsh = true;
                sc.PickToolUp();
            }
        }
        else if (isBomb)
        {
            if (!haveBomb)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Bomb");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category);
                haveBomb = true;
                sc.PickToolUp();
            }
        }
        else if (isLeaf)
        {
            if (!haveLeaf)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Leaf");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category);
                haveLeaf = true;
                sc.PickToolUp();
            }
        }
        Tea.Instance.ChangeIngredientType(ingredientName);
    }
}
