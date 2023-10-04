using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_move : MonoBehaviour
{
    Animator anim;
    public float speed = 2f;
    public List<Transform> list = new List<Transform>();
    List<Vector3> listV3 = new List<Vector3>();
    int index;
    Vector3 targetPos;

    float waitTime;
    public float WAIT_TIME = 1f;


    void Start()
    {
        anim = GetComponent<Animator>();
        for (int i = 0; i < list.Count; i++)
        {
            listV3.Add(list[i].position);
            list[i].gameObject.SetActive(false);
        }

        index = 0;
        targetPos = listV3[index];

        Vector3 _dir = targetPos - transform.position;
        transform.rotation = Quaternion.LookRotation(_dir);

    }

    void Update()
    {
        if (Time.time < waitTime)
        {
            anim.SetTrigger("Idle");
            return;
        }

        // 정확한 그 지점
        //Vector3.MoveTowards(p0, p1, 지정된속도 >> 단위변화위치)
        //       .Lerp(p0, p1, 0 ~ 1)
        //       .Slerp(p0, p1, 0 ~ 1)
        if (transform.position == targetPos)
        {
            index = (index + 1) % listV3.Count;
            targetPos = listV3[index];

            //일정시간 휴식
            waitTime = Time.time + WAIT_TIME;



            // 방향결정
            Vector3 _dir = targetPos - transform.position;
            if (_dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(_dir);
            }

        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        anim.SetTrigger("Run");



        // 키보드 또는 조이스틱 컨트롤할때
        //transform.Translate

        // 원시이동
        //transform.position

    }
#if UNITY_EDITOR
    public float debugRadius = 0.2f;
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying && listV3.Count > 0)
        {
            for (int i = 0; i < listV3.Count; i++)
            {
                Gizmos.DrawWireSphere(listV3[i], debugRadius);
            }
        }
    }
#endif
}