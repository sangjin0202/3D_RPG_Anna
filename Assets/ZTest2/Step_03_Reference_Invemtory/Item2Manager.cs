using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class Item2Manager : MonoBehaviour
    {
        public static Item2Manager ins;
        private void Awake()
        {
            ins = this;
        }
        public List<Item2> list = new List<Item2>();

        public void SetItem2(Item2 _item2)
        {
            if (!list.Contains(_item2))
            {
                list.Add(_item2);
            }
        }

        public void RenoveItem2(Item2 _item2)
        {
            if (list.Contains(_item2))
            {
                list.Remove(_item2);
            }
        }
    }
}