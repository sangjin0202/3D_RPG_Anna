/////////////////////////////////////////
//   2013-07-22      : NGUI Object Destroy
//   2013-07-26      : getRandSerial(Ticks이용)
//                 encoding <=> decoding
/////////////////////////////////////////

//#define DEBUG_ON
using UnityEngine;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

public class SSUtil
{
    public static System.Text.Encoding enc = System.Text.Encoding.ASCII;
    public static string strkey = "secret8";



    // read xml, txt file(Resources)
    public static string load(string _file)
    {
#if DEBUG_ON
         Debug.Log("SSUtil load _file:" + _file);
#endif

        //1. file read and return
        TextAsset _textAsset = (TextAsset)Resources.Load(_file);
        return _textAsset.text;
    }

    //jar(Resources folder xx.bytes) image to out file
    public static string getResourcesBytesToOutFile(string _filename)
    {
        string _imagePath = Application.persistentDataPath + "/" + _filename + ".png";
        if (!System.IO.File.Exists(_imagePath))
        {
            TextAsset _w = Resources.Load(_filename) as TextAsset;
            if (_w.bytes != null)
            {
                System.IO.FileStream _fs = new System.IO.FileStream(_imagePath, System.IO.FileMode.Create);
                _fs.Write(_w.bytes, 0, _w.bytes.Length);
                _fs.Close();
            }
        }
        return _imagePath;
    }

    //capture screen shot to out file.
    public static string captureScreenShotToOutFile(string _filename)
    {
        _filename = _filename + ".png";
        string _imagePath = Application.persistentDataPath + "/" + _filename;
        ScreenCapture.CaptureScreenshot(_filename);
        return _imagePath;
    }


    /// ////////////////////////////////////////////////////////////////////
    //_strName      : /tmp_1366364214754.png
    //_strURL      : http://images.earthcam.com/ec_metros/ourcams/tmp_1366364214754.png
    //_strPathFolder:
    //   Android        /mnt/sdcard/Android/data/com.sangsangdigital.farmtycoongg/files
    //   PC           C:/Documents and Settings/Administrator/Local Settings/Application Data/SangSangDigital/wwwwebhandler
    //_strPathFile   :
    //   Android        /mnt/sdcard/Android/data/com.sangsangdigital.farmtycoongg/files/tmp_1366364214754.png
    //   PC           C:/Documents and Settings/Administrator/Local Settings/Application Data/SangSangDigital/wwwwebhandler/tmp_1366364214754.png
    /// ////////////////////////////////////////////////////////////////////
    public static string getDataPath(string _strName)
    {
        return Application.persistentDataPath + "/" + _strName;
    }

    public static string getDataPathProtocol(string _strName)
    {
#if UNITY_EDITOR
        _strName = "file://c://" + Application.persistentDataPath + "/" + _strName;
#elif UNITY_ANDROID || UNITY_IPHONE
         _strName = "file://" + Application.persistentDataPath + "/" + _strName;
#elif UNITY_WEBPLAYER
         _strName = _strURL;
#else
         _strName = "file://c://" + Application.persistentDataPath + "/" + _strName;
#endif


        return _strName;
    }


    // 패스워드 암호화.
    //public static string getGuestPassword(){
    //   string _password = "";
    //   /
    //   for(int i = 0; i < 8; i++){
    //      if(i%2 == 0){
    //         _password += Random.Range(0, 10);
    //      }else{
    //         _password += (char)(97 + Random.Range(0, 25));
    //      }
    //   }
    //   return _password;
    //}

    ////////////////////////////////////////////////////////////////////////////////////////////
    public static int getInt(string _param)
    {

        if (_param == null)
        {
            return -1;
        }
        else
        {
            int _rtn = -1;
            try
            {
                _rtn = System.Convert.ToInt32(_param);
            }
            catch (System.FormatException e)
            {
                //#if DEBUG_ON
                Debug.LogError("#### SSUtil getInt error(" + _param + "):" + e);
                //#endif
            }
            return _rtn;
        }
    }

    public static int getInt(string _param, int _startIdx, int _len)
    {
        if (_param == null)
        {
            return -1;
        }
        else if (_param.Length < _startIdx + _len)
        {
            return -1;
        }
        else
        {
            int _rtn = -1;
            try
            {
                _rtn = System.Convert.ToInt32(_param.Substring(_startIdx, _len));
            }
            catch (System.FormatException e)
            {
                //#if DEBUG_ON
                Debug.LogError("#### SSUtil getInt error(" + _param + "):" + e);
                //#endif
            }
            return _rtn;
        }
    }

    public static long getLong(string _param)
    {
        if (_param == null)
        {
            return -1;
        }
        else
        {
            long _rtn = -1;
            try
            {
                _rtn = System.Convert.ToInt64(_param);
            }
            catch (System.FormatException e)
            {
                //#if DEBUG_ON
                Debug.LogError("#### SSUtil getInt error(" + _param + "):" + e);
                //#endif
            }
            return _rtn;
        }
    }

    public static string getString(byte[] _b)
    {
        return enc.GetString(_b);
    }

    public static bool isURL(string _url)
    {

        if (_url == null)
        {
            return false;
        }

        _url = _url.Trim();

        if (_url.IndexOf("http://") == 0)
            return true;
        else if (_url.IndexOf("https://") == 0)
            return true;
        else
            return false;
    }

    public static string getFilenameFromURL(string _url)
    {
        string[] _tokens = _url.Split('/');
        return "/" + _tokens[_tokens.Length - 1];
    }

    //고유의  시리얼 키를 얻어오기.
    public static string getRandSerial()
    {
        string _str = getDateTimeTicks();
        char _rand, _rand2;

        for (int i = 0; i < 4; i++)
        {
            _rand = (char)('0' + Random.Range(0, 9));
            _rand2 = (char)('A' + Random.Range(0, 26));
            _str = _str.Replace(_rand, _rand2);
        }

        return _str;
    }

    private static string getDateTimeTicks()
    {
        return System.DateTime.Now.Ticks.ToString();
    }


    //3. 시간차 구하기.
    // 어떤 시간이 넣으면 현재 시간을 지나갔는가?
    // true    : 지정 시간지남.   > 일반모드.
    // false    : 아직 시간남음.    > x2모드.
    public static bool isPassedDate(string _strStartDate)
    {
        System.DateTime _dtPoint = System.DateTime.Parse(_strStartDate);
        System.DateTime _dtNow = System.DateTime.Now;
        System.TimeSpan _sp = _dtNow - _dtPoint;

        return _sp.Milliseconds < 0 ? true : false;
    }

}