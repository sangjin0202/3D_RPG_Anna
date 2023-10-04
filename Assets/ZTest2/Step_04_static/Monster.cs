using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step04
{
    public class Monster : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StaticTest.ins.x = 99;
            Debug.Log(StaticTest.ins.x);
            Debug.Log(StaticTest.x2);
            Debug.Log(StaticTest.ins.Add(Vector3.one, Vector3.one));
            Debug.Log(StaticTest.Add2(1,2));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}