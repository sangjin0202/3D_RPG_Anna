using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step12
{

    public class Monster : MonoBehaviour
    {
        public static int count;

        void Start()
        {
            count++;
            //Player.list.Add(this);
        }
    }
}
