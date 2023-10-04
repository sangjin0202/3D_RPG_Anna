using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step03
{
    public class CameraRay : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit _hit;
                Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit))
                {
                    Item2 _Item2 = _hit.collider.GetComponent<Item2>();
                    Obstacle _obstacle = _hit.collider.GetComponent<Obstacle>();
                    if (_Item2 != null)
                    {
                        _Item2.ClickEvent();
                    }

                    if(_obstacle !=null)
                    {
                        _obstacle.ClickEvent();
                    }
                }    

            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit _hit;
                Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit))
                {
                    Item2 _Item2 = _hit.collider.GetComponent<Item2>();
                    if (_Item2 != null)
                    {
                        _Item2.RemoveEvent();
                    }
                }

            }
        }
    }
}
