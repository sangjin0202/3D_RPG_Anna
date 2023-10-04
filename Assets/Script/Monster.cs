using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Step06;

public abstract class Monster : FSM<Monster.eMonsterState>
{
	public enum eMonsterState { None, Idle, Move, Chase, Attack, Attack2, AttackIdle, Die }
	// 일반몹A					  0		0	 0		0	   0		 		  0		   0
	// 일반몹B					  0		0	 0		0	   0		0		  0		   0
	// 보스몹					  0		0	 		0	   0		0		  0		   0

	#region control value
	public float attackRadius = 2f;
	protected float attackRadius2;
	public float recognizeRadius = 5f;
	public float releaseRadius = 10f;
	public float releaseWayPointRadius = 20f;
	public float releaseReturnSpawnPoint = 50f;
	protected float releaseReturnSpawnPoint2;
	public float seeAngle = 60f;
	protected float seeAngleHalf;

	public float moveSpeed = 1f;
	public float chaseSpeed = 2f;
	public float stopDistance = 0.2f;
	public float damage = 10f;

	protected float idleWaitTime;
	public float IDLE_WAIT_TIME = 2f;

	protected Vector3 hitPoint;
	public ParticleSystem effectHit;
	protected Transform trans;
	public float charThick = 0.5f;
	public float dropDistance = 0.5f;
	public Animator animator;
	protected WayPoint wayPoint;
	public Player target, player;
	public LayerMask targetMask;
	protected float searchTime;
	public float SEARCH_TIME = 0.5f;
	public float BASE_OFFSET_RAY = 0.5f;
	public float ROTATION_INTERVAL_SPEED = 2f;
	public float ATTACKED_TIME = 5f; //유저가 몬스터를 공격하고 난다음에 시간...
	protected float attackedTime;

	protected Vector3 attackedDirView;
	public Vector3 attackedHitHeight = new Vector3(0, 0.5f, 0);
	protected bool bInit;
	#endregion

	#region Monster ATT, HP, DEF, LV etc
	protected float lv;
	protected float att;
	protected float hp;
	protected float exp;
	protected float damaged;
	public float ATTACK_TIME = 2f;  //한번 공격하고 다음 공격을 위한 간격...
	protected float attackTime;
	#endregion

	#region Item
	public SpawnArea spawnArea;
	public List<int> listSpawnItem;
	#endregion


	//abstract or virtual
	public abstract void InitData(SpawnArea _spawnArea, Player _player);

	public abstract void SetDamage(Player _player, float _damage, Vector3 _dir, Vector3 _charHeight);

	protected abstract void CheckDamage();

	public void Animator_Die()
	{
		StopCoroutine("Co_SinkDown");
		StartCoroutine("Co_SinkDown", 2f);
	}

	IEnumerator Co_SinkDown(float _duration)
	{
		float _speed = 1f / _duration;
		Vector3 _p0 = trans.position;
		Vector3 _p1 = trans.position + Vector3.down * 2f;
		float _percent = 0;
		while (_percent < 1f)
		{
			_percent += _speed * Time.deltaTime;
			trans.position = Vector3.Lerp(_p0, _p1, _percent);
			yield return null;
		}

		//몬스터 죽음..
		//Destroy(gameObject);
		MonsterManager.ins.RemoveMonster(this);
		GetComponent<Collider>().enabled = true;
		gameObject.SetActive(false);
	}

	//해당위치 Point, 공격거리/인식거리/지근거리
	protected bool CheckRadius(Vector3 _point, float _radius)
	{
		//if (Time.time < attackedTime)return true;

		bool _rtn = false;
		Vector3 _dir = _point - trans.position;
		float _radius2 = _radius * _radius;
		float _sqrMagnitude = _dir.sqrMagnitude;
		if (_sqrMagnitude < _radius2)
		{
			_rtn = true;
		}
		return _rtn;
	}

}