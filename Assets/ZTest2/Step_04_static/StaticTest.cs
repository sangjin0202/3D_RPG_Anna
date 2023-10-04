using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step04
{
    public class StaticTest : MonoBehaviour
    {
        public int x;
        public static int x2;
        public static StaticTest ins;
        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
            }
            else if (ins != null && ins != this)
            {
                DestroyImmediate(this);
            }
        }

        public Vector3 Add(Vector3 _x, Vector3 _y)
        {
            return _x + _y;
        }
        public static int Add2(int _x, int _y)
        {
            return _x + _y;
        }
    }
}