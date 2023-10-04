using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Step11
{
    public class ToolTipManager : MonoBehaviour
    {
        #region
        public static ToolTipManager ins;
        private void Awake()
        {
            ins = this;
        }
        #endregion

        public enum eLanguageType { None, Korea, English }
        public eLanguageType languageType = eLanguageType.Korea;
        SSParser parserToolTip = new SSParser();
        Dictionary<string, ToolTip> dic_ToolTip = new Dictionary<string, ToolTip>();

        public void ReadAndParse()
        {
            //1. file/Net -> read
            //2. parser.parsing(xml데이타, 구분자);
            //3. while(parser.next()) 
            //4. dic넣어둬~~~
            //5. 사용...
            ToolTip _toolTip;
            string strFileData = SSUtil.load("txt/tooltip");
            string _key;
            parserToolTip.parsing(strFileData, "tooltip");
            while (parserToolTip.next())
            {
                _key = parserToolTip.getString("count");
                if (!dic_ToolTip.ContainsKey(_key))
                {
                    _toolTip = new ToolTip();
                    dic_ToolTip.Add(_key, _toolTip);

                    _toolTip.count  = parserToolTip.getString("count");
                    _toolTip.tip    = parserToolTip.getString("tip");
                    _toolTip.eng    = parserToolTip.getString("eng");
                }
            }
        }

        public string GetToolTip(string _key)
        {
            string _rtn = "";
            if (dic_ToolTip.ContainsKey(_key))
            {
                if (languageType == eLanguageType.Korea)
                    _rtn = dic_ToolTip[_key].tip;
                else if (languageType == eLanguageType.English)
                    _rtn = dic_ToolTip[_key].eng;
            }
            return _rtn;
        }

    }
}