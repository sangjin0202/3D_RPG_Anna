using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    //local Point -> World Point
    public List<Vector3> list = new List<Vector3>();
    List<Vector3> worldList = new List<Vector3>();
    int index = -1;
    public Vector3 destinationPoint;

    void Start()
    {
        worldList.Clear();

        if (Random.Range(0f,1f) <= 0.5f)
        {
            for (int i = 0, imax = list.Count; i < imax; i++)
            {
                worldList.Add(transform.TransformPoint(list[i]));
            }
        }
        else
        {
            for (int i = list.Count-1, imax = 0; i >= 0; i--)
            {
                worldList.Add(transform.TransformPoint(list[i]));
            }
        }
    }


    public Vector3 CalculateNextPoint()
    {
        index = (index + 1) % worldList.Count;
        destinationPoint = worldList[index];
        return destinationPoint;
    }

    //가장가까운 곳의 포인트로 이동...

    //도달 지점근처까지 왔나
    public bool CheckNearDestination(float _stopDistance)
    {
        float _stopDistanceDouble = _stopDistance + _stopDistance;
        Vector3 _dir = destinationPoint - transform.position;
        
        //유효거리... 이내냐???
        if(_dir.sqrMagnitude < _stopDistanceDouble)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (Application.isPlaying)
        {
            for (int i = 0, imax = worldList.Count - 1; i < imax; i++)
            {
                Gizmos.DrawLine(worldList[i], worldList[i + 1]);
            }
        }
        else
        {
            //editor
            for (int i = 0, imax = list.Count - 1; i < imax; i++)
            {
                Gizmos.DrawLine(
                    transform.TransformPoint(list[i]),
                    transform.TransformPoint(list[i + 1]));
            }
        }
    }
}
