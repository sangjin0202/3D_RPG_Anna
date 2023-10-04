using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Step03
{
    public class Item : MonoBehaviour
    {
        //private void OnTriggerEnter(Collider other)
        //{
        //    Debug.Log(this + "2");
        //}
        public SpriteAtlas atlas;
        public ItemData itemData;

        private void Start()
        {
            SpriteRenderer _r = GetComponentInChildren<SpriteRenderer>();
            if(_r != null)
            {
                _r.sprite = atlas.GetSprite(itemData.iconName);
            }
        }
        public ItemData GetItemData()
        {
            return itemData;
        }
        public void DestroyItem()
        {
            Destroy(gameObject);
        }
    }
}
