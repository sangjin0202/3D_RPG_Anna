using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step08
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 5f;

        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}