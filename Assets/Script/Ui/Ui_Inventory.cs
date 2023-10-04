using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Inventory : MonoBehaviour
{
    #region singleton
    public static Ui_Inventory ins;
    private void Awake()
    {
        ins = this;
        
    }
    #endregion
    
    public int inventorySize = 20;
    public Slot slotPrefap;
    //public GameObject backBoard;
    public GameObject body;
    public Transform itemParent;
    public List<ItemData> list = new List<ItemData>();
    public List<Slot> listSlot = new List<Slot>();
    Player player;

    void Start()
    {
        Invoke_Close();
    }

    public void SetPlayer(Player _player)
	{
        player = _player;
	}

    public bool SetHealth(float _plusHp)
	{
        return player.SetHealth(_plusHp);
	}

    public void Invoke_Close()
    {
        body.SetActive(false);
        //backBoard.SetActive(false);
    }

    public void Invoke_Open()
    {
        // 인벤토리가 활성화되어있으면 또열리면안된다
        if (body.activeSelf == true)
            return;

        // 인벤토리,프리팹 활성화
        body.SetActive(true);
        //backBoard.SetActive(true);
        slotPrefap.gameObject.SetActive(true);

        // 0. 기존슬롯의 오브젝트 삭제
        // list 연결고리 삭제
        for (int i = listSlot.Count-1; i >= 0; i--)
        {
            Destroy(listSlot[i].gameObject);
        }
        listSlot.Clear();

        // 1. 리스트를 순회하면서 인벤토리화면 생성
        for (int i = 0; i < list.Count; i++)
        {
            // 인벤토리 슬롯을 생성 + 셋팅한다
            Slot _slot = Instantiate(slotPrefap, itemParent) as Slot;
            _slot.SetItemData(list[i], this);

            // 버튼 이벤트를 등록한다
            _slot.GetComponent<Button>().onClick.AddListener(_slot.Invoke_Click);

            // 삭제하기위해 리스트에저장
            listSlot.Add(_slot);
        }

        // 2. 다사용한 프리팹 off
        slotPrefap.gameObject.SetActive(false);
    }

    public void RemoveSlot(Slot _slot)
	{
		if (listSlot.Contains(_slot))
		{
            listSlot.Remove(_slot);
		}
	}

    public bool Input_ItemData(ItemDrop _newItemDrop)
    {
        bool _rtn = false;
        bool _bPlus = false;
        ItemInfoBase _newItemInfoBase = _newItemDrop.itemInfoBase;
        int _count = _newItemDrop.count;
        //Debug.Log(_newitemData.itemcode);

        if (list.Count < inventorySize)
        {
            // 리스트에 추가
            int _subcategory = _newItemInfoBase.subcategory;
            switch (_subcategory)
            {
                case Constant.SUBCATEGORY_WEAR_WEAPON:
                case Constant.SUBCATEGORY_WEAR_ARMOR:
                case Constant.SUBCATEGORY_WEAR_BOTTOM:
                case Constant.SUBCATEGORY_WEAR_BOOTS:
                    //wear -> 한개당 한슬롯...
                    //Debug.Log("@@필드에떨어진 아이템 클래스로 변경...");
                    list.Add(new ItemData(_newItemInfoBase, 1));
                    break;
                case Constant.SUBCATEGORY_POTION_HEALING:
                    _bPlus = false;
                    for (int i = 0; i < list.Count; i++)
                    {
                        //Debug.Log(i + " >> " + list[i].itemcode);
                        if (list[i].itemcode == _newItemInfoBase.itemcode)
                        {
                            //Debug.Log("전 소모템 수량누적 : " + list[i].count);
                            //Debug.Log("@@필드에떨어진 아이템 클래스로 변경...");
                            list[i].count += _count; //_newitemInfoBase.count;
                            //Debug.Log("후 소모템 수량누적 : " + list[i].count);
                            _bPlus = true;
                        }

                    }
                    //소모템 -> 한슬롯당 ~Max량..
                    //Debug.Log("@@@@ 소모템 분류해서 넣기 @@@");
                    //소모템에서 수량을 추가하지 않는놈을 추가한다.
                    if (_bPlus == false)
                    {
                        list.Add(new ItemData(_newItemInfoBase, _count));
                    }
                    break;
            }
            //인벤토리에 넣어주면서 인벤토리를 갱신할 필요가 없다.
            //Debug.Log("@@@@ 인벤토리 넣은후 UI갱신@@@@");
            _rtn = true;
        }


        return _rtn;
    }
 
}
