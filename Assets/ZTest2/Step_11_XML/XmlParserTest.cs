using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step11
{
    public class XmlParserTest : MonoBehaviour
    {
        SSParser parserToolTip = new SSParser();
        SSParser parserServer = new SSParser();
        SSParser parserItemInfo = new SSParser();

        Dictionary<string, ToolTip> dic_ToolTip = new Dictionary<string, ToolTip>();
        Dictionary<string, ItemInfoBase> dic_ItemInfoBase = new Dictionary<string, ItemInfoBase>();
        string strFileData, text;
        string strServerData =
        "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
        +   "<rows>"
        +       "<equipment><resultcode>1111</resultcode></equipment>" 
        +       "<equipment><resultcode>1112</resultcode></equipment>" 
        +       "<equipment><resultcode>1113</resultcode></equipment>" 
        +       "<equipment><resultcode>1114</resultcode></equipment>" 
        +    "</rows>";

        void Start()
        {
            string _key;
            /*
            //-----------------------------------------
            //1. file/Net -> read
            //2. parser.parsing(xml데이터, 구분자);
            //3. while(parser.next())
            //      -> 존재유무 파악
            //      -> <= parser.getString(셀구분자);
            //-----------------------------------------
            //1. file/Net -> read
            //   이미 들어와 있어요...

            //2. parser.parsing(xml데이터, 구분자);
            parserServer.parsing(strServerData, "equipment");

            //3. while(parser.next())
            //      -> 존재유무 파악
            //      -> <= parser.getString(셀구분자);
            while(parserServer.next())
            {
                string _srt = parserServer.getString("resultcode");
                Debug.Log(_srt);
            }

            //1. file/Net -> read
            //2. parser.parsing(xml데이타, 구분자);
            //3. while(parser.next()) 
            //4. dic넣어둬~~~
            //5. 사용...
            
            ToolTip _toolTip;
            strFileData = SSUtil.load("txt/tooltip");
            Debug.Log(strFileData);
            parserToolTip.parsing(strFileData, "tooltip");
            while (parserToolTip.next())
            {
                _key = parserToolTip.getString("count");
                if ( !dic_ToolTip.ContainsKey(_key) )
                {
                    _toolTip = new ToolTip();
                    dic_ToolTip.Add(_key, _toolTip);

                    _toolTip.count = parserToolTip.getString("count");
                    _toolTip.tip = parserToolTip.getString("tip");
                    _toolTip.eng = parserToolTip.getString("eng");
                }
                //Debug.Log(parserToolTip.getString("count")
                //    + " : "
                //    + parserToolTip.getString("tip")
                //    + " : "
                //    + parserToolTip.getString("eng"));
            }
            Debug.Log(GetToolTip("0"));
            Debug.Log(GetToolTip("55"));
            */

            //1. file/Net -> read
            //2. parser.parsing(xml데이타, 구분자);
            //3. while(parser.next()) 
            //4. dic넣어둬~~~
            //5. 사용...
            ItemInfoBase _itemInfoBase;
            strFileData = SSUtil.load("txt/iteminfo");
            parserItemInfo.parsing(strFileData, "wearpart");
            while (parserItemInfo.next())
            {
                _key = parserItemInfo.getString("itemcode");
                if (!dic_ItemInfoBase.ContainsKey(_key))
                {
                    _itemInfoBase = new ItemInfoBase();
                    dic_ItemInfoBase.Add(_key, _itemInfoBase);

                    _itemInfoBase.itemcode = parserItemInfo.getInt("itemcode");
                    _itemInfoBase.category = parserItemInfo.getInt("category");
                    _itemInfoBase.subcategory = parserItemInfo.getInt("subcategory");
                    _itemInfoBase.equpslot = parserItemInfo.getInt("equpslot");
                    _itemInfoBase.itemname = parserItemInfo.getString("itemname");
                }
            }
            //Debug.Log(GetItemInfoItemName("100"));
            //Debug.Log(GetItemInfoItemName("200"));
        }

        string GetToolTip(string _key)
        {
            string _rtn = "";
            if (dic_ToolTip.ContainsKey(_key))
            {
                _rtn = dic_ToolTip[_key].tip;
                //_rtn = dic_ToolTip[_key].eng;
            }
            return _rtn;
        }

        string GetItemInfoItemName(string _itemcode)
        {
            string _rtn = "";
            if (dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                _rtn = dic_ItemInfoBase[_itemcode].itemname;
            }
            return _rtn;
        }
    }
}