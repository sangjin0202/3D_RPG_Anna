using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class CharInfoManager : MonoBehaviour
{
    #region
    public static CharInfoManager ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion


    public AtlasManager atlas;
    SSParser parser = new SSParser();

    Dictionary<int, CharClass> dic_CharClass = new Dictionary<int, CharClass>();
	bool bRead = false;

    public void ReadAndParse()
    {
        if (bRead) return;
		//1. file/Net -> read
		//2. parser.parsing(xml데이타, 구분자);
		//3. while(parser.next()) 
		//4. dic넣어둬~~~
		//5. 사용...
		string strFileData = SSUtil.load("txt/gameinfo");

		//1. charclass
		parser.parsing(strFileData, "charclass");
        ParseCharClass(parser);

        bRead = true;
    }

	
    #region charclass
    void ParseCharClass(SSParser _parser)
    {
        int _classcode;
		CharClass _charClass;
        while (_parser.next())
        {
            _classcode = _parser.getInt("classcode");
            if (!dic_CharClass.ContainsKey(_classcode))
            {
                //base 부분을 함수로 만듬...
                _charClass = new CharClass();

				//icon	classname
				//baseatt	basehp	basedef	baseatttime	baseattradius	baserecognizeradius	plusatt	plushp	plusdef
				_charClass.classcode	= _classcode;

				_charClass.icon			= _parser.getString("icon");
				_charClass.classname	= _parser.getString("classname");

				_charClass.baseatt				= _parser.getFloat("baseatt");
                _charClass.basehp				= _parser.getFloat("basehp");
                _charClass.basedef				= _parser.getFloat("basedef");
				_charClass.baseatttime			= _parser.getFloat("baseatttime");
				_charClass.baseattradius		= _parser.getFloat("baseattradius");
				_charClass.baserecognizeradius	= _parser.getFloat("baserecognizeradius");

				_charClass.plusatt	= _parser.getFloat("plusatt");
				_charClass.plushp	= _parser.getFloat("plushp");
				_charClass.plusdef	= _parser.getFloat("plusdef");


				dic_CharClass.Add(_classcode, _charClass);
				if (Constant.DEBUG_CHARINFOMANAGER)Debug.Log(_charClass.ToString());
            }
        }
    }

	public CharClass GetCharClass(int _classcode)
	{
		CharClass _charClass = null;
		if (dic_CharClass.ContainsKey(_classcode))
		{
			_charClass = dic_CharClass[_classcode];
		}
		else
		{
			#if UNITY_EDITOR
				Debug.LogError("classcode:" + _classcode + " not found");
			#endif
		}
		return _charClass;
	}
	#endregion
	/**/
}

[System.Serializable]
public class CharClass
{
	public int classcode;
	public string icon;
	public string classname;
	public float baseatt;
	public float basehp;
	public float basedef;
	public float baseatttime;
	public float baseattradius;
	public float baserecognizeradius;
	public float plusatt;
	public float plushp;
	public float plusdef;

	public override string ToString()
	{
		return "classcode" + classcode
			+ " icon" + icon
			+ " classname" + classname
			+ " baseatt" + baseatt
			+ " basehp" + basehp
			+ " basedef" + basedef
			+ " baseatttime" + baseatttime
			+ " baseattradius" + baseattradius
			+ " baserecognizeradius" + baserecognizeradius
			+ " plusatt" + plusatt
			+ " plushp" + plushp
			+ " plusdef" + plusdef;
	}
}
