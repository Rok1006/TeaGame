using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script is to determine each tea type and check if the tea player serve is the same with it
//and offer punishment: lower satsfaction, sensei poof the tea and make u make a new one
//if satisfaction low to 0 u game over
//Place this script in ManagerObj 
public class JudgeTea : MonoBehaviour
{
    public static JudgeTea Instance;
    public bool pass = false;
    public TeaType StudentTeaSOJ1; //student ghost
    public TeaType LaikaSOJ1; //laika
    public TeaType CapitalistSOJ1; //capitalist
    public TeaType currentSOJ;
    [Header("PlayerTea")]  //reset when discard cup and after serve
    public float amtOfLiquid = 0;
    public int heatnessOfWater = 0;
    public int powderAdded = 0; 
    public List<string> IngredientsCatAdded = new List<string>();//add name when player add ingredients
    public string playerMakingOrder;  //"12345"
    void Awake() {
        Instance = this;
    }
    void Start()
    {
       CurrentSOJ();  
    }

    void Update()
    {
       CurrentSOJ(); 
    //    if(Input.GetKey(KeyCode.Space)){
    //        IngredientsCatAdded.Clear();
    //     IngredientsCatAdded.TrimExcess();
    //    }
    }
    public void CurrentSOJ(){
        switch(GameManager.Instance.ghostIndex){
            case 0: //sensei, no need judge
            break;
            case 1: //student ghost
            currentSOJ = StudentTeaSOJ1;
            break;
            case 2: //Laika  //if more than one tea make another switch thing that current = teatype 2.1 or sth
            currentSOJ = LaikaSOJ1;
            break;
            case 3:  //capitalist
            currentSOJ = CapitalistSOJ1;
            break;
        }
    }
    public bool CheckIngredName(){  //check all required categories of ingredients are there
        bool have = false;
        for(int i = 0; i<IngredientsCatAdded.Count; i++){  //what player have
            //var sc.IngredientsAdded[i].GetComponent<Ingredients
            for(int j = 0; j<currentSOJ.IngredientsCategory.Length; j++){  //the standard list
                if(IngredientsCatAdded[i]==currentSOJ.IngredientsCategory[j]){ //if have that ingred in it
                    have = true;
                }else{
                    have = false;
                }
            }
        }
        if(have){
            return true;
        }else{
            return false;
        }
    }
    public bool CheckMakeOrder(){  //do the addthing when using the stuff
    bool right = false;
        for(int i = 0; i<playerMakingOrder.Length; i++){
            for(int j = 0; j<currentSOJ.teaMakingOrder.Length; j++){ 
                if(playerMakingOrder[i]==currentSOJ.teaMakingOrder[i]){
                    right = true;
                }else{
                    right = false; //if one step is wrong it instant break
                    break;
                }
            }
        }
        if(right){return true;
        }else{return false;}
    }
    public void CheckCurrentTea(){  //Before final step
        if(amtOfLiquid == currentSOJ.amtOfLiquid){currentSOJ.enoughLiquid = true;}else{currentSOJ.enoughLiquid = false;}
        if(heatnessOfWater > currentSOJ.heatnessOfWater){currentSOJ.heatnessRight = true;}else{currentSOJ.heatnessRight = false;}
        if(powderAdded == currentSOJ.scoopOfPowder){currentSOJ.enoughPowder = true;}else{currentSOJ.enoughPowder = false;}
        if(CheckIngredName()==true){currentSOJ.ingredientCorrect = true;}else{currentSOJ.ingredientCorrect = false;}
        //stirred is determine at another place?
        if(CheckMakeOrder()==true){currentSOJ.correctOrder = true;}else{currentSOJ.correctOrder = false;}
    }
    public bool IFPass(){ //Final Step   if pass(true) ghost happy if fail(false) ghost unhappy/angry
        int count = 0;
        if(currentSOJ.enoughLiquid){count+=1;}
        if(currentSOJ.heatnessRight){count+=1;}
        if(currentSOJ.enoughPowder){count+=1;}
        if(currentSOJ.ingredientCorrect){count+=1;}
        if(currentSOJ.stirred){count+=1;}
        if(currentSOJ.correctOrder){count+=1;}  //this is less harsh
        if(count==7){
            return true;
        }else{
            return false;
        }
    }
    public void ResetPlayerState(){
        amtOfLiquid = 0;
        heatnessOfWater = 0;
        powderAdded = 0; 
        IngredientsCatAdded.Clear();
        IngredientsCatAdded.TrimExcess();
        playerMakingOrder = "";  //"12345"
    }
}
//Sketch
//what about give a number to every action usign the tools, and add that number to a list here , if they match they add to the tea but if nt make the tea slightly bad
