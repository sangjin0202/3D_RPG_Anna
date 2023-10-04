using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step06
{
    public class DelegateTest : MonoBehaviour
    {
        public delegate void VOID_FUN_VOID();
        public delegate void VOID_FUN_BOOL(bool _b);
        VOID_FUN_VOID callback;
        void Start()
        {
            //Fun();
            callback += Fun;
            callback += Fun;
            callback += Fun;
            callback += Fun;
            callback += Fun2;
            callback += Fun3;

            if(callback != null)
            {
                callback();
            }

            callback -= Fun;
            callback -= Fun;
            callback -= Fun;
            callback -= Fun;

            if (callback != null)
            {
                callback();
            }
        }

        void Fun()
        {
            Debug.Log("Fun");
        }

        void Fun2()
        {
            Debug.Log("Fun2");
        }
        
        void Fun3()
        {
            Debug.Log("Fun3");
        }

        
    }
}