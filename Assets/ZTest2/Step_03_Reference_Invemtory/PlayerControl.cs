using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class PlayerControl : MonoBehaviour
    {
        Transform trans;
        Vector3 move;
        Inventroy inventory;
        Rigidbody rigidbody;
        public float speed = 2f;
        void Start()
        {
            trans = transform;
            rigidbody = GetComponent<Rigidbody>();
            inventory = GetComponent<Inventroy>();
        }

        void Update()
        {
            float _h = Input.GetAxisRaw("Horizontal");
            float _v = Input.GetAxisRaw("Vertical");
            // move = new Vector3(_h, 0, _v);
            move.Set(_h, 0, _v);

            if (move.x != 0 || move.z != 0)
            {
                trans.Translate(move.normalized * speed * Time.deltaTime);
            }

            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

            
        }

        private void OnTriggerEnter(Collider _col)
        {
            if(_col.CompareTag("Item"))
            {
                Item _item = _col.GetComponent<Item>();
                if(_item != null)
                {
                    Debug.Log("아이템 가져옴");
                    // 해당 아이템을 내 인벤토리에 넣어주기
                    ItemData _itemData = _item.GetItemData();
                    bool _eat = inventory.Input_ItemData(_itemData);
                    if (_eat)
                    {
                        //2. 해당 아이템은 필드에서 삭제...
                        _item.DestroyItem();
                    }
                }
            }
            else if(_col.CompareTag("EventZone"))
            {

            }
        }
    }
}
