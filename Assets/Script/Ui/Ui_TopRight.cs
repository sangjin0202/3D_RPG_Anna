using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_TopRight : MonoBehaviour
{
    #region singleton
    public static Ui_TopRight ins;
    private void Awake()
    {
        ins = this;

    }
    #endregion
    public void Invoke_InventoryUIOpen()
    {
        Ui_Inventory.ins.Invoke_Open();
    }

}
