using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    Camera mainCamera;
    Vector3 starPos = Vector3.zero;   // 카메라의 시작위치
    Vector3 oldPos;                   // Updata에서의 캐릭터의 위치
    public GameObject player;           // 플레이어
    public float zoomSpeed;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        oldPos = player.transform.position;
        //카메라가 플레이어 위치를 바라본다.
        transform.LookAt(player.transform.position);

    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            mainCamera.fieldOfView += distance;
        }
    }



    private void Update()
    {
        Zoom();
    }

    private void LateUpdate()
    {
        // 캐릭터가 움직인 거리차이
        Vector3 deltaPos = player.transform.position - oldPos;
        transform.position += deltaPos;
        oldPos = player.transform.position;
    }

}
