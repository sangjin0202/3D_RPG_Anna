using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step06
{
    public class TempTest : MonoBehaviour
    {
        void Start()
        {
            Debug.Log(Add(1, 2));
            Debug.Log(Add(1.1f, 2.2f));
            Debug.Log(Add(new Vector3(1,2,3), new Vector3(10,20,30)));
            //Debug.Log(Add(new CubeMove(), new CubeMove()));

            MeshRenderer _mr    = GetComponent<MeshRenderer>();
            Collider _c         = GetComponent<Collider>();
            Debug.Log(Add<int>(1, 2));
            Debug.Log(Add<float>(1, 2));
            Debug.Log(Add<Vector3>(new Vector3(1, 2, 3), new Vector3(10, 20, 30)));
        }

        T Add<T>(T _x, T _y)
        {
            return _x;
        }
        int Add(int _x, int _y)
        {
            return _x + _y;
        }

        float Add(float _x, float _y)
        {
            return _x + _y;
        }
        
        Vector3 Add(Vector3 _x, Vector3 _y)
        {
            return _x + _y;
        }

        CubeMove Add(CubeMove _x, CubeMove _y)
        {
            return _x;
        }

    }
}