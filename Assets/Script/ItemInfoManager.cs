using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class ItemInfoManager : MonoBehaviour
{
    #region
    public static ItemInfoManager ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    public AtlasManager atlas;
    SSParser parser = new SSParser();
    Dictionary<int, ItemInfoBase> dic_ItemInfoBase = new Dictionary<int, ItemInfoBase>();

    Dictionary<int, WearPart> dic_WearPart = new Dictionary<int, WearPart>();
    Dictionary<int, Randombox> dic_Randombox = new Dictionary<int, Randombox>();
    Dictionary<int, Food> dic_Food = new Dictionary<int, Food>();
    Dictionary<int, Cashcoin> dic_Cashcoin = new Dictionary<int, Cashcoin>();
    Dictionary<int, Gamecoin> dic_Gamecoin = new Dictionary<int, Gamecoin>();
    Dictionary<int, Staticinfo> dic_Staticinfo = new Dictionary<int, Staticinfo>();
    Dictionary<int, Levelupreward> dic_Levelupreward = new Dictionary<int, Levelupreward>();
    bool bRead = false;

    public void ReadAndParse()
    {
        if (bRead) return;
		//1. file/Net -> read
		//2. parser.parsing(xml데이타, 구분자);
		//3. while(parser.next()) 
		//4. dic넣어둬~~~
		//5. 사용...
		string strFileData = SSUtil.load("txt/iteminfo");

        //1. wearpart
        parser.parsing(strFileData, "wearpart");
        ParseWearPart(parser);

        //2. randombox... 
        parser.parsing(strFileData, "randombox");
        ParseReandomBox(parser);

        //3. food
        parser.parsing(strFileData, "food");
        ParseFood(parser);

        //4. cashcoin
        parser.parsing(strFileData, "cashcoin");
        ParseCashCoin(parser);

        //5. gamecost
        parser.parsing(strFileData, "gamecoin");
        ParseGameCoin(parser);

        //6. staticinfo
        parser.parsing(strFileData, "staticinfo");
        ParseStaticinfo(parser);

        //7. livelupreward
        parser.parsing(strFileData, "livelupreward");
        ParseLevelupreward(parser);

        bRead = true;
    }


    #region ItemInfoBase
    void ParseBase(SSParser _parser, ItemInfoBase _iteminfoBase)
    {
        int _itemcode = _parser.getInt("itemcode");
        dic_ItemInfoBase.Add(_itemcode, _iteminfoBase);

        _iteminfoBase.itemcode = _itemcode;
        _iteminfoBase.category = _parser.getInt("category");
        _iteminfoBase.subcategory = _parser.getInt("subcategory");
        _iteminfoBase.equpslot = _parser.getInt("equpslot");
        _iteminfoBase.itemname = _parser.getString("itemname");
        _iteminfoBase.activate = _parser.getInt("activate");
        _iteminfoBase.toplist = _parser.getInt("toplist");
        _iteminfoBase.grade = _parser.getInt("grade");
        _iteminfoBase.discount = _parser.getInt("discount");
        _iteminfoBase.icon = _parser.getString("icon");
        _iteminfoBase.playerlv = _parser.getInt("playerlv");
        _iteminfoBase.multistate = _parser.getInt("multistate");
        _iteminfoBase.gamecoin = _parser.getInt("gamecost");
        _iteminfoBase.cashcost = _parser.getInt("cashcost");
        _iteminfoBase.buyamount = _parser.getInt("buyamount");
        _iteminfoBase.sellcost = _parser.getInt("sellcost");
        _iteminfoBase.description = _parser.getString("description");
        //Debug.Log(_iteminfoBase.icon);

    }

    public string GetIconName(int _itemcode)
    {
        string _icon = "";
        if (dic_ItemInfoBase.ContainsKey(_itemcode))
        {
            _icon = dic_ItemInfoBase[_itemcode].icon;
        }
        return _icon;
    }

    public Sprite GetIcon(int _itemcode)
    {
        string _iconName = GetIconName(_itemcode);
        Debug.Log(_itemcode + " : " + _iconName);
        return atlas.GetSprite(_iconName);
    }

    public string GetItemName(int _itemcode)
    {
        string _itemname = "";
        if (dic_ItemInfoBase.ContainsKey(_itemcode))
        {
            _itemname = dic_ItemInfoBase[_itemcode].itemname;
        }
        return _itemname;
    }



    public ItemInfoBase GetItemInfoBase(int _itemcode)
    {
        ItemInfoBase _itemInfoBase = null;
        if(dic_ItemInfoBase.ContainsKey(_itemcode))
        {
            _itemInfoBase = dic_ItemInfoBase[_itemcode];
        }
        else
        {
        #if UNITY_EDITOR
            Debug.LogError("itemcode:" + _itemcode + " not found");
        #endif
        }
        return _itemInfoBase;
    }

    #endregion

    #region wearpart
    void ParseWearPart(SSParser _parser)
    {
        int _itemcode;
        WearPart _wearPart;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _wearPart = new WearPart();
                ParseBase(_parser, _wearPart);

                dic_WearPart.Add(_itemcode, _wearPart);

                _wearPart.att = _parser.getInt("att");
                _wearPart.def = _parser.getInt("def");
                _wearPart.hp = _parser.getInt("hp");
                _wearPart.attspeed100 = _parser.getInt("attspeed100");
                _wearPart.movespeed100 = _parser.getInt("movespeed100");


				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_wearPart.ToString());
            }
        }
    }



    public int GetBasedef(int _itemcode)
    {
        int _def = 0;
        if (dic_WearPart.ContainsKey(_itemcode))
        {
            _def = dic_WearPart[_itemcode].def;
        }
        return _def;
    }

    #endregion

    #region randombox
    void ParseReandomBox(SSParser _parser)
    {
        int _itemcode;
        Randombox _randomBox;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _randomBox = new Randombox();
                ParseBase(_parser, _randomBox);

                dic_Randombox.Add(_itemcode, _randomBox);

                _randomBox.ritemcode1 = _parser.getInt("ritemcode1");
                _randomBox.ritemcode2 = _parser.getInt("ritemcode2");

				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_randomBox.ToString());
            }
        }
    }



    public int GetRitemCode(int _itemcode)
    {
        int _ritemcode = 0;
        if (dic_Randombox.ContainsKey(_itemcode))
        {
            Randombox _randombox = dic_Randombox[_itemcode];
            if (Random.Range(0, 2) == 0)
            {
                _ritemcode = _randombox.ritemcode1;
            }
            else
            {
                _ritemcode = _randombox.ritemcode2;
            }
        }
        return _ritemcode;
    }

    #endregion

    #region food
    void ParseFood(SSParser _parser)
    {
        int _itemcode;
        Food _food;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _food = new Food();
                ParseBase(_parser, _food);

                dic_Food.Add(_itemcode, _food);

                _food.eathp = _parser.getInt("eathp");
                _food.eatmp = _parser.getInt("eatmp");

                _food.attbuff = _parser.getInt("attbuff");
                _food.attTime100 = _parser.getInt("atttime100");
                _food.defbuff = _parser.getInt("defbuff");
                _food.defTime100 = _parser.getInt("deftime100");

				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_food.ToString());
            }
        }
    }

 //   public int GetEatHP(int _itemcode)
	//{
 //       int _eathp = 0;
	//	if (dic_Food.ContainsKey(_itemcode))
	//	{
 //           _eathp = dic_Food[_itemcode].eathp;
	//	}
 //       return _eathp;
	//}

 //   public int GetEatMP(int _itemcode)
 //   {
 //       int _eatmp = 0;
 //       if (dic_Food.ContainsKey(_itemcode))
 //       {
 //           _eatmp = dic_Food[_itemcode].eatmp;
 //       }
 //       return _eatmp;
 //   }


    #endregion

    #region cashcoin
    void ParseCashCoin(SSParser _parser)
    {
        int _itemcode;
        Cashcoin _cashcoin;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _cashcoin = new Cashcoin();
                ParseBase(_parser, _cashcoin);

                dic_Cashcoin.Add(_itemcode, _cashcoin);

				//_cashcoin.eathp = _parser.getInt("eathp");
				//_cashcoin.eatmp = _parser.getInt("eatmp");

				//_cashcoin.attbuff = _parser.getInt("attbuff");
				//_cashcoin.attTime100 = _parser.getInt("atttime100");
				//_cashcoin.defbuff = _parser.getInt("defbuff");
				//_cashcoin.defTime100 = _parser.getInt("deftime100");

				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_cashcoin.ToString());
            }
        }
    }

    #endregion

    #region gamecoin
    void ParseGameCoin(SSParser _parser)
    {
        int _itemcode;
        Gamecoin _gamecoin;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _gamecoin = new Gamecoin();
                ParseBase(_parser, _gamecoin);

                dic_Gamecoin.Add(_itemcode, _gamecoin);

				//_cashcoin.eathp = _parser.getInt("eathp");
				//_cashcoin.eatmp = _parser.getInt("eatmp");

				//_cashcoin.attbuff = _parser.getInt("attbuff");
				//_cashcoin.attTime100 = _parser.getInt("atttime100");
				//_cashcoin.defbuff = _parser.getInt("defbuff");
				//_cashcoin.defTime100 = _parser.getInt("deftime100");

				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_gamecoin.ToString());
            }
        }
    }

    #endregion

    #region staticinfo
    void ParseStaticinfo(SSParser _parser)
    {
        int _itemcode;
        Staticinfo _staticinfo;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _staticinfo = new Staticinfo();
                ParseBase(_parser, _staticinfo);

                dic_Staticinfo.Add(_itemcode, _staticinfo);

				//_cashcoin.eathp = _parser.getInt("eathp");
				//_cashcoin.eatmp = _parser.getInt("eatmp");

				//_cashcoin.attbuff = _parser.getInt("attbuff");
				//_cashcoin.attTime100 = _parser.getInt("atttime100");
				//_cashcoin.defbuff = _parser.getInt("defbuff");
				//_cashcoin.defTime100 = _parser.getInt("deftime100");

				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_staticinfo.ToString());
            }
        }
    }

    #endregion

    #region livelupreward
    void ParseLevelupreward(SSParser _parser)
    {
        int _itemcode;
        Levelupreward _livelupreward;
        while (_parser.next())
        {
            _itemcode = _parser.getInt("itemcode");
            if (!dic_ItemInfoBase.ContainsKey(_itemcode))
            {
                //base 부분을 함수로 만듬...
                _livelupreward = new Levelupreward();
                ParseBase(_parser, _livelupreward);

                dic_Levelupreward.Add(_itemcode, _livelupreward);

				//_cashcoin.eathp = _parser.getInt("eathp");
				//_cashcoin.eatmp = _parser.getInt("eatmp");

				//_cashcoin.attbuff = _parser.getInt("attbuff");
				//_cashcoin.attTime100 = _parser.getInt("atttime100");
				//_cashcoin.defbuff = _parser.getInt("defbuff");
				//_cashcoin.defTime100 = _parser.getInt("deftime100");

				if (Constant.DEBUG_ITEMINFOMANAGER) Debug.Log(_livelupreward.ToString());
            }
        }
    }

    #endregion
}

//public class ItemInfo
[System.Serializable]
public class ItemInfoBase
{
    public int itemcode;
    public int category;
    public int subcategory;
    public int equpslot;
    public string itemname;
    public int activate;
    public int toplist;
    public int grade;
    public int discount;
    public string icon;
    public int playerlv;
    public int multistate;
    public int gamecoin;
    public int cashcost;
    public int buyamount;
    public int sellcost;
    public string description;


    public override string ToString()
    {
        return "itemcode" + itemcode
            + " category" + category
            + " subcategory" + subcategory
            + " equpslot" + equpslot
            + " itemname" + itemname
            + " activate" + activate
            + " toplist" + toplist
            + " discount" + discount
            + " icon" + icon
            + " playerlv" + playerlv
            + " multistate" + multistate
            + " gamecost" + gamecoin
            + " cashcost" + cashcost
            + " buyamount" + buyamount
            + " sellcost" + sellcost
            + " description" + description;


    }
}

[System.Serializable]
public class WearPart : ItemInfoBase
{
    public int att;
    public int def;
    public int hp;
    public int attspeed100
    {
        get { return attspeed100_; }
        set
        {
            attspeed100_ = value;
            attspeed = value / 100f;
        }
    }        //10 > 0.10f
    private int attspeed100_;
    public float attspeed;
    public int movespeed100
    {
        get { return movespeed100_; }
        set
        {
            movespeed100_ = value;
            movespeed = value / 100f;
        }
    }       //10 > 0.10f
    private int movespeed100_;
    public float movespeed;


    public override string ToString()
    {
        return base.ToString()
            + " att:" + att
            + " def:" + def
            + " hp:" + hp
            + " attspeed100:" + attspeed100
            + " attspeed:" + attspeed
            + " movespeed100:" + movespeed100
            + " movespeed:" + movespeed;
    }
}

[System.Serializable]
public class Randombox : ItemInfoBase
{
    public int ritemcode1;
    public int ritemcode2;

    public override string ToString()
    {
        return base.ToString()
            + " ritemcode1:" + ritemcode1
            + " ritemcode2:" + ritemcode2;
    }
}

[System.Serializable]
public class Food : ItemInfoBase
{
    public int eathp;
    public int eatmp;

    public int attbuff;
    public int attTime100
    {
        get { return attTime100_; }
        set
        {
            attTime100_ = value;
            attTime = value / 100f;
        }
    }       // 10 > 0.10f
    private int attTime100_;
    public float attTime;

    public int defbuff;
    public int defTime100
    {
        get { return defTime100_; }
        set
        {
            defTime100_ = value;
            defTime = value / 100f;
        }
    }       // 10 > 0.10f
    private int defTime100_;
    public float defTime;

    public override string ToString()
    {
        return base.ToString()
            + " eathp:" + eathp
            + " eatmp:" + eatmp
            + " attbuff:" + attbuff
            + " attTime100:" + attTime100
            + " attTime:" + attTime
            + " defbuff:" + defbuff
            + " defTime100:" + defTime100
            + " defTime:" + defTime;
    }
}

[System.Serializable]
public class Cashcoin : ItemInfoBase
{

}

[System.Serializable]
public class Gamecoin : ItemInfoBase
{

}

[System.Serializable]
public class Staticinfo : ItemInfoBase
{

}

[System.Serializable]
public class Levelupreward : ItemInfoBase
{

}