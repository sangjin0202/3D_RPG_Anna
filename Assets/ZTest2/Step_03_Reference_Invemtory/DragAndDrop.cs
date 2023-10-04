using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class DragAndDrop : MonoBehaviour
    {
        Transform target;
        Camera camera;
        Plane plane;
        Vector3 offset;
        private void Start()
        {
            camera = Camera.main;
        }
        public void Update()
        {
            Ray _ray2 = camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_ray2.origin, _ray2.direction);

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit _hit;
                Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit))
                {
                    target = _hit.transform;
                    offset = target.position - _hit.point;
                    plane = new Plane(-camera.transform.forward, _hit.point);
                }
            }
            else if (Input.GetMouseButton(0) && target)
            {
                float _distance;
                Ray _ray = camera.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(_ray, out _distance))
                {
                    target.position = _ray.GetPoint(_distance) + offset;
                }
            }
            else if (Input.GetMouseButtonUp(0) && target)
            {
                target = null;
               // Bounds bounds = target.GetComponent<Collider>().bounds;
               // if(bounds.Intersects())
            }
        }
    }
}