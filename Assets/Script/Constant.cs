using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum eItemKind { Wear, Weapon, Consume }
public enum eGameState { None, Init, Gaming }
public enum eMonsterManagerState { None, Loading, Spawning, Destroy }
public enum ePlayerState { None, Move, Attack, Stun}

public class Constant 
{
    public const bool DEBUG_BOSS            = false;
    public const bool DEBUG_GM              = false;
    public const bool DEBUG_MONSTER         = false;
    public const bool DEBUG_MONSTER_ATTACK  = false;
    public const bool DEBUG_MONSTER_MANAGER = false;
	public const bool DEBUG_ITEMINFOMANAGER = false;
	public const bool DEBUG_CHARINFOMANAGER = false;

    //category
    //subcategory
    public const int    SUBCATEGORY_WEAR_WEAPON             = 1,    //무기(1)
                        SUBCATEGORY_WEAR_ARMOR              = 2,    //상의(2)
                        SUBCATEGORY_WEAR_BOTTOM             = 3,    //하의(3)
                        SUBCATEGORY_WEAR_BOOTS              = 4,    //신발(4)

                        SUBCATEGORY_RANDOM_BOX              = 20,//랜덤박스(20)

                        SUBCATEGORY_POTION_HEALING          = 21,   //물약(21)
                        //SUBCATEGORY_POTION_MANA           = -9999, //마나
                        //SUBCATEGORY_POTION_BUFF           = -9998, //버프

                        SUBCATEGORY_CASHCOST                = 30, //다이아(30)
                        SUBCATEGORY_GAMECOST                = 31, //아데나(31)

                        SUBCATEGORY_INFOMATION_COLLECTION   = 50, //정보수집(50)
                                                                //SUBCATEHORY_QUEST_ITEM            = -9997, //퀘템
                        SUBCATEGORY_INFO_QUEST              = 60, //퀘스트(60)

                        SUBCATEGORY_XXX                     = 9999;

}
