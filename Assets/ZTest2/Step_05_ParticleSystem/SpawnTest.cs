using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step05
{
    public class SpawnTest : MonoBehaviour
    {
        public ParticleSystem p1, p2, p3;
        public Missle missle;

        void Update()
        {
            float _h = Input.GetAxisRaw("Horizontal");
            if (_h != 0) transform.Translate(Vector3.right * _h * 2f * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ParticleSystem _ps = Instantiate(p1, transform.position, Quaternion.identity) as ParticleSystem;
                _ps.gameObject.SetActive(true);
                _ps.Stop();
                _ps.Play();

                Destroy(_ps.gameObject, 10f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ParticleSystem _ps = Instantiate(p1, transform.position, Quaternion.identity) as ParticleSystem;
                _ps.gameObject.SetActive(true);
                _ps.Stop();
                _ps.Play();

                Destroy(_ps.gameObject, _ps.main.duration);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ParticleSystem _ps = Instantiate(p1, transform.position, Quaternion.identity) as ParticleSystem;
                _ps.gameObject.SetActive(true);
                _ps.Stop();
                _ps.Play();

                Destroy(_ps.gameObject, _ps.main.startLifetime.constantMax);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Missle _missle = Instantiate(missle, transform.position, Quaternion.identity) as Missle;
                _missle.gameObject.SetActive(true);
                
                Destroy(_missle.gameObject, 30f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                ParticleSystem _ps = Instantiate(p2, transform.position, Quaternion.identity) as ParticleSystem;
                _ps.gameObject.SetActive(true);
                _ps.Stop();
                _ps.Play();

                Destroy(_ps.gameObject, _ps.main.duration);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                ParticleSystem _ps = Instantiate(p3, transform.position, Quaternion.identity) as ParticleSystem;
                _ps.gameObject.SetActive(true);
                _ps.Stop();
                _ps.Play();

                Destroy(_ps.gameObject, _ps.main.duration);
            }

        }
    }
}