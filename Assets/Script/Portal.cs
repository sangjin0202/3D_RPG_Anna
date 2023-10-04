using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Portal : MonoBehaviour
{
    public Portal otherPotal;
	public Transform point;
	public bool bLock;
	public Transform viewPoint;

	private void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			Debug.Log(_other);
			Player _player = _other.GetComponent<Player>();
			if (_player != null)
			{
				_player.SetPortal(otherPotal.point);
				if (otherPotal.bLock)
				{
					otherPotal.gameObject.SetActive(false);
				}
			}
			if (otherPotal.viewPoint != null)
			{
				StopCoroutine("Co_LookAt");
				StartCoroutine("Co_LookAt");
			}
		}
	}

	IEnumerator Co_LookAt()
	{
		yield return null;
		Camera.main.transform.LookAt(otherPotal.viewPoint);
	}

}
