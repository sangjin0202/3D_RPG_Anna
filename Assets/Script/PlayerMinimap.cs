using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct MinimapData
{
    public Vector3 viewDir, viewDirN;
    public float viewDistance;
    public Monster monster;
    public Vector3 viewPort;
    public RectTransform minimapPoint;


    public MinimapData(Vector3 _viewdir, Vector3 _viewDirN, float _viewDistance, Monster _monster, float _radius)
    {
        viewDir      = _viewdir;
        viewDirN     = _viewDirN;
        viewDistance = _viewDistance;
        monster      = _monster;
        minimapPoint = null;

        viewPort = new Vector3(viewDir.x / _radius, viewDir.z / _radius, 0);
    }
}

public class PlayerMinimap : MonoBehaviour
{
    Transform trans;
    float searchTime;
    public float SEARCH_TIME = 0.5f;
    public float minimapRadius = 20f;
    Player player;
    public List<MinimapData> listMinimapData = new List<MinimapData>();

    void Start()
    {
        trans = transform;
        player = GetComponent<Player>();
    }

    void Update()
    {
        Search();
    }

    void Search()
    {
        if (Time.time < searchTime)
        {
            return;
        }
        searchTime = Time.time + SEARCH_TIME;
        //1. 주위 검색...
        //거리범위 내에 Player를 검색을 한다... (Layer)
        float _radius = minimapRadius;
        Collider[] _cols = Physics.OverlapSphere(trans.position, _radius, player.targetMask);
        //Debug.Log(_cols.Length);
        Transform _viewTrans;
        Vector3 _viewDir, _viewDirN;
        float _viewDistance;

        Ui_Minimap.ins.ClearData(listMinimapData);
        listMinimapData.Clear();
        for (int i = 0, imax = _cols.Length; i < imax; i++)
        {
            _viewTrans    = _cols[i].transform;

            _viewDir      = _viewTrans.position - trans.position;
            _viewDirN     = _viewDir.normalized;
            _viewDistance = _viewDir.magnitude;

            Monster _monster = _cols[i].GetComponent<Monster>();
            if (_monster != null)
            {
                listMinimapData.Add(new MinimapData(_viewDir, _viewDirN, _viewDistance, _monster, _radius));
            }
        }
        Ui_Minimap.ins.SetData(listMinimapData, _radius);
    }

    private void OnDrawGizmos()
    {
        Gizmos2.DrawCircle(transform.position, Color.green, minimapRadius);
    }
}