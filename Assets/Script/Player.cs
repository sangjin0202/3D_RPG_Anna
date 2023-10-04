using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player : MonoBehaviour
{
    public Text Name;
    public TrailRenderer trail;
    public string strTakeDamageEffect = "";
    Animator anim;
    public ParticleSystem effectHit;
    public float moveSpeed = 5f;
    Dictionary<int, int> dic_MoveStopAnimations = new Dictionary<int, int>()
    {
        { Animator.StringToHash("Skill1"), Animator.StringToHash("Skill1")},
        { Animator.StringToHash("Skill2"), Animator.StringToHash("Skill2")},
        { Animator.StringToHash("Skill3"), Animator.StringToHash("Skill3")},
        { Animator.StringToHash("Attack1"), Animator.StringToHash("Attack1")},
        { Animator.StringToHash("Attack2"), Animator.StringToHash("Attack2")},
        //Animator.StringToHash("Skill2"),
        //Animator.StringToHash("Skill3"),
        //Animator.StringToHash("Attack1"),
        //Animator.StringToHash("Attack2"),
    };

    Vector3 hitPoint;
    Transform trans;
	Vector3 move;
	Vector3 zero = Vector3.zero;
	public float charThick = 0.5f;
    public Vector3 attackedHitHeight = new Vector3(0, 0.5f, 0);
    float searchTime;
    float takeDamage;
    public float SEARCH_TIME = 0.5f;
    public LayerMask targetMask;
    public float BASE_OFFSET_RAY = 1.5f;

	public int classcode = 82000;
	public PlayerData playerData;
    public Monster targetFake;
    public Monster targetReal;
    public List<Monster> listTargetFakeMonster = new List<Monster>();

    IEnumerator Start()

    {
        trans = transform;
        anim = GetComponent<Animator>();
        trail.gameObject.SetActive(false);

		yield return null;
		//Debug.Log("@@@@ yield return null 편법 나중에 GameManager  -> Player 컨드롤 하는 방식으로 가야함...");
		//Debug.Log("@@@@ 강제로 PlayerData, CharClass가져옴..");
		playerData = new PlayerData();
		CharClass _charClass = CharInfoManager.ins.GetCharClass(classcode);
		playerData.SetInit(_charClass);
        Ui_Inventory.ins.SetPlayer(this);

        float _curExp = playerData.exp % 100f;
        Ui_HpBar.ins.DisplayExp(_curExp / 100f);

    }

    #region Attack
    public void Invoke_Attack()
    {

        // 검색 후 턴
        if(targetFake != null)
        {
   //         float _bodySize = 0;
   //         SphereCollider _sphereCollider = targetFake.GetComponent<SphereCollider>();
			//if (_sphereCollider != null)
			//{
   //             _bodySize = _sphereCollider.radius;
   //             Debug.Log("_bodySize :" + _bodySize);
			//}
            Vector3 _dir = targetFake.transform.position - transform.position;
            float _distance = _dir.magnitude; // - _bodySize;            //Debug.Log(this + " " + _distance );
            if (_distance < playerData.attackRadius)
            {
                //거리가 공격범위 내이면 그쪽 방향으로 공격...
                _dir.y = 0;
                trans.rotation = Quaternion.LookRotation(_dir);
                Debug.Log("turn");
                targetReal = targetFake;
            }
            else
            {
                //거리가 멀리 떨어짐...
                //Debug.Log("clear");
                targetFake = null;
                targetReal = null;
            }
		}
		
		anim.SetTrigger("Atk1");
		anim.SetFloat("Atk1Speed", playerData.attackSpeed);
    }
    public void Animator_Attack()
    {
        //Debug.Log(this + "Animator_Attack targetReal:" + targetReal);
        if(targetReal != null)
        {
            //Debug.Log("Attack1 OK");
            targetReal.SetDamage(this, playerData.attackDamage, (transform.position - targetReal.transform.position).normalized, attackedHitHeight);
        }
        else
        {
            //Debug.Log("Attack1 No");
        }
    }
    public void Animator_AttackStart()
    {
        trail.gameObject.SetActive(true);
        trail.Clear();
    }
    public void Animator_AttackEnd()
    {
        trail.gameObject.SetActive(false);
    }
	#endregion

	#region Skill
	public void Invoke_Skill1()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Skill"))
        {
            return;
        }
        // 캐릭터를 가장가까운방향으로 턴
        if (targetFake != null)
        {
            Vector3 _dir = targetFake.transform.position - transform.position;
            _dir.y = 0;
            trans.rotation = Quaternion.LookRotation(_dir);
        }
        
        anim.SetTrigger("Skill1");
    }
    public void Animator_Skill1()
    {
        ParticleSystem _ps
                = PoolManager.ins.Instantiate("LightningSphereBlast", trans.position, Quaternion.identity).GetComponent<ParticleSystem>();
        _ps.Stop();
        _ps.Play();
        Debug.Log("Animator_Skill1");
        Monster _target;
        for (int i = 0; i < listTargetFakeMonster.Count; i++)
        {
            _target = listTargetFakeMonster[i];
            if (listTargetFakeMonster[i] != null)
            {
                _target.SetDamage(this, playerData.skill1Damage, 
                    (transform.position - _target.transform.position).normalized,
                    attackedHitHeight);
            }
        }
        
    }
    public void Invoke_Skill2()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Skill"))
        {
            return;
        }
        // 캐릭터를 가장가까운방향으로 턴
        if (targetFake != null)
        {
            Vector3 _dir = targetFake.transform.position - transform.position;
            _dir.y = 0;
            trans.rotation = Quaternion.LookRotation(_dir);
        }
        anim.SetTrigger("Skill2");

    }
    public void Animator_Skill2()
    {
        Debug.Log("Animator_Skill2");
        Monster _target;
        for (int i = 0; i < listTargetFakeMonster.Count; i++)
        {
            _target = listTargetFakeMonster[i];
            if (listTargetFakeMonster[i] != null)
            {
                _target.SetDamage(this, playerData.skill1Damage,
                    (transform.position - _target.transform.position).normalized,
                    attackedHitHeight);
            }
        }
    }
    public void Invoke_Skill3()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Skill"))
        {
            return;
        }
        // 캐릭터를 가장가까운방향으로 턴
        if (targetFake != null)
        {
            Vector3 _dir = targetFake.transform.position - transform.position;
            _dir.y = 0;
            trans.rotation = Quaternion.LookRotation(_dir);
        }
        anim.SetTrigger("Skill3");

    }
    public void Animator_Skill3()
    {
        Debug.Log("Animator_Skill3");
        Monster _target;
        for (int i = 0; i < listTargetFakeMonster.Count; i++)
        {
            _target = listTargetFakeMonster[i];
            if (listTargetFakeMonster[i] != null)
            {
                _target.SetDamage(this, playerData.skill1Damage,
                    (transform.position - _target.transform.position).normalized,
                    attackedHitHeight);
            }
        }
    }
	#endregion

	void Search()
    {
        if (Time.time < searchTime)
        {
            return;
        }
        searchTime = Time.time + SEARCH_TIME;
        //1. 주위 검색...
        //거리범위 내에 Player를 검색을 한다... (Layer)
        Collider[] _cols = Physics.OverlapSphere(trans.position, playerData.recognizeRadius, targetMask);
        //Debug.Log(_cols.Length);
        Transform _viewTrans;
        Vector3 _viewDir, _viewDirN;
        float _viewDistance;
        float _viewAngle;
        Ray _ray;
        RaycastHit _hit;
        listTargetFakeMonster.Clear();
        float _distance = float.MaxValue;
        for (int i = 0, imax = _cols.Length; i < imax; i++)
        {
            _viewTrans = _cols[i].transform;
            _viewDir = _viewTrans.position - trans.position;
            _viewDirN = _viewDir.normalized;
            _viewDistance = _viewDir.magnitude;
            _viewAngle = Vector3.Angle(trans.forward, _viewDirN);
            //2. 시야범위내...
            //if (_viewAngle < seeAngleHalf)
            //{
                //_ray = new Ray(trans.position + Vector3.up * BASE_OFFSET_RAY, _viewDirN);
                //if (Physics.Raycast(_ray, out _hit, _viewDistance))
                {
                    Monster _monster = _cols[i].GetComponent<Monster>();
                    if (_monster != null && !listTargetFakeMonster.Contains(_monster))
                    {
                        listTargetFakeMonster.Add(_monster);
                        float _distance2 = (trans.position - _cols[i].transform.position).magnitude;
                        if (_distance2 < _distance)
                        {
                            _distance = _distance2;
                            targetFake = _monster;
                            //Debug.Log("retarget");
                        }
                    }
                }
            //}
        }
    }

	void Update()
	{
        //입력
        //연산
        //그리기
        float _v = Input.GetAxisRaw("Vertical");
        float _h = Input.GetAxisRaw("Horizontal");

        ePlayerState _state = CheckPlayerState();
        if(_state == ePlayerState.Attack)
		{
            _v = 0;
            _h = 0;
		}
        //bool _press = Input.GetMouseButton(1);
        move.Set(_h, 0, _v);
        if(_h != 0 || _v != 0)
		{
            anim.SetFloat("walk", 1);
		}
		else
		{
            anim.SetFloat("walk", 0);
		}
        
        if (move != zero )
		{
            move = move.normalized;
            trans.rotation = Quaternion.LookRotation(move);

            trans.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        Search();

        //데미지 계산
        CheckDamage();

    }

    //카메라
    private void LateUpdate()
	{
        Vector3 tmp = transform.position;
        tmp.y += 3.0f;
        Name.transform.position = Camera.main.WorldToScreenPoint(tmp);
    }

    ePlayerState CheckPlayerState()
	{
        ePlayerState _rtn = ePlayerState.None;
        AnimatorStateInfo _asi = anim.GetCurrentAnimatorStateInfo(0);
        _rtn = dic_MoveStopAnimations.ContainsKey(_asi.shortNameHash) 
            ? ePlayerState.Attack 
            : ePlayerState.None;
        //Debug.Log("_rtn:" + _rtn);
        return _rtn;
	}


    void CheckDamage()
	{
		float _takeDamage = takeDamage;
		takeDamage = 0f;
		if (_takeDamage > 0)
		{
			//파티클... 
			ParticleSystem _ps 
                = PoolManager.ins.Instantiate(strTakeDamageEffect, hitPoint, Quaternion.identity).GetComponent<ParticleSystem>();
			_ps.Stop();
			_ps.Play();
			//hp감소...
			playerData.hp -= _takeDamage;
            //hp = hp - _takeDamage;
            Ui_HpBar.ins.DisplayHp(playerData.hp / playerData.hpMax);

			//health <= 0 die
			if (playerData.hp <= 0)
			{
				Debug.Log("GameOver");
				transform.root.gameObject.SetActive(false);
				return;
			}
		}
	}

	private void OnTriggerEnter(Collider _col)
    {
        if (_col.CompareTag("Item"))
        {
            Item _item = _col.GetComponent<Item>();
            if (_item != null)
            {

                // 해당 아이템을 내 인벤토리에 넣어주기
                ItemDrop _itemDrop = _item.GetItemDrop();
                if (Ui_Inventory.ins == null)
                {
                    //Debug.Log("@@@@Ui_Inventory.ins 없어서 강제 삭제");
                    _item.DestroyItem();
                    return;
                }

                bool _eat = Ui_Inventory.ins.Input_ItemData(_itemDrop);
                if (_eat)
                {
                    //Debug.Log("아이템 먹음");
                    //2. 해당 아이템은 필드에서 삭제...
                    _item.DestroyItem();
                }
                else
                {
                    Debug.Log("아이템 못먹음(자리가없다)");
                }
            }
        }
        //else if (_col.CompareTag("Monster"))
        //{
        //    // 몬스터추가
        //    Monster _monster = _col.GetComponent<Monster>();
        //    if (_monster != null)
        //    {
        //        listAroundMonster.Add(_monster);
        //    }
        //}
    }

    //public void OnTriggerExit(Collider _col)
    //{
    //    if (_col.CompareTag("Monster"))
    //    {
    //        Monster _monster = _col.GetComponent<Monster>();
    //        if (_monster != null)
    //        {
    //            listAroundMonster.Remove(_monster);
    //        }
    //    }
    //}
    public void SetPortal(Transform _point)
	{
        transform.position = _point.position;
        transform.rotation = _point.rotation;
	}
    
    public void SetDamage(float _damage, Vector3 _attackedDirView, Vector3 _charHeight)
    {
        takeDamage += _damage;
        hitPoint = trans.position + _attackedDirView * charThick + _charHeight;
    }

    public bool SetHealth(float _plus)
	{
		if (playerData.hp >= playerData.hpMax)
		{
            return false;
		}
        playerData.hp += _plus;
        playerData.hp = Mathf.Clamp(playerData.hp, 0, playerData.hpMax);
        Ui_HpBar.ins.DisplayHp(playerData.hp / playerData.hpMax);
        return true;

    }

    public void AddExp(Monster _dieMonster, float _exp)
    {
        //Debug.Log("_exp:" + _exp);
        int playLv = playerData.lv;
		playerData.exp += _exp;
        float _curExp = playerData.exp % 100f;
        Ui_HpBar.ins.DisplayExp(_curExp / 100f);

		if (playerData.lv != playLv)
		{
            Debug.Log("LV" + playerData.lv);
            Ui_HpBar.ins.DisplayLV(playerData.lv);
            ParticleSystem _ps
                = PoolManager.ins.Instantiate("LightDome", trans.position, Quaternion.identity).GetComponent<ParticleSystem>();
            _ps.Stop();
            _ps.Play();
        }

        if( listTargetFakeMonster.Contains(_dieMonster))
        {
            listTargetFakeMonster.Remove(_dieMonster);
        }

        if(_dieMonster == targetFake)
        {
            targetFake = null;
        }

        if(_dieMonster == targetReal)
        {
            targetFake = null;
        }
        //파티클..
        //애니...
        //lv++...
        //레벨업 보상...
        Debug.Log("@@@경험치가 일정이상 올라가면 레벨업");
    }

    private void OnDrawGizmos()
    {
		/*
        //see
        Gizmos.color = Color.white;
        float _angle = transform.eulerAngles.y;
        float _angleL = _angle - seeAngle * 0.5f;
        float _angleR = _angle + seeAngle * 0.5f;

        Vector3 _dirL = Quaternion.Euler(Vector3.up * _angleL) * Vector3.forward * recognizeRadius;
        Vector3 _dirR = Quaternion.Euler(Vector3.up * _angleR) * Vector3.forward * recognizeRadius;

        Gizmos.DrawRay(transform.position, _dirL);
        Gizmos.DrawRay(transform.position, _dirR);
		/**/


		//radius
		if(playerData != null && playerData.charClass != null)
		{	
			Gizmos2.DrawCircle(transform.position, Color.red, playerData.attackRadius);
			Gizmos2.DrawCircle(transform.position, Color.yellow, playerData.recognizeRadius);
		}
		
    }
}
