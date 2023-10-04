using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Minimap : MonoBehaviour
{
    #region sigletone
    public static Ui_Minimap ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    public RectTransform minimapRoot;
    Vector2 sizeDelta;
    private void Start()
    {
        sizeDelta = minimapRoot.sizeDelta * 0.5f;
        //Debug.Log(minimapRoot.sizeDelta);
    }
    public void ClearData(List<MinimapData> _list)
    {
        for (int i = 0, imax = _list.Count; i < imax; i++)
        {
            if (_list[i].minimapPoint != null)
            {
                _list[i].minimapPoint.gameObject.SetActive(false);
            }
        }
    }

    public void SetData(List<MinimapData> _list, float _radius)
    {
        MinimapData _data;
        for (int i = 0, imax = _list.Count; i < imax; i++)
        {
            _data = _list[i];
            if (_data.minimapPoint != null)
            {
                _data.minimapPoint.gameObject.SetActive(true);
            }
            else
            {
                _data.minimapPoint = (RectTransform)PoolManager.ins.Instantiate("MinimapPoint").transform;
                _list[i] = _data;
            }

            //위치계산하기
            _data.minimapPoint.anchoredPosition = new Vector2(_data.viewDir.x 
                                                                / _radius * sizeDelta.x, _data.viewDir.z 
                                                                / _radius * sizeDelta.y);
        }
    }
}
