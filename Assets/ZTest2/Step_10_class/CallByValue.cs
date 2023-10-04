using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step10
{
    [System.Serializable]
    public struct PosAndRot
    {
        public Vector3 pos;
        public Quaternion rot;

        public override string ToString()
        {
            return pos + " / " + rot;
        }
    }

    public class CallByValue : MonoBehaviour
    {
        public int age        = 10;
        public float health   = 10;
        public Vector3 pos    = new Vector3(10,10,10);
        public Quaternion rot = Quaternion.identity;
        public PosAndRot pr;

        void Start()
        {
            int _age = age;
            _age *= 10;
            Debug.Log(_age + " : " + age);

            float _health = health;
            _health *= 10f;
            Debug.Log(_health + " : " + health);

            Vector3 _pos = pos;
            _pos *= 10f;
            Debug.Log(_pos + " : " + pos);

            Quaternion _rot = rot;
            _rot.w = 10f;
            Debug.Log(_rot + " : " + rot);

            PosAndRot _pr = pr;
            _pr.pos *= 10f;
            Debug.Log(_pr + " : " + pr);

        }

        void Update()
        {

        }
    }
}