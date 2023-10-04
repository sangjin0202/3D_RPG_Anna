using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Item : MonoBehaviour
{
    public Transform gfx;
    public ItemDrop itemDrop;
    //public ItemInfoBase itemInfoBase;

    public void InitItemInfoDrop(int _itemcode, int _count)
    {
        BoxCollider _col = GetComponent<BoxCollider>();
        _col.center = gfx.localPosition;
        gameObject.SetActive(true);

        itemDrop = new ItemDrop(ItemInfoManager.ins.GetItemInfoBase(_itemcode));
    }

    public ItemDrop GetItemDrop()
    {
        return itemDrop;
    }

    public void DestroyItem()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

 
}
