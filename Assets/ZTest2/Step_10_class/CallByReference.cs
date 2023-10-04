using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step10
{
    [System.Serializable]
    public class ItemBase
    {
        public int itemcode;
        public string itemname;
        public int itemtype; //0 무기 , 100 소모템 ....
        //.........
    }

    [System.Serializable]
    public class ItemData
    {
        //class -> ItemOrignalData (class)
        public ItemBase itemBase;
        public int itemcode;
        //public 아이템 종류 type;
        public int count;
        public int update;
    }

    public class CallByReference : MonoBehaviour
    {
        //public Dictionary<int, ItemBase> dic_ItemBase = new Dictionary<int, ItemBase>();
        public List<ItemBase> list_ItemBase = new List<ItemBase>();
        public List<ItemData> listInventory = new List<ItemData>();

        ItemBase GetItemBase(int _itemcode)
        {
            ItemBase _rtn = null;
            foreach (ItemBase _itemBase in list_ItemBase)
            {
                if (_itemBase.itemcode == _itemcode)
                {
                    _rtn = _itemBase;
                    break;
                }
            }
            return _rtn;
        }

        void Start()
        {
            for (int i = 0; i < listInventory.Count; i++)
            {
                ItemBase _itembase = GetItemBase(listInventory[i].itemcode);
                listInventory[i].itemBase = _itembase;

                listInventory[i].itemBase.itemname += "@";
            }


            List<ItemData> _inventory = listInventory;
            ItemData _itemData = _inventory[2];
            _itemData.count += 1;

        }
    }
}