using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Monster
{
	/*
	#region control value
	public enum eBossState { None, Idle, Chase, Attack1, Attack2, AttackIdle, Die }
	public float attackRadius = 2f;
	public float moveSpeed = 1f;
	public float chaseSpeed = 2f;
	public float stopDistance = 0.2f;
	public float damage = 10f;

	float attackIdleWaitTime;
	public float ATTACK_IDLE_WAIT_TIME = 2f;

	Vector3 hitPoint;
	public ParticleSystem effectHit;
	Transform trans;
	public float dropDistance = 0.5f;
	public Animator animator;

	public float ROTATION_INTERVAL_SPEED = 2f;

	public Player target;
	bool bInit;
	#endregion
	*/
#region Monster ATT, HP, DEF, LV etc
	/*
	public float lv;
	float att;
	float hp;
	float exp;
	float damaged;
	*/
	//public float ATTACK_TIME = 3f;  //한번 공격하고 다음 공격을 위한 간격...
	//float attackTime;
	bool bAttack = false;
#endregion


	#region Item
	public int itemcode = 10001;
	#endregion

	void Start()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " Start");

		AddState(eMonsterState.Idle,		In_Idle,		Modify_Idle,		null);
		AddState(eMonsterState.Chase,		In_Chase,		Modify_Chase,		null);
		AddState(eMonsterState.Attack,		In_Attack,		Modify_Attack,		null);
		AddState(eMonsterState.Attack2,		In_Attack2,		Modify_Attack2,		null);
		AddState(eMonsterState.AttackIdle,	In_AttackIdle,	Modify_AttackIdle,	null);
		AddState(eMonsterState.Die,			In_Die,			null,				null);

		bInit = true;
		InitData(null, null);
		MoveState(eMonsterState.Idle);
	}
	public override void InitData(SpawnArea spawnArea, Player _target)
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " InitData");

		//Debug.Log("@@@@ lv -> HP, ATT, DEF 루틴....");
		att = 10 + lv * 2;
		hp	= 15 + lv * 5;
		exp = 30 + lv * 10;

		if (Constant.DEBUG_BOSS) Debug.Log(this + " @@@@@@@@@@@@");
		hp = 40;
		if (Constant.DEBUG_BOSS) Debug.Log(this + " @@@@@@@@@@@@");

		gameObject.SetActive(true);
		seeAngleHalf = seeAngle * 0.5f;
		attackRadius2 = attackRadius * attackRadius;

		target = null;
		trans = transform;
		if (animator == null) animator = GetComponentInChildren<Animator>();

		if (bInit)
		{
			MoveState(eMonsterState.Idle);
		}
	}

	public void SetTarget(Player _target)
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " SetTarget");
		target = _target;
	}

	public override void SetDamage(Player _player, float _damage, Vector3 _dir, Vector3 _charHeight)
	{
		if (Constant.DEBUG_BOSS) Debug.Log("Player -> Boss Damage");
		player = _player;
		damaged += _damage;
		hitPoint = trans.position + _dir * charThick + _charHeight;

		target = _player;
	}

	protected override void CheckDamage()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + "CheckDamage" + damaged);
		float _damage = damaged;
		damaged = 0f;
		if (_damage > 0)
		{
			//Debug.Log(this + " >>" + hp + ":" + _damage);

			//파티클... 
			ParticleSystem _ps = Instantiate(effectHit, hitPoint, Quaternion.identity) as ParticleSystem;
			_ps.Stop();
			_ps.Play();
			//hp감소...
			hp -= _damage;

			//health <= 0 die
			if (hp <= 0)
			{
				//몬스터 죽었어요...   -> Player 검험치.
				//                  -> 아이템은 필드에 떨구고..
				GetComponent<Collider>().enabled = false;
				player.AddExp(this, exp);
				//Debug.Log("@@@@ 아이템 필드에 생성");
				int _itemcode = itemcode;
				int _count = 1;
				//ItemInfoBase _itemInfoBase = ItemInfoManager.ins.GetItemInfoBase(_itemcode);

				Vector3 _dir = (trans.position - player.transform.position).normalized;
				Vector3 _pos = trans.position + _dir * dropDistance;

				//Item _item = Instantiate(prefabItem, _pos, Quaternion.identity) as Item;
				Item _item = PoolManager.ins.Instantiate("Item", _pos, Quaternion.identity).GetComponent<Item>();
				_item.InitItemInfoDrop(_itemcode, _count);

				//몬스터 죽는 동작...
				animator.SetTrigger("die");
				MoveState(eMonsterState.Die);
			}
			else
			{
				MoveState(eMonsterState.Chase);
			}
		}

	}

	#region Idle

	bool IsTarget()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " IsTarget");
		if (target == null)
		{
			return false;
		}
		else if (target.gameObject.activeSelf == false)
		{
			return false;
		}
		else if (target.gameObject.activeInHierarchy == false)
		{
			return false;
		}
		return true;
	}

	void In_Idle()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " In_Idle");

		//animator.... idle
		animator.SetFloat("move", 0f);

		//처음들어올떄 타켓을 클리어 ...
		//다른 상태에서 전이 해서 올때도 클리어...
		//target = null;
	}


	void Modify_Idle()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " Modify_Idle target:" + target );
		if (target != null)
		{
			MoveState(eMonsterState.Chase);
			return;
		}

	}

	#endregion

	#region Chase

	void In_Chase()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " In_Chase");
		animator.SetFloat("move", 1f);
	}


	bool CheckRadius(Vector3 _point, float _radius)
	{
		//if (Constant.DEBUG_BOSS) Debug.Log(this + " CheckRadius_point:" + _point + " _radius:" + _radius);
		//if (Time.time < attackedTime)return true;

		Vector3 _dir = _point - trans.position;
		float _radius2 = _radius * _radius;
		float _sqrMagnitude = _dir.sqrMagnitude;
		return _sqrMagnitude < _radius2;

	}


	void Modify_Chase()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " Modify_Chase");

		//타켓이 너무 멀리갔거나...
		//몬스터의 스폰위치에서 너무 멀리 갔으면....
		if (target == null || !IsTarget())
		{
			if (Constant.DEBUG_BOSS) Debug.Log(" >> Idle");
			//타켓을 읽어버리면...
			MoveState(eMonsterState.Idle);
			return;
		}
		else if (CheckRadius(target.transform.position, attackRadius))
		{
			//공격범위 내에 타켓이 있는가? 거리를 항상체킹....
			if (Constant.DEBUG_BOSS) Debug.Log(" >> Attack");
			//percent -> 70/30
			float _percent = Random.Range(0f, 100f);
			if (_percent < 70f) //70
			{
				MoveState(eMonsterState.Attack);
			}
			else
			{
				MoveState(eMonsterState.Attack2);
			}
			return;
		}

		//타겟까지 접급하기..
		Vector3 _targetPosition = target.transform.position;
		Vector3 _dirView = _targetPosition - trans.position;
		_dirView.y = 0;
		Quaternion _newRotation = Quaternion.LookRotation(_dirView);
		trans.rotation = Quaternion.Lerp(trans.rotation, _newRotation, ROTATION_INTERVAL_SPEED * Time.deltaTime);
		trans.position = Vector3.MoveTowards(trans.position, _targetPosition, chaseSpeed * Time.deltaTime);

		CheckDamage();
		/**/
	}
	#endregion

	#region Animator 후 보스에 영향을 주는 함수
	//------------------------------------
	public void Animator_TakeDamage(int _index)
	{
		Debug.Log(this + " Animator_TakeDamage " + _index);
		//attack01 -> 1, 2, 3	-> 99
		//attack02 -> 4			-> 99

		//1. 어택 방향...
		Vector3 _targetPosition = target.transform.position;
		Vector3 _dirView = _targetPosition - trans.position;
		_dirView.y = 0;
		attackedDirView = (-_dirView).normalized;
		float _viewAngle = Vector3.Angle(trans.forward, _dirView.normalized);

		//2. 거리...
		if (_index != 99)
		{
			if (_dirView.sqrMagnitude > attackRadius2)
			{
				if (Constant.DEBUG_BOSS) Debug.Log(" attack pass 거리");
				_index = -1;
			}

			//3. 시야각...
			if (_viewAngle > seeAngleHalf)
			{
				if (Constant.DEBUG_BOSS) Debug.Log(" attack pass 시야");
				_index = -1;
			}
		}

	
		
	

		switch (_index)
		{
			case -1:
				break;
			case 1:
				//Boss -> Player TakeDamage
				target.SetDamage(damage, attackedDirView, attackedHitHeight);
				break;
			case 2:
				target.SetDamage(damage, attackedDirView, attackedHitHeight);
				break;
			case 3:
				target.SetDamage(damage, attackedDirView, attackedHitHeight);
				break;
			case 4:
				target.SetDamage(damage, attackedDirView, attackedHitHeight);
				break;
			case 99:
				//완료... -> 상태전이...AttackIdle
				bAttack = true;
				break;
		}
	}
	#endregion

	#region Attack
	void In_Attack()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " In_Attack");
		//attackTime = Time.time + ATTACK_TIME;
		animator.SetTrigger("attack01");
		bAttack = false;
	}

	void Modify_Attack()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " Modify_Attack");
		if (bAttack)
		{
			MoveState(eMonsterState.AttackIdle);
			return;
		}

		CheckDamage();

	}
	#endregion

	#region Attack2
	void In_Attack2()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " In_Attack2");
		animator.SetTrigger("attack02");
		bAttack = false;
		
	}

	void Modify_Attack2()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " Modify_Attack2");
		if (bAttack)
		{
			MoveState(eMonsterState.AttackIdle);
			return;
		}
		CheckDamage();
	}
	#endregion

	#region AttackIdle
	void In_AttackIdle()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " In_AttackIdle");
		animator.SetFloat("move", 0);
		attackTime = Time.time + ATTACK_TIME;

	}

	void Modify_AttackIdle()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " Modify_AttackIdle");
		if (attackTime < Time.time )
		{
			MoveState(eMonsterState.Chase);
			return;
		}

		Vector3 _targetPosition = target.transform.position;
		Vector3 _dirView = _targetPosition - trans.position;
		_dirView.y = 0;
		Quaternion _newRotation = Quaternion.LookRotation(_dirView);
		trans.rotation = Quaternion.Lerp(trans.rotation, _newRotation, ROTATION_INTERVAL_SPEED * Time.deltaTime);
		//trans.position = Vector3.MoveTowards(trans.position, _targetPosition, chaseSpeed * Time.deltaTime);

		CheckDamage();


	}
	#endregion

	#region Die
	void In_Die()
	{
		if (Constant.DEBUG_BOSS) Debug.Log(this + " In_Die");
	}

	//void Modify_Die()
	//{
	//	if (Constant.DEBUG_BOSS) Debug.Log(this + " Modify_Die");
	//}
	#endregion

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		//see
		Gizmos.color = Color.white;
		float _angle = transform.eulerAngles.y;
		float _angleL = _angle - seeAngle * 0.5f;
		float _angleR = _angle + seeAngle * 0.5f;

		Vector3 _dirL = Quaternion.Euler(Vector3.up * _angleL) * Vector3.forward * recognizeRadius;
		Vector3 _dirR = Quaternion.Euler(Vector3.up * _angleR) * Vector3.forward * recognizeRadius;
		Gizmos.DrawRay(transform.position, _dirL);
		Gizmos.DrawRay(transform.position, _dirR);

		//radius
		Gizmos2.DrawCircle(transform.position, Color.red, attackRadius);
	
	}
#endif

}