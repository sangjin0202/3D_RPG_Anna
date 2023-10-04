using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step08
{
    public class TestPool : MonoBehaviour
    {
        public GameObject cube;
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                PoolManager.ins.Instantiate("Cube", transform.position, transform.rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PoolManager.ins.Instantiate("Sphere", transform.position, transform.rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PoolManager.ins.Instantiate("Capsule", transform.position, transform.rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PoolManager.ins.Instantiate("Classic_Small_Explosion", transform.position, transform.rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                Instantiate(cube, transform.position, transform.rotation);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ParticleSystem _ps = PoolManager.ins.Instantiate("Effect2", transform.position, transform.rotation).GetComponent<ParticleSystem>();
                _ps.Stop();
                _ps.Play();
            }
        }
    }
}