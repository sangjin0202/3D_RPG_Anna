using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : FSM<eMonsterManagerState>
{
    #region sigletone
    public static MonsterManager ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    //스폰지역... 스폰할 몬스터...
    public List<SpawnArea> listSpawnArea = new List<SpawnArea>();

    //실제 계산에 들어가는 정보... (prefab, point
    //public int totalMonsterCount;
    //public List<SpawnPoint> listSpawnPoint      = new List<SpawnPoint>();
    //int index;
    //public List<Transform> listSpawn        = new List<Transform>();
    //public List<Monster> listMonsterPrefab  = new List<Monster>();

    //필드에 소환된 몬스터...
    public float RESPAWN_TIME = 0.5f;
    float respawnTime;
    public List<Monster> listAliveMonster = new List<Monster>();
    public List<RespawnData> listRespawnData = new List<RespawnData>();

    void Start()
    {
        //Loading           -> 몬스터를 소환...
        //                      GameManager -> 게임 진행해도 된다고 알려줘...
        //Spawning....      -> 유저가 몬스터를 사냥하면... 줄어든 그 몬스터를 소환...
        //                     일정시간후에 소환...
        //Destory           -> 필드에 모든 몬스터 제거...

        AddState(eMonsterManagerState.Loading,   In_Loading,   null,            null);
        AddState(eMonsterManagerState.Spawning,  In_Spawning,  Modify_Spawning, null);
        AddState(eMonsterManagerState.Destroy,   In_Destroy,   Modify_Destroy,  null);

        MoveState(eMonsterManagerState.Loading);
    }


    #region Loading
    public bool IsLoad()
    {
        return curState != eMonsterManagerState.Loading;
    }

    void In_Loading()
    {
        if (Constant.DEBUG_MONSTER_MANAGER) Debug.Log(this + " In_Loading");

        AllSpawnMonster();
        MoveState(eMonsterManagerState.Spawning);
    }

    void AllSpawnMonster()
    {
        //totalMonsterCount = 0;
        //index = 0;

        //처음 생성되는 단계... >> 몬스터 생성하기...
        SpawnArea _spawnArea;
        
        for (int i = 0, imax = listSpawnArea.Count; i < imax; i++)
        {
            _spawnArea           = listSpawnArea[i];

            //지역에 있는 몬스터를 수량만큼 생성...
            for (int j = 0, jmax = _spawnArea.MONSTER_COUNT; j < jmax; j++)
            {
                SpawnMonster(_spawnArea);
            }
        }

        //리스폰 되어야할 것은 지금 이지역에서는 없는 상태...
        listRespawnData.Clear();
    }

    void SpawnMonster(SpawnArea _spawnArea)
    {
        Transform _t = _spawnArea.GetNextPoint();

        Monster _monster = PoolManager.ins.Instantiate(_spawnArea.prefab.name, _t.position, Quaternion.identity).GetComponent<Monster>();
        //_monster.name       += _spawnArea.spawPointIndex;
        _monster.InitData(_spawnArea, null);
        //_monster.transform.SetParent(transform);

        //살아있는 몬스터 리스트 추가
        listAliveMonster.Add(_monster);
    }
    #endregion

    #region Spawning
    void In_Spawning()
    {
        if (Constant.DEBUG_GM) Debug.Log(this + " In_Spawning");

    }

    //죽기 직전의 몬스터가 불러줘야함...
    public void RemoveMonster(Monster _monster)
    {
        //Debug.Log("@@@@ 몬스터가 죽음 >> 신규소환 ");
        //몬스터 사망  -> 관리 리스트에서 삭제...
        if (listAliveMonster.Contains(_monster))
        {
            listAliveMonster.Remove(_monster);
        }

        if (!_monster.spawnArea.IsEmpty())
        {
            //             -> 다음 몬스터 생성하기 위해서 생성커멘더에 등록,,,
            //                시간 + SpawnArea
            RespawnData _rd = new RespawnData(_monster.spawnArea);
            listRespawnData.Add(_rd);
        }
    }


    void Modify_Spawning()
    {
        if(Time.time > respawnTime)
        {
            respawnTime = Time.time + RESPAWN_TIME;
            CheckRespawnData();
        }
    }

    void CheckRespawnData()
    {
        //다음 생성 명령어가 들어 있는 리스트에서 시간이되면 리스폰...
        RespawnData _rd;
        SpawnArea _spawnArea;
        Transform _t;
        for (int i = listRespawnData.Count -1; i >= 0; i--)
        {
            _rd = listRespawnData[i];
            if (Time.time > _rd.nextTime)
            {
                //리스폰...
                SpawnMonster(_rd.spawnArea);
                
                //리스폰에서 이것은 제거...
                listRespawnData.RemoveAt(i);
            }
        }

        //foreach(RespawnData _rd in listRespawnData)
        //{
        //    if(Time.time > _rd.nextTime)
        //    {
        //        //소환...

        //    }
        //}
    }
    #endregion

    #region Destroy
    void In_Destroy()
    {
        if (Constant.DEBUG_GM) Debug.Log(this + " Destory");

    }


    void Modify_Destroy()
    {

    }
    #endregion

    [ContextMenu ("스폰지역 넣어주기...")]
    void Editor_SpawnArea()
    {
        SpawnArea _spawnArea;
        Transform _t;
        for(int i = 0, imax = listSpawnArea.Count; i < imax; i++)
        {
            _spawnArea  = listSpawnArea[i];
            _t          = _spawnArea.spawnPointMaster;

            _spawnArea.SetName(_t.name);
            _spawnArea.Clear();
            foreach (Transform _child in _t)
            {
                _spawnArea.Add(_child);
            }
            //for (int j = 0, jmax = _t.childCount; j < jmax; j++ )
            //{
            //    _spawnArea.Add(_t.GetChild(j));
            //}
        }
    }

}

[System.Serializable]
public class RespawnData
{
    public float nextTime;
    public SpawnArea spawnArea;

    public RespawnData(SpawnArea _spawnArea)
    {
        spawnArea   = _spawnArea;
        nextTime    = Time.time + spawnArea.REGEN_TIME;
    }
}

[System.Serializable]
public class SpawnArea
{
    public string strLevel;
    public int level;
    public int MONSTER_COUNT;
    public float REGEN_TIME = 10f;
    public Monster prefab;
    public Transform spawnPointMaster;          //Editor용도... 하단의 list -> listSpawnPoint
    public List<int> listSpawnItem = new List<int>();

    [HideInInspector] public int spawPointIndex = -1;
    public List<Transform> listSpawnPoint = new List<Transform>();

    //[HideInInspector] public int monsterCount;
    public bool IsEmpty() { return listSpawnPoint.Count <= 0; }

    public void SetName(string _name)
    {
        strLevel = _name;
        string[] _s = _name.Split('_');
        bool _b = int.TryParse(_s[1], out level);
        if(_b == false)
        {
            Debug.LogError("숫자를 입력해주세요" + _name);
        }
    }

    public void Clear()
    {
        listSpawnPoint.Clear();
    }

    public void Add(Transform _child)
    {
        listSpawnPoint.Add(_child);
    }

    public Transform GetNextPoint()
    {
        spawPointIndex = (spawPointIndex + 1) % listSpawnPoint.Count;
        return listSpawnPoint[spawPointIndex];
    }

}

//public class SpawnPoint
//{
//    public SpawnArea spawnArea;
//    public Monster prefab           { get { return spawnArea.prefab; } }
//    public Vector3 point;
//    public int level                { get { return spawnArea.level; } }
//    public List<int> listSpawnItem  { get { return spawnArea.listSpawnItem; } }

//    public SpawnPoint() { }
//    public SpawnPoint(SpawnArea _spawnArea, Vector3 _point)
//    {
//        spawnArea = _spawnArea;
//        point = _point;
//    }

    
//}
