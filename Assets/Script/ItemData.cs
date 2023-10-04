using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemData
{

    public int itemcode //코드...
    {
       get {return itemInfoBase.itemcode; }
    }
    public ItemInfoBase itemInfoBase;
    public int count = 1;
    public ItemData(ItemInfoBase _itemInfoBase, int _count = 1)
    {
        itemInfoBase    = _itemInfoBase;
        count           = _count;
    }

    //public ItemData(int _itemcode, ItemInfoBase _itemInfoBase, int _count = 1)
    //{
    //    itemcode = _itemcode;
    //    itemInfoBase = _itemInfoBase;
    //    count = _count;
    //}

    public void Init()
    {
        //itemInfoBase = ItemInfoManager.ins.GetItemInfoBase(itemcode);
    }

    ///
    public int GetSubCategory()
    {
        return itemInfoBase.subcategory;
    }
}
