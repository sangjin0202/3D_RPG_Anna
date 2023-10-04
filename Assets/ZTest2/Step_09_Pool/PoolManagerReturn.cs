using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Step09
{
    public class PoolManagerReturn : MonoBehaviour
    {
        private void Awake()
        {
            ParticleSystem _particle = GetComponent<ParticleSystem>();
            if(_particle !=null)
            {
                returnTime = _particle.main.duration;
            }
        }
        public float returnTime = 5f;
        private void OnEnable()
        {
            Invoke("Destroy", returnTime);
        }
        void Destroy()
        {
            gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            CancelInvoke();
        }

    }
}