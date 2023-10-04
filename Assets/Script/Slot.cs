using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData itemData;

    public Image itemImage;
    public Text txtCount;
    public GameObject spriteCountBorad;
    Ui_Inventory inventory;

    public void SetItemData(ItemData _itemData, Ui_Inventory _inverntory)
    {
        inventory = _inverntory;
        itemData = _itemData;
        itemImage.sprite = ItemInfoManager.ins.GetIcon(_itemData.itemcode);
        Debug.Log("_itedata,itemcode:" + itemData.itemcode);
        Debug.Log("_itedata,sprite:" + itemImage.sprite);
        ItemInfoBase _itemInfoBase = itemData.itemInfoBase;

        //Debug.Log(this + "SetItemData ItemKind:" + _itemInfo.itemKind);
        switch (_itemInfoBase.subcategory)
        {
            case Constant.SUBCATEGORY_WEAR_WEAPON:
            case Constant.SUBCATEGORY_WEAR_ARMOR:
            case Constant.SUBCATEGORY_WEAR_BOTTOM:
            case Constant.SUBCATEGORY_WEAR_BOOTS:
                spriteCountBorad.SetActive(false);
                break;
            case Constant.SUBCATEGORY_POTION_HEALING:
            case Constant.SUBCATEGORY_RANDOM_BOX:
                // 수량 갱신
                //Debug.Log("슬롯아이템 수량확인 " + itemData.count);
                txtCount.text = "" + itemData.count;
                break;
            default:
                spriteCountBorad.SetActive(false);
                break;
        }
    }

    public void Invoke_Click()
    {
        // 현재 클릭된것이 무슨 종류인지 -> 물약이면 플레이어에게 물약전달.
        ItemInfoBase _itemInfoBase = itemData.itemInfoBase;
		switch (_itemInfoBase.subcategory)
		{
            case Constant.SUBCATEGORY_WEAR_WEAPON:
            case Constant.SUBCATEGORY_WEAR_ARMOR:
            case Constant.SUBCATEGORY_WEAR_BOTTOM:
            case Constant.SUBCATEGORY_WEAR_BOOTS:
                Debug.Log("@@@@@장비교체나 버리기");
                break;
            case Constant.SUBCATEGORY_POTION_HEALING:
                Debug.Log("@@@@@물약 먹기");
                bool _eat = inventory.SetHealth(((Food)_itemInfoBase).eathp);
				
                if (_eat)
				{
                    itemData.count--;
                    if (itemData.count <= 0)
                    {
                        inventory.RemoveSlot(this);
                        gameObject.SetActive(false);
                    }
                }
                txtCount.text = "" + itemData.count;
                break;
            case Constant.SUBCATEGORY_RANDOM_BOX:
                Debug.Log("@@@@@랜덤 박스");
                itemData.count--;
                txtCount.text = "" + itemData.count;
				break;
		}

		Debug.Log(this + "Invoke_Click" + itemData.itemInfoBase.ToString());

    }
}
