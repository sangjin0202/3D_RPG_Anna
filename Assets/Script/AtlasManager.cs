using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AtlasManager : MonoBehaviour
{
	#region
	public static AtlasManager ins;
	private void Awake()
	{
		ins = this;
	}
	#endregion

	public SpriteAtlas atlas;

	public Sprite GetSprite(string _name)
	{
		return atlas.GetSprite(_name);
	}

}
