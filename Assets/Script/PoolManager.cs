using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    public string name;
    public GameObject prefab;
    public int count;
    public Transform parent;
}
public class PoolManager : MonoBehaviour
{
    //                Instantiate(GameObject, p, r)
    //PoolManager.ins.Instantiate(GameObject,
    //PoolManager.ins.Instantiate(string,     p, r) as GameObject -> GetComponent<T>();
    //UI                                                          -> GetComponent<Image>();
    //ParticleSystem                                              -> GetComponent<ParticleSystem>();
    #region sigleton
    public static PoolManager ins;
    private void Awake()
    {
        ins = this;
        Init();
    }
    #endregion

    public List<ObjectData> objList = new List<ObjectData>();
    public bool willGrow = true;

    public Dictionary<string, List<GameObject>> poolList = new Dictionary<string, List<GameObject>>();

    void Init()
    {
        GameObject _go, _obj;
        List<GameObject> _list;
        int _count;
        Transform _parent;
        for (int j = 0, jmax = objList.Count; j < jmax; j++)
        {
            _count = objList[j].count;
            _obj = objList[j].prefab;
            _parent = objList[j].parent;
            _list = new List<GameObject>();
            poolList.Add(_obj.name, _list);

            for (int i = 0; i < _count; i++)
            {
                _go = Instantiate(_obj) as GameObject;
                _go.transform.SetParent(_parent);
                _go.SetActive(false);
                _list.Add(_go);
            }
        }
    }

    public GameObject Instantiate(string _name, Vector3 _pos, Quaternion _rot)
    {
        GameObject _rtnObject = Instantiate(_name);
        _rtnObject.transform.position = _pos;
        _rtnObject.transform.rotation = _rot;

        return _rtnObject;

    }

    public GameObject Instantiate(string _name)
    {
        if (!poolList.ContainsKey(_name))
        {
            Debug.LogError("Not Found Pooling GameObject name" + _name);
            return null;
        }

        GameObject _rtn = null;
        bool _bFind = false;
        List<GameObject> _list = poolList[_name];
        for (int i = 0; i < _list.Count; i++)
        {
            //gameobject 비할성화 -> 지금사용중이지 않다는 의미...
            if (!_list[i].activeInHierarchy)
            {
                _rtn = _list[i];
                _bFind = true;
                _rtn.SetActive(true);
                break;
            }
        }

        //not found > create
        if (!_bFind && willGrow)
        {
            ObjectData _obj = GetObject(_name);
            GameObject _go = Instantiate(_obj.prefab) as GameObject;
            _go.transform.SetParent(_obj.parent);
            _go.SetActive(true);
            _list.Add(_go);

            _rtn = _go;
        }

        return _rtn;
    }

    ObjectData GetObject(string _name)
    {
        ObjectData _obj = null;
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i].name == _name)
            {
                _obj = objList[i];
                break;
            }
        }
        return _obj;
    }

    [ContextMenu("이름 자동완성")]
    void Editor_CreateName()
    {
        //Debug.Log("Perform operation");
        for (int i = 0; i < objList.Count; i++)
        {
            objList[i].name = objList[i].prefab.name;
        }
    }
}