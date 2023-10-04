using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Step11
{
    public class Ui_XXX : MonoBehaviour
    {
        public string toolTipCount;
        public Text text;

        public int itemcode;
        public Image image;
        public Text text2;
        public Text text3;

        public void Invoke_Click()
        {
            text.text = ToolTipManager.ins.GetToolTip(toolTipCount);
            image.sprite = ItemInfoManager.ins.GetIcon(itemcode);
            text2.text = ItemInfoManager.ins.GetBasedef(itemcode).ToString();
            text3.text = ItemInfoManager.ins.GetItemName(itemcode).ToString();
        }
    }
}