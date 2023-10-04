using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_HpBar : MonoBehaviour
{
    #region singleton
    public static Ui_HpBar ins;
    private void Awake()
    {
        ins = this;

    }
    #endregion

    public Image hpBar;    // 1000 / 1000
    public Image expBar;   // 1000 / 1000
    public Text lvText;

    public void DisplayHp(float _value)
	{
        hpBar.fillAmount = Mathf.Clamp01(_value);
	}

    public void DisplayExp(float _value)
    {
        expBar.fillAmount = Mathf.Clamp01(_value);
    }

    public void DisplayLV(int _value)
    {
        lvText.text = _value.ToString();
    }
}
