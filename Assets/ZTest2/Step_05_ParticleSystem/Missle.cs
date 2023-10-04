using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step05
{
    public class Missle : MonoBehaviour
    {
        public ParticleSystem particle;
        public float speed = 2f;
        void Start()
        {
            particle.Stop();
            particle.Play();
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    }
}