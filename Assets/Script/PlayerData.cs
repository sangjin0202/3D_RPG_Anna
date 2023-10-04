using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
	public void SetInit(CharClass _charClass)
	{
		//Debug.Log("@@@ 초기값 세팅...");
		charClass = _charClass;

		exp = 0;
		hp_ = charClass.basehp;
	}

	public CharClass charClass;

	public float exp;   //s
	public int lv {
		get {
			//경험치 -> lv 공식있어 (기획자줌)
			return (int)exp / 100 + 1;
		}
	}

	public float attackDamage
	{
		get
		{
			//능력치 = base + 레벨능력치 + 장비 + 버프(장비 + 능력치)
			//Debug.Log("@@@@ att 능력계산추가행함");
			float _base = charClass.baseatt + weaponAtt;
			return _base + _base * buffPercent;
		}
	}

	public float hp_;	//save
	public float hp {
		get { return hp_; }
		set {
			//물약		: value 
			//데미지	: value
			//value : 원래HP + 추가HP
			//value : 원래HP - 데미지HP
			hp_ = value;
			if (hp_ > hpMax)
				hp_ = hpMax;
			else if(hp_ < 0)
				hp_ = 0;
		}
	}
	public float hpMax
	{
		//능력치 = base + 레벨능력치 + 장비 + 버프(장비 + 능력치)
		get {
			Debug.Log("@@@@ hp 능력계산추가행함");
			return charClass.basehp;
		}
	}

	public float def
	{
		get
		{
			//능력치 = base + 레벨능력치 + 장비 + 버프(장비 + 능력치)
			Debug.Log("@@@@ def 능력계산추가행함");
			return (charClass.basedef)
				+ (charClass.basedef) * buffPercent;
		}
	}

	//public float debugAttackSpeed = 0f;
	public float attackSpeed {
		//장비중에 속도 향상...
		//장비 100% -> 100/100;
		//장비 150% -> 150/100
		get {
			Debug.Log("@@@ 장비버프 적용..." );
			return (charClass.baseatttime) * 0.01f;
		}
	}


	public float attackRadius { get { return charClass.baseattradius;} }
	public float recognizeRadius { get { return charClass.baserecognizeradius; } }
	public float weaponAtt;
	public float buffPercent;

	public float skill1Damage {
		get {
			return (charClass.baseatt + weaponAtt) 
				+ (charClass.baseatt + weaponAtt) * buffPercent;
		}
	}
	public float skill2Damage { get { return skill1Damage; } }
	public float skill3Damage { get { return skill1Damage; } }

}
