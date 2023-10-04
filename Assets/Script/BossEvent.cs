using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BossEvent : MonoBehaviour
{
    public Boss boss;


    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

	private void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			Player _player = _other.GetComponent<Player>();
			if(boss != null && _player != null)
			{
				boss.SetTarget(_player);
			}
		}
	}
}
