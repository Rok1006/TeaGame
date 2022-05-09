using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredients : MonoBehaviour
{
    [Header("Information")]
    public string ingredientName;
    public string[] category;
   // public Tea teaSc;
    [Header("Others")]
    public Outline oc;
    public GameObject IngredientPrefab;

    public bool isAsh;
    public bool isBomb;
    public bool isLeaf;
    public bool isChili;

    public static bool haveAsh;
    public static bool haveBomb;
    public static bool haveLeaf;
    public static bool haveChili;

    public static GameObject ashObj;
    public static GameObject bombObj;
    public static GameObject leafObj;
    public static GameObject chiliObj;
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
        if(TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.GetIngredient||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay){
        oc.enabled = true;
        //TeaCeremonyManager.Instance.CurrentToolName = "IngredPlate";
        GoToDrawer.Instance.IGText = ingredientName;
        }
    }

    void OnMouseExit(){
        oc.enabled = false;
        GoToDrawer.Instance.IGText = "";
    }
    void OnMouseDown() {
        if(TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.GetIngredient||TeaCeremonyManager.Instance.currentTutorialState == TeaCeremonyManager.TutorialState.FreePlay){
        if(!Tutorial.Instance.tutorialComplete){  //!Tutorial.Instance.tutorialComplete&&
            GameManager.Instance.arrowAnim.SetTrigger("Deactivate");
            GameManager.Instance.arrowAnim.SetTrigger("ingredients");   
            //Debug.Log("usiignsjknk");
            GameManager.Instance.tutorialIngredGet = true;
        }
       
        if (isAsh)
        {
            if (!haveAsh)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Ash");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category[0]);
                JudgeTea.Instance.IngredientsRankAdded.Add(this.category[1]);
                haveAsh = true;
                sc.PickToolUp();
            }
        }
        else if (isBomb)
        {
            if (!haveBomb)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Bomb");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category[0]);
                JudgeTea.Instance.IngredientsRankAdded.Add(this.category[1]);
                haveBomb = true;
                sc.PickToolUp();
            }
        }
        else if (isLeaf)
        {
            if (!haveLeaf)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Leaf");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category[0]);
                JudgeTea.Instance.IngredientsRankAdded.Add(this.category[1]);
                haveLeaf = true;
                sc.PickToolUp();
            }
        }
        else if (isChili)
        {
            if (!haveChili)
            {
                TeaCeremonyManager.Instance.IngredientsAdd(IngredientPrefab, "Chili");
                JudgeTea.Instance.IngredientsCatAdded.Add(this.category[0]);
                JudgeTea.Instance.IngredientsRankAdded.Add(this.category[1]);
                haveChili = true;
                sc.PickToolUp();
            }
        }
        Tea.Instance.ChangeIngredientType(ingredientName);
        }
    }
}
