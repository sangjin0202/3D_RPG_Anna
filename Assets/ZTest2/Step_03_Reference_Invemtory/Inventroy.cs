using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class Inventroy : MonoBehaviour
    {
        public int size = 2;
        public List<ItemData> list = new List<ItemData>();
        public bool Input_ItemData(ItemData _itemData)
        { 
            bool _rtn = false;

            if (list.Count < size)
            {
                // 리스트에 추가
                eItemKind _itemKind = _itemData.GetKindItem();
                switch (_itemKind)
                {
                    case eItemKind.Wear:
                    case eItemKind.Weapon:
                        //wear -> 한개당 한슬롯...
                        list.Add(_itemData);
                        break;
                    case eItemKind.Consume:
                        //소모템 -> 한슬롯당 ~Max량..
                        Debug.Log("@@@@ 소모템 분류해서 넣기 @@@");
                        list.Add(_itemData);
                        break;

                }

                // 추가했어

                //인벤토리에 넣어주면서 인벤토리를 갱신할 필요가 없다.
                //Debug.Log("@@@@ 인벤토리 넣은후 UI갱신@@@@");

                _rtn = true;
            }
            

            return _rtn;
        }

        //public bool Output_Item(ItemData _itemData)
        //{
           
        //    list.Add(_itemData);
        //    return true;
        //}
    }
}
