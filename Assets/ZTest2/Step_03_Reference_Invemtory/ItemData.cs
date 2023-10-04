using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public enum eItemKind { Wear, Weapon, Consume}
    [System.Serializable]
    public class ItemData
    {
        public int itemcode; //코드...
        public string name;
        public string iconName; // name -> sprite(UI)
        public int count = 1;

        public int kind; // 착용템(0), 무기(1), 소모템(2).........
                         // enum... 변경...
        eItemKind itemKind;
        public float att, def, hp, mp;

        ///
        public eItemKind GetKindItem()
        {
            itemKind = (eItemKind)kind;
            return itemKind;
            //switch(kind)
            //{
            //    case 0: itemKind = eItemKind.Wear; break;
            //    case 1: itemKind = eItemKind.Weapon; break;
            //    case 2: itemKind = eItemKind.Consume; break;


            //}
            //return itemKind;
        }
    }
}