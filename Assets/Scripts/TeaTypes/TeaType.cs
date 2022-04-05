using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GhostTeaType", menuName = "ScriptableObject/Tea")]
public class TeaType : ScriptableObject
{//Rules to tea
    //1. amt of liquid
    //2. heatness of water: have to be heated
    //3. amt of powder
    //4. type of ingredient
    //5. stirred
   // public int numberOfTea = 0; //some ghost have more than one tea. this is the number for 
    public string teaName; //for clarity
    public float amtOfLiquid = 0;
    public int heatnessOfWater = 0;
    public int scoopOfPowder = 0;
    public string[] IngredientsCategory;
    public string teaMakingOrder;  //"12345" 
    [Header("DetermineTea")]
    public bool correctOrder = false;
    public bool enoughLiquid = false;
    public bool heatnessRight = false;
    public bool enoughPowder = false;
    public bool ingredientCorrect = false;
    public bool stirred = false;
    public int TeaFlavour; //the score of the tea, default ?
    
}
/*Tea Making order:
1 = Puting teapot on stove
2 = put powder
3= Add ingredients
4 = Stir Tea 
*/
