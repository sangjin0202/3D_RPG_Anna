using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace step05
{
    public class SpawnTest2 : MonoBehaviour
    {
        public List<ParticleSystem> list = new List<ParticleSystem>();
        int index = 0;

        void Start()
        {

        }

        void Update()
        {
            float _h = Input.GetAxisRaw("Horizontal");
            if (_h != 0) transform.Translate(Vector3.right * _h * 2f * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {

                ParticleSystem _p = list[index];
                ParticleSystem _ps = Instantiate(_p, transform.position, Quaternion.identity) as ParticleSystem;
                _ps.gameObject.SetActive(true);
                _ps.Stop();
                _ps.Play();

                Destroy(_ps.gameObject, _ps.main.duration);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                index = index - 1;
                index = index < 0 ? list.Count - 1 : index;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                index = (index + 1) % list.Count;
            //    index = index + 1;
            //    index = index > list.Count - 1 ? 0 : index;
            }
        }
    }
}
