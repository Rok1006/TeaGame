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
    public int GhostTeaNum = 0;
    public bool pass = false;
    public TeaType StudentTeaSOJ1; //student ghost
    public TeaType LaikaSOJ1; //laika
    public TeaType LaikaSOJ2; //laika
    public TeaType CapitalistSOJ1; //capitalist
    public TeaType CapitalistSOJ2; //capitalist
    public TeaType CapitalistSOJ3; //capitalist
    public TeaType currentSOJ;
    [Header("PlayerTea")]  //reset when discard cup and after serve
    public float amtOfLiquid = 0;
    public int heatnessOfWater = 0;
    public int powderAdded = 0; 
    public List<string> IngredientsCatAdded = new List<string>();//add name when player add ingredients
    public List<string> IngredientsRankAdded = new List<string>();//add ingredient grade when add in gred
    public string playerMakingOrder;  //"12345"
    [Header("DetermineTea")]
    public bool correctOrder = false;
    public bool enoughLiquid = false;
    public bool heatnessRight = false;
    public bool enoughPowder = false;
    public bool correctIngredAmount = false;
    public bool ingredientCorrect = false;
    public bool stirred = false;
    public int TeaFlavour; //the score of the tea, default ?
    public static bool hot;
    public static bool cold;
    public static bool bitter;
    public static bool light;
    public static bool little;
    public static bool much;
    public static bool ingre;
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
        if(CheckAmtofPowder()&&CheckIngredFlavour()){
            Tea.Instance.targetColor = currentSOJ.teaColor;
        }else{
            Tea.Instance.TeaColorChange();
        }
        
    }
    public void CurrentSOJ(){
        switch(GameManager.Instance.ghostIndex){
            case 0: //sensei, no need judge
                currentSOJ = StudentTeaSOJ1;
            break;
            case 1: //student ghost
                GetPlayerTeaState();
                currentSOJ = StudentTeaSOJ1;
            break;
            case 2: //Laika  //if more than one tea make another switch thing that current = teatype 2.1 or sth
                GetPlayerTeaState();
                switch(GhostTeaNum){
                    case 0:
                        currentSOJ = LaikaSOJ1;
                        if(IFPass()){GhostTeaNum = 1; }
                    break;
                    case 1:
                        currentSOJ = LaikaSOJ2;
                        if(IFPass()){GhostTeaNum = 2; }
                    break;
                    case 2:
                        // GhostTeaNum = 0;
                    break;
                }
            break;
            case 3:  //capitalist
                GetPlayerTeaState();
                switch(GhostTeaNum){
                    case 0:
                        currentSOJ = CapitalistSOJ1;
                        //if(IFPass()){GhostTeaNum = 1; }
                    break;
                    case 1:
                        currentSOJ = CapitalistSOJ2;
                        //if(IFPass()){GhostTeaNum = 2; }
                    break;
                    case 2:
                        currentSOJ = CapitalistSOJ3;  //reset after pass
                        //if(IFPass()){GhostTeaNum = 3; }
                    break;
                    case 3:
                        // GhostTeaNum = 0;
                    break;
                }
            break;
        }
    }
    public bool CheckIngredFlavour(){  //check there is certain ingred
        bool haveF = false;
        bool haveR = false;
        int FCount = 0;
        int RCount = 0;
        // if(IngredientsRankAdded.Count>0){ 
            for(int i = 0; i<IngredientsCatAdded.Count; i++){  //what player have
                for(int j = 0; j<currentSOJ.IngredientsCategory.Length; j++){  //the standard list
                    if(IngredientsCatAdded[i]==currentSOJ.IngredientsCategory[j]){ //if have that ingred in it
                        FCount+=1;
                    }else{
                        FCount+=0;
                    }
                }
            }
            if(FCount>=currentSOJ.IngredientsCategory.Length){
                haveF = true;
            }else{
                haveF = false;
            }
            for(int i = 0; i<IngredientsRankAdded.Count; i++){  //what player have
                for(int j = 0; j<currentSOJ.IngredientsCategory.Length; j++){  //the standard list
                    if(IngredientsRankAdded[i]==currentSOJ.IngredientsCategory[j]){ //if have that ingred in it
                        RCount+=1;
                    }else{
                        RCount+=0;
                    }
                }
            }
            if(RCount>=currentSOJ.IngredientsCategory.Length){
                haveR = true;
            }else{
                haveR = false;
            }
        if(haveF||haveR){
            //Debug.Log(haveR);
            return true;
        }else{
            //Debug.Log(FCount);
            return false;
        }
    }
    public void CompareNumOfIngred(string s){  //use this to check the amount of certain ingred //Condition 4: correct ingredient?
        int numOFIngredInPlayerTea = 0;
        int numOfIngredInStandard = 0;
        for(int i = 0; i<IngredientsCatAdded.Count; i++){
            if(IngredientsCatAdded[i]==s){
                numOFIngredInPlayerTea+=1;
            }
        }
        for(int i = 0; i<IngredientsCatAdded.Count; i++){
            if(IngredientsRankAdded[i]==s){
                numOFIngredInPlayerTea+=1;
            }
        }
        for(int j = 0; j<currentSOJ.IngredientsCategory.Length; j++){
            if(currentSOJ.IngredientsCategory[j]==s){
                numOfIngredInStandard+=1;
            }
        }
        if(numOFIngredInPlayerTea==numOfIngredInStandard){
            correctIngredAmount = true;
        }else{
            //wrong ingredient; trigger dialogue?
            correctIngredAmount = false;
        }
    }
    public void IngredCompareAccordtoTeaType(){
            if(currentSOJ==StudentTeaSOJ1){
                CompareNumOfIngred("sweet");
            }
            if(currentSOJ==LaikaSOJ1){
                CompareNumOfIngred("savory");
            }
            if(currentSOJ==LaikaSOJ2){
                CompareNumOfIngred("savory");
                CompareNumOfIngred("spicy");
            }
            if(currentSOJ==CapitalistSOJ1){
                CompareNumOfIngred("expensive");
            }
            if(currentSOJ==CapitalistSOJ2){
                CompareNumOfIngred("expensive");
                CompareNumOfIngred("cheap");
            }
            if(currentSOJ==CapitalistSOJ3){
                CompareNumOfIngred("cheap");
                CompareNumOfIngred("sweet");
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
    Debug.Log("executed");
        hot = false;cold = false;light = false;little = false;bitter = false;much = false;ingre = false;
        if(CheckLiquidAmt()){
            enoughLiquid = true;
        }else{
            enoughLiquid = false;
        }
        if(CheckWaterTemperature()){
            heatnessRight = true;
        }else{
            heatnessRight = false;
        }
        if(CheckAmtofPowder()){
            enoughPowder = true;
        }else{
            enoughPowder = false;
        }
        if(CheckIngredFlavour()){
            ingredientCorrect = true;
            
        }else{
            ingredientCorrect = false;
            ingre = true;
        }
        // if(IngredCompareAccordtoTeaType){
        //     correctIngredAmount = true;
        // }else{
        //     correctIngredAmount = false;
        // }
        IngredCompareAccordtoTeaType();
        if(CheckMakeOrder()==true){correctOrder = true;}else{correctOrder = false;}
    }
    public bool CheckLiquidAmt(){  //Condition 1: enough liquid
        if(amtOfLiquid >= currentSOJ.amtOfLiquid){
            return true;
        }else{
            //too less water;trigger dialogue?
            little = true;
          
            return false;
        }
    }
    public bool CheckWaterTemperature(){ //Condition 2: hot enough?
        if(heatnessOfWater > currentSOJ.heatnessOfWater){ //for conditioned dialogue
            //too hot;trigger dialogue?
            hot = true;
        }else{
            //too cold;trigger dialogue?
            cold = true;
        }
        if(heatnessOfWater >= currentSOJ.heatnessOfWater&&heatnessOfWater <= currentSOJ.maxHeatness){
            return true;
        }else{
            return false;
        }
    }
    public bool CheckAmtofPowder(){   //Condition 3: powder enough?
        if(currentSOJ==StudentTeaSOJ1){  //this one with either one or 2 scoop, others are fixed
            //Debug.Log("student");
            if(powderAdded>0&&powderAdded<=2){
                return true;
            }else{
                return false;
            }
        }else{
            if(powderAdded > currentSOJ.scoopOfPowder){
                //too bitter;trigger dialogue?
                bitter = true;
            }else{
                light = true;
                //too watery;trigger dialogue?
            }
            if(powderAdded == currentSOJ.scoopOfPowder){
                return true;
            }else{
                return false;
            }
        }

    }
    public void GetPlayerTeaState(){
        amtOfLiquid = Tea.Instance.liquidLevel;
        //heatnessOfWater = (int)Tea.Instance.temp;
        powderAdded = Tea.Instance.numOfPowder;
    }
    public bool IFPass(){ //Final Step   if pass(true) ghost happy if fail(false) ghost unhappy/angry
        int count = 0;
        if(enoughLiquid){count+=1;}
        if(heatnessRight){count+=1;}
        if(enoughPowder){count+=1;}
        if(ingredientCorrect){count+=1;}
        if(stirred){count+=1;} //later add back
        if(correctIngredAmount){count+=1;}
        // if(correctOrder){count+=1;}  //later add back
        // if (GameManager.Instance.ghostIndex==0) { return true; }
        
        if(count>=6){
            //Debug.Log(count);
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
        IngredientsRankAdded.Clear();
        IngredientsRankAdded.TrimExcess();
        playerMakingOrder = "";  //"12345"
        correctOrder = false;
        enoughLiquid = false;
        heatnessRight = false;
        enoughPowder = false;
        correctIngredAmount = false;
        ingredientCorrect = false;
        stirred = false;
        //TeaFlavour; //the score of the tea, default ?
    }
}
//Sketch
//what about give a number to every action usign the tools, and add that number to a list here , if they match they add to the tea but if nt make the tea slightly bad
