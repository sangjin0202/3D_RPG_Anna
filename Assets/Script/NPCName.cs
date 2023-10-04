using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCName : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();
    public List<Text> list2 = new List<Text>();
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 tmp = list[i].transform.position;
            tmp.y += 2.5f;

            list2[i].transform.position = Camera.main.WorldToScreenPoint(tmp);
        }
    }
}
