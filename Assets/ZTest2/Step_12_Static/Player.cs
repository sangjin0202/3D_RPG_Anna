using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step12
{

    public class Player : MonoBehaviour
    {
		#region sigletone
		public static Player ins;
		private void Awake()
		{
            ins = this;
		}
		#endregion
		//public static List<Monster> list = new List<Monster>();
		public Monster prefab;
        void Start()
        {
            Debug.Log(ins);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
			{
                Debug.Log(SUtil.Add(1, 2));
                //SUtil s = new SUtil();
                
			}
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log(SUtil2.Add(1, 2));
                SUtil2 s = new SUtil2();
                //s.ADD(1, 2);
                s.x = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SUtil2 s = new SUtil2();
                //s.ADD(1, 2);
                s.x = 1;
                s.y = 2;
                Debug.Log("s:"+SUtil2.Add(s.x, s.y));

                SUtil2 s2 = new SUtil2();
                s2.x = 10;
                s2.y = 20;
                Debug.Log("s2:" +SUtil2.Add(s2));
                //Input
                //Random
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Instantiate(prefab, transform.position + Random.onUnitSphere * 5, Quaternion.identity);
                Debug.Log(Monster.count);
                //Debug.Log(list.Count);

            }

        }
    }
}
