using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Step02
{
    [System.Serializable]
    public class ItemData
    {
        public int itemcode;
        public string name;
        public int cashcost;
        //..........
    }
    public class ValueReference : MonoBehaviour
    {
        public int intValue = 10;
        public float floatValue = 10f;
        public Vector3 v3Value = new Vector3(10, 10, 10);
        public Ray ray;
        public RaycastHit hit;

        // ------------------------------------------------------
        public int[] intArray = new int[2]; // 10, 11
        public float[] floatArray = new float[2]; // 10f, 11f
        public Vector3[] v3Array = new Vector3[2]; // 10, 11, 12

       

        //-------------------------------------------------------

        public MeshRenderer render;
        ItemData item;
        public List<Vector3> listV3 = new List<Vector3>();
        public List<Transform> listTrans = new List<Transform>();


        void Start()
        {
            item = new ItemData();
            item.cashcost = 10;
            ItemData _item2 = item;
            _item2.cashcost *= 10;
            Debug.Log(item.cashcost + ":" + _item2.cashcost);

            ray = new Ray(transform.position, transform.forward);
            Debug.Log(ray.origin + ":" + ray.direction);
            Ray _ray2 = ray;
            _ray2.origin *= 10;
            Debug.Log(ray.origin + ":" + ray.direction);
            Debug.Log(_ray2.origin + ":" + _ray2.direction);

            render = GetComponent<MeshRenderer>();
            render.material.color = Color.red;

            MeshRenderer _r = render;
            _r.material.color = Color.green;

            Fun(render);


            int _iv = intValue;
            _iv *= 10;
            Fun(intValue);

            float _fv = floatValue;
            _fv *= 10;
            Fun(floatValue);

            Vector3 _vv = v3Value;
            _vv *= 10;
            Fun(v3Value);

            int[] _ia = intArray;
            for (int i = 0; i < _ia.Length; i++)
                _ia[i] *= 10;
            Fun(intArray);

            float[] _fa = floatArray;
            for (int i = 0; i < _fa.Length; i++)
                _fa[i] *= 10;
            Fun(floatArray);

            Vector3[] _va = v3Array;
            for (int i = 0; i < _va.Length; i++)
                _va[i] *= 10;
            Fun(v3Array);

            ShufflyEye(intArray);

            Fun<int>(intValue);
            Fun<MeshRenderer>(render);
            Fun<ItemData>(item);
        }
        
        void Fun<T>(T _v) 
        {
            _v = _v;
        }
        void Fun(MeshRenderer _r)
        {
            _r.material.color = Color.blue;
        }

        void ShufflyEye(int[] _arrayCard)
        {
            //System.Random _rand = new System.Random(99);
            Random.seed = 99;

            int _end = _arrayCard.Length;
            int _temp;
            int _index;
            for (int i = 1; i < _end; i++)
            {
                _index = Random.Range(i, _end);

                _temp               = _arrayCard[i - 1];
                _arrayCard[i - 1]   = _arrayCard[_index];
                _arrayCard[_index]  = _temp;
            }
        }
        void Fun(int[] _v)
        {
            for (int i = 0; i < _v.Length; i++)
                _v[i] *= 10;
        }
        void Fun(float[] _v)
        {
            for (int i = 0; i < _v.Length; i++)
                _v[i] *= 10;
        }
        void Fun(Vector3[] _v)
        {
            for (int i = 0; i < _v.Length; i++)
                _v[i] *= 10;
        }
        
        void Fun(int _v)
        {
            _v *= 10;
        }
        void Fun(float _v)
        {
            _v *= 10;
        }
        void Fun(Vector3 _v)
        {
            _v *= 10;
        }

        void Update()
        {
            for(int i = 0; i < listTrans.Count; i++)
            {
                listTrans[i].Translate(Vector3.forward * 2f * Time.deltaTime);
            }

            
        }
    }
}
