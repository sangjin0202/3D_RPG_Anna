using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class Obstacle : MonoBehaviour
    {
        public void ClickEvent()
        {
            Debug.Log(this + "OnMouseDown");
        }
    }
}