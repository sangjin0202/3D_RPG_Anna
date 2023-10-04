using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

/*
public class ItemManager : MonoBehaviour
{
    #region singleton
    public static ItemManager ins;
    private void Awake()
    {
        ins = this;

    }
    #endregion
    public SpriteAtlas atlas;
    //public Item prefabItem;
    public List<ItemInfoBase> list = new List<ItemInfoBase>();
    

    private void Start()
    {

        //for (int i = 0; i < list.Count; i++)
        //{
        //    switch (list[i].subcategory)
        //    {
        //        case 0: list[i].itemKind = Constant.SUBCATEGORY_WEAR_WEAPON; break;
        //        case 1: list[i].itemKind = Constant.SUBCATEGORY_WEAR_BOTTOM; break;
        //        case 2: list[i].itemKind = Constant.SUBCATEGORY_WEAR_ARMOR; break;
        //        case 3: list[i].itemKind = Constant.SUBCATEGORY_POTION_HEALING; break;
        //    }
        //}
    }

    public Sprite GetIcon(int _itemcode)
    {
        for (int i = 0, imax = list.Count; i < imax; i++)
        {
            if (_itemcode == list[i].itemcode)
            {
                return atlas.GetSprite(list[i].icon);
            }
        }
        return null;
    }

    //public Sprite GetIcon(ItemData _itemdate)
    //{
    //    for (int i = 0, imax = list.Count; i < imax; i++)
    //    {
    //        if (_itemdate.itemcode == list[i].itemcode)
    //        {
    //            return atlas.GetSprite(list[i].icon);
    //        }
    //    }
    //    return null;
    //}

    public ItemInfoBase GetItemInfo(int _itemcode)
    {
        for (int i = 0, imax = list.Count; i < imax; i++)
        {
            if (_itemcode == list[i].itemcode)
            {
                return list[i];
            }
        }
        return null;
    }

    public void CreateItem(Vector3 _pos, int _itemcode)
    {
        ItemInfoBase _itemInfo = GetItemInfo(_itemcode);
        if (_itemInfo != null)
        {
            //Item _item = Instantiate(prefabItem, _pos, Quaternion.identity) as Item;
            Item _item = PoolManager.ins.Instantiate("Item", _pos, Quaternion.identity).GetComponent<Item>();
            _item.InitItemData(new ItemData(_itemcode, _itemInfo, 1));
        }

    }
}

/*
[System.Serializable]
public class ItemInfo
{
    public int itemcode;    //코드...
    //public string name;
    public string icon; // name -> sprite(UI)
    public int category;  
    public int subcategory;  
    //public int itemKind;
    public float att, def, hp, mp;
}
*/
