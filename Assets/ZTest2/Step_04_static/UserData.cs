using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Step04
{
    public class UserData : MonoBehaviour
    {
        #region sigleton
        public static UserData ins;
        private void Awake()
        {
            if (ins == null)
            {
                ins = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (ins != null && ins != this)
            {
                DestroyImmediate(this);
            }
        }
        #endregion
        public int health = 10;

    }
}