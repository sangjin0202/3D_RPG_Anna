using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Step04
{
    public class SceneInfo : MonoBehaviour
    {
        public string strNextScene;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(strNextScene);
            }
        }
    }
}