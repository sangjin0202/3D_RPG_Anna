using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrop
{
        //public int itemcode { get { return itemInfoBase.itemcode; } } //코드...
        public ItemInfoBase itemInfoBase;
        public int count = 1;
        public ItemDrop(ItemInfoBase _itemInfoBase)
        {
            itemInfoBase    = _itemInfoBase;
            count           = 1;
        }

        public ItemDrop(ItemInfoBase _itemInfoBase, int _count = 1)
        {
            itemInfoBase    = _itemInfoBase;
            count           = _count;
        }
}
