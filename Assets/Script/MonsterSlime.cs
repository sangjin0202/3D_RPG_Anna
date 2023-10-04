using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSlime : Monster
{
	void Start()
	{
		AddState(eMonsterState.Idle,	In_Idle,	Modify_Idle,	null);
		AddState(eMonsterState.Move,	In_Move,	Modify_Move,	null);
		AddState(eMonsterState.Chase,	In_Chase,	Modify_Chase,	null);
		AddState(eMonsterState.Attack,	In_Attack,	Modify_Attack,	null);
		AddState(eMonsterState.Die,		In_Die,		null,			null);

		bInit = true;
		MoveState(eMonsterState.Idle);
	}


	public override void InitData(SpawnArea _spawnArea, Player _player)
	{
		spawnArea = _spawnArea;

		//Debug.Log("@@@@ lv -> HP, ATT, DEF 루틴....");
		lv = _spawnArea.level;
		att = 10 + lv * 2;
		hp = 15 + lv * 5f;
		exp = 30 + lv * 10;
		listSpawnItem = _spawnArea.listSpawnItem;

		gameObject.SetActive(true);
		seeAngleHalf = seeAngle * 0.5f;
		releaseReturnSpawnPoint2 = releaseReturnSpawnPoint * releaseReturnSpawnPoint;
		attackRadius2 = attackRadius * attackRadius;
		trans = transform;
		wayPoint = GetComponent<WayPoint>();
		if (animator == null) animator = GetComponentInChildren<Animator>();

		if (bInit)
		{
			MoveState(eMonsterState.Idle);
		}
	}

	public override void SetDamage(Player _player, float _damage, Vector3 _dir, Vector3 _charHeight)
	{
		player = _player;
		damaged += _damage;
		hitPoint = trans.position + _dir * charThick + _charHeight;

		target = _player;
		attackedTime = Time.time + ATTACKED_TIME;
	}

	protected override void CheckDamage()
	{
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
				int _rand = Random.Range(0, listSpawnItem.Count);
				int _itemcode = listSpawnItem[_rand];
				int _count = 1;
				//ItemInfoBase _itemInfoBase = ItemInfoManager.ins.GetItemInfoBase(_itemcode);

				Vector3 _dir = (trans.position - player.transform.position).normalized;
				Vector3 _pos = trans.position + _dir * dropDistance;

				//Item _item = Instantiate(prefabItem, _pos, Quaternion.identity) as Item;
				Item _item = PoolManager.ins.Instantiate("Item", _pos, Quaternion.identity).GetComponent<Item>();
				_item.InitItemInfoDrop(_itemcode, _count);

				//몬스터 죽는 동작...
				animator.SetTrigger("Die");
				MoveState(eMonsterState.Die);
			}
			else
			{
				MoveState(eMonsterState.Chase);
			}
		}

	}

	#region Idle
	void In_Idle()
	{
		if (Constant.DEBUG_GM) Debug.Log(this + " In_Idle");

		//animator.... idle
		animator.SetFloat("Move", 0f);
		if (Constant.DEBUG_GM) Debug.Log("@@@@ animator idle");
		idleWaitTime = Time.time + IDLE_WAIT_TIME + Random.Range(0f, 3f);

		//처음들어올떄 타켓을 클리어 ...
		//다른 상태에서 전이 해서 올때도 클리어...
		target = null;
	}

	bool IsTarget()
	{
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

	void Modify_Idle()
	{
		if (Constant.DEBUG_GM) Debug.Log(this + " Modify_Idle");
		if (IsTarget())
		{
			MoveState(eMonsterState.Chase);
			return;
		}
		else if (Time.time > idleWaitTime)
		{
			MoveState(eMonsterState.Move);
			return;
		}

		//가만히 서있는 상태에서....서치...
		//자신의 능력범위 내에서 서치....
		Search();

		CheckDamage();
	}

	void Search()
	{
		//Debug.Log(" search");
		if (Time.time < searchTime) return;
		searchTime = Time.time + SEARCH_TIME;

		//1. 주위 검색...
		//거리범위 내에 Player를 검색를 한다...
		Collider[] _cols = Physics.OverlapSphere(trans.position, recognizeRadius, targetMask);
		//Debug.Log(_cols.Length);
		Transform _viewTrans;
		Vector3 _viewDir, _viewDirN;
		float _viewDistance;
		float _viewAngle;
		Ray _ray;
		RaycastHit _hit;
		for (int i = 0, imax = _cols.Length; i < imax; i++)
		{
			_viewTrans = _cols[i].transform;

			_viewDir = _viewTrans.position - trans.position;
			_viewDirN = _viewDir.normalized;
			_viewDistance = _viewDir.magnitude;
			_viewAngle = Vector3.Angle(trans.forward, _viewDirN);

			//2. 시야범내...
			if (_viewAngle < seeAngleHalf)
			{
				//Debug.Log("2. 시야내");
				//3. 장애물이 있는가?
				_ray = new Ray(trans.position + Vector3.up * BASE_OFFSET_RAY, _viewDirN);
				if (Physics.Raycast(_ray, out _hit, _viewDistance))
				{
					//Debug.Log("3. Find User");
					//Debug.DrawLine(_ray.origin, _hit.point, Color.red);
					//4. 다른 유저가 있는가?
					if (_viewTrans == _hit.collider.transform)
					{
						//Debug.Log("4. Find User");
						target = _hit.collider.GetComponent<Player>();
						break;
					}
				}
			}
		}
	}
	#endregion

	#region Move
	void In_Move()
	{
		if (Constant.DEBUG_MONSTER) Debug.Log(this + " In_Move");

		//걷는 애니메이션...
		animator.SetFloat("Move", 1f);
		//다음 포인트 지점계산...
		wayPoint.CalculateNextPoint();
	}

	void Modify_Move()
	{
		if (Constant.DEBUG_MONSTER) Debug.Log(this + " Modify_Move");
		//분기문...
		if (IsTarget())
		{
			MoveState(eMonsterState.Chase);
			return;
		}
		else if (wayPoint.CheckNearDestination(stopDistance))
		{
			MoveState(eMonsterState.Idle);
			return;
		}

		//이동하기...
		Vector3 _dirView = wayPoint.destinationPoint - trans.position;
		_dirView.y = 0;
		Quaternion _newRotation = Quaternion.LookRotation(_dirView);
		trans.rotation = Quaternion.Lerp(trans.rotation, _newRotation, ROTATION_INTERVAL_SPEED * Time.deltaTime);
		trans.position = Vector3.MoveTowards(trans.position, wayPoint.destinationPoint, moveSpeed * Time.deltaTime);

		//서치
		Search();

		CheckDamage();
	}
	#endregion

	#region Chase
	void In_Chase()
	{
		if (Constant.DEBUG_MONSTER) Debug.Log(this + " In_Chase");
	}

	bool CheckSpawnPointDistance()
	{
		if (Time.time < attackedTime) return false;
		if (Time.time < searchTime) return false;
		searchTime = Time.time + SEARCH_TIME;

		bool _rtn = false;
		Vector3 _dirView = wayPoint.destinationPoint - trans.position;
		if (_dirView.sqrMagnitude > releaseReturnSpawnPoint2)
		{
			_rtn = true;
		}

		return _rtn;
	}

	void Modify_Chase()
	{
		if (Constant.DEBUG_MONSTER) Debug.Log(this + " Modify_Chase");

		//Debug.Log("A:" + !IsTarget());
		//Debug.Log(" B:" + target);
		//Debug.Log(" B:" + !CheckRadius(target.transform.position, releaseRadius));
		//Debug.Log(" C:" + CheckSpawnPointDistance());

		//타켓이 너무 멀리갔거나...
		//몬스터의 스폰위치에서 너무 멀리 갔으면....
		if (target == null
			|| !IsTarget()
			|| !CheckRadius(target.transform.position, releaseRadius)
			|| CheckSpawnPointDistance())
		{
			if (Constant.DEBUG_MONSTER) Debug.Log(" >> Idle");
			//타켓을 읽어버리면...
			MoveState(eMonsterState.Idle);
			return;
		}
		else if (CheckRadius(target.transform.position, attackRadius))
		{
			//공격범위 내에 타켓이 있는가? 거리를 항상체킹....
			if (Constant.DEBUG_MONSTER) Debug.Log(" >> Attack");
			MoveState(eMonsterState.Attack);
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
	}
	#endregion

	#region Attack
	void In_Attack()
	{
		if (Constant.DEBUG_MONSTER) Debug.Log(this + " In_Attack");
		//attackTime = Time.time + ATTACK_TIME;
	}

	void Animotor_Attack()
	{
		if (Constant.DEBUG_MONSTER_ATTACK) Debug.Log("Player -> Attack -> HP--");


		target.SetDamage(damage, attackedDirView, attackedHitHeight);
	}
	bool CheckAttackRadius()
	{
		//if (Time.time < searchTime) return false;
		//searchTime = Time.time + SEARCH_TIME;

		bool _rtn = false;
		Vector3 _dirView = target.transform.position - trans.position;
		if (_dirView.sqrMagnitude > attackRadius2)
		{
			_rtn = true;
		}

		return _rtn;
	}

	void Modify_Attack()
	{
		if (Constant.DEBUG_MONSTER) Debug.Log(this + " Modify_Attack");
		//Debug.Log(!IsTarget() + ":" + CheckAttackRadius());            
		if (!IsTarget() || CheckAttackRadius())
		{
			MoveState(eMonsterState.Chase);
			return;
		}

		//현재 공격거리내 && 시간내
		if (Time.time > attackTime)
		{
			Vector3 _targetPosition = target.transform.position;
			Vector3 _dirView = _targetPosition - trans.position;
			_dirView.y = 0;
			trans.rotation = Quaternion.LookRotation(_dirView);
			attackedDirView = (-_dirView).normalized;

			attackTime = Time.time + ATTACK_TIME;
			animator.SetTrigger("Attack");
			//Debug.Log("Attack");
		}


		CheckDamage();
	}
	#endregion

	#region Die
	void In_Die()
	{
		if (Constant.DEBUG_GM) Debug.Log(this + "In_Die");
	}

	#endregion



	//-----------------------------------------
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
		Gizmos2.DrawCircle(transform.position, Color.blue, recognizeRadius);
		Gizmos2.DrawCircle(transform.position, Color.black, releaseRadius);
		Gizmos2.DrawCircle(transform.position, Color.grey, releaseWayPointRadius);

		//실행중에만... Gizmos표현하기...
		if (Application.isPlaying && wayPoint != null)
		{
			Gizmos2.DrawCircle(wayPoint.destinationPoint, Color.grey, releaseReturnSpawnPoint);
		}
	}
#endif


}
