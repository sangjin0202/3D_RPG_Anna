using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class Item2 : MonoBehaviour
    {
        //private void OnMouseDown()
        //{
        //    Debug.Log(this + "OnMouseDown");
        //}

        public void ClickEvent()
        {
            Debug.Log(this + "OnMouseDown");
            Item2Manager.ins.SetItem2(this);
        }

        public void RemoveEvent()
        {
            Item2Manager.ins.RenoveItem2(this);
        }
    }
}