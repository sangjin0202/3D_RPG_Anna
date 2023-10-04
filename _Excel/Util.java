
import java.util.Random;

class Util{
	///////////////////////////////////////////////////
	// �ڹ��� �������� ���� �Ҷ� ��� => byte[4] 0 1 2 3
	///////////////////////////////////////////////////
	public static byte[] setJInt(int data){
		byte ret[] = new byte[4];
		ret[0] = (byte)(data >> 24);
		ret[1] = (byte)(data >> 16);
		ret[2] = (byte)(data >> 8);
		ret[3] = (byte)(data >> 0);
		return ret;
	}

    public static void setJInt(byte data[], int idx, int value){
    	data[idx    ] = (byte)(value >> 24 & 0xff);
    	data[idx + 1] = (byte)(value >> 16 & 0xff);
    	data[idx + 2] = (byte)(value >> 8  & 0xff);
        data[idx + 3] = (byte)(value  	   & 0xff);
    }

	//////////////////////////////////////////////////////////////
	//		�ڹ����¿��� ����Ÿ �б�
	//		PLTE Change CRC Check
	/////////////////////////////////////////////////////////////
	// 1. ĳ���� => ����Ʈ => ���� => ���� => ����
	// 2. ĳ���� => ���� => ����Ʈ => ���� => �ణ������ ���� ���÷ο� �߻�
    public static int getJInt(byte data[], int idx){
		return (   (int)data[idx] 	<< 24 & 0xFF000000)
				| ((int)data[idx+1] << 16 & 0x00FF0000)
				| ((int)data[idx+2] << 8  & 0x0000FF00)
				| ((int)data[idx+3]       & 0x000000FF);
    }

    //����Ÿ�� ������� RGB���� �� ���� ��� ����
	public static int getRGB(byte[] data, int idx){
		return (  (((int)data[idx + 0] << 16) & 0x00FF0000)
				| (((int)data[idx + 1] << 8 ) & 0x0000FF00)
				| (((int)data[idx + 2]      ) & 0x000000FF));
	}

	////////////////////////////////////////////////////////////////////
	//			C�� �ڷᱸ���� �����ϱ�
	///////////////////////////////////////////////////////////////////
	public static int setCInt(byte[] buf, int idx, int data){
		buf[idx + 3] = (byte)(data >> 24);
		buf[idx + 2] = (byte)(data >> 16);
		buf[idx + 1] = (byte)(data >> 8);
		buf[idx + 0] = (byte)(data >> 0);
		return idx + 4;
	}
	public static int setCShort(byte[] buf, int idx, int data){
		buf[idx + 1] = (byte)(data >> 8);
		buf[idx + 0] = (byte)(data >> 0);
		return idx + 2;
	}

	//////////////////////////////////////////////////////////////////////
	//			C => Java
	/////////////////////////////////////////////////////////////
	public static int getCByte(byte[] data, int idx){
		//return (((int)data[idx + 0]) & 0x000000FF);
		return (int)(data[idx]     & 0x000000FF);
	}

	public static short getCShort(byte[] data, int idx){
		return (short)((((int)data[idx + 1] << 8 ) & 0x0000FF00)
					 | (((int)data[idx + 0]      ) & 0x000000FF));
	}

	//���� ���۷��� ����
	public static int getCInt(byte[] data, int idx){
		// 1. ĳ���� => ����Ʈ => ���� => ���� => ����
		// 2. ĳ���� => ���� => ����Ʈ => ���� => �ణ������ ���� ���÷ο� �߻�
		return (  (((int)data[idx + 3] << 24) & 0xFF000000)
				| (((int)data[idx + 2] << 16) & 0x00FF0000)
				| (((int)data[idx + 1] << 8 ) & 0x0000FF00)
				| (((int)data[idx + 0]      ) & 0x000000FF));
	}

    public static String getCString(byte[] source, int index, int len){
    	int start = index, end = index + len;
    	//System.out.println(source[end - 1]);
    	if(len == 0){
    		//System.out.println("����");
    		return "";
    	}else if(source[end - 1] == 0){
    		index = end - 1;
    		//System.out.println("1 source["+index+"]:"+source[index]);
    	}else{
	    	for(; index < end; ++index){
	    		//if(Constant.DEBUG)
	    		//	System.out.println("2 source["+index+"]:"+source[index]);
	    		if(source[index] == 0)
	    			break;
	    	}
	    }
    	///if(Constant.DEBUG)
    		///System.out.println(start+":"+(index - start)+":"+(new String(source, start, (index - start))));
    	//return (new String(source, start, (index - start))).trim();
    	return (new String(source, start, (index - start)));
    }

    public static int setCString(byte _src[], byte[] _tar, int _index){
    	//������ + 0[�����ڿ�] => ������0 �̿� ���� �����Ѵ�.
    	byte _src2[] = new byte[_src.length + 1];
    	System.arraycopy(_src, 0, _src2, 0, _src.length);

    	System.arraycopy(_src2, 0, _tar, _index, _src2.length);
    	return _index + _src2.length;
    }

	/*
	///////////////////////////////////////////////////
	//		300000 => 300,000
	///////////////////////////////////////////////////
	public static String displayMoney(String str){
		String tmp = "";
		int size = str.length();
		do{
			tmp = str.substring(size-3,size)+","+tmp;
			str = str.substring(0, size-3);
		}while(size > 3);
		return (str+","+tmp.substring(0, tmp.length()-1));
	}
	/**/

	////////////////////////////////////////////////////
	//		//(0,4) => 0,1,2,3 ���� ���´�.
	//(����)	���ӿ��� �ٷ� ȣ���ϸ� ���� ��ȣ�� ���� ���ɼ��� ����.
	////////////////////////////////////////////////////
	public static int random(int min, int max){
		if(min == max){
			//0 , 0 ������ �������ϱ� ���� �ʰ� �����.
			max+=1;
		}else if(min > max){
			//���� ���� �� ũ�� �����Ѵ�.
			// 0 , -1
			int tmp = max;
			max = min;
			min = tmp;
		}
		Random random = new Random();
		return Math.abs(random.nextInt())%(max - min) + min;
	}

	//���ڿ� Ư���ϰ� �и��ϱ�
	//replace("hi\n\n", "\n", ""+'\n', 2);
	public static String replace(String _src, String _matchStr, String _toChange, int _size){
		if(Constant.DEBUG)
			System.out.println("_src:" + _src
								+"\n _matchStr:" +_matchStr
								+"\n _toChange:" +_toChange
								+"\n _size:" +_size);
		String tmpStr = "", tmpStr2 = "", tmpStr3 = "";
		int index, index2, _len = _matchStr.length();
		while(_src.length() != 0){
			index = _src.indexOf(_matchStr);
			//System.out.println("index:"+index);
			if(index < 0){
				tmpStr += _src;
				_src = "";
			}else{
				//tmpStr2 = _src.substring(0, index);
				//while(tmpStr2.length() != 0){
				//	if(tmpStr2.length() > _size){
				//		tmpStr3 =tmpStr3 + tmpStr2.substring(0,_size)+_toChange;
				//		tmpStr2 = tmpStr2.substring(_size);
				//	}else{
				//		tmpStr3 +=tmpStr2;//_size = 4
				//		tmpStr2 = "";
				//	}
				//}
				tmpStr += _src.substring(0, index);
				_src = _src.substring(index + _matchStr.length());
				tmpStr += _toChange;
			}
		}
		return tmpStr;
	}

	//===================================================================
	public static String makeQuery(String _str){
		return "'" + _str + "'";
	}

	//===================================================================
	//��ȯ��
	public static boolean isNumber(String _str){
		boolean _bFind = false;
		int _si = _str.indexOf("(");
		int _ei = _str.indexOf(")");
		if(_si >= 0 && _ei >= 0 && _si < _ei){
			_bFind = true;
		}

		char _word = _str.charAt(0);
		if(_word >= '0' && _word <= '9' || _word == '-'){
			_bFind = true;
		}

		return _bFind;
	}

	public static int parseInt(String _str, String _comment){
		int _ri = -1;
		int _startIdx = -1;
		int _endIdx = -1;
		//System.out.println(_str);

		if(_str.indexOf("(") >= 0 && _str.indexOf(")") >= 0){
			//()�� ���ؼ� ��������
			//(-1), (0), (1), ....
			if(_str.indexOf("(+") >= 0)
				_startIdx = _str.indexOf("(+")+1;
			else
				_startIdx = _str.indexOf("(");
			_endIdx = _str.indexOf(")");
			if(_startIdx >= 0){
				//System.out.println("����:" + _str);
				_str = _str.substring(_startIdx + 1, _endIdx);
				//System.out.println("��ȯ:" + _str);
			}
		}else if(_str.indexOf("Q") >= 0){
			//Q��ũ�� �ִ°�
			//Q11, Q22, Q13, ...
			_startIdx = _str.indexOf("Q");
			_endIdx = _str.length();
			if(_startIdx >= 0){
				//System.out.println("����:" + _str);
				_str = _str.substring(_startIdx + 1, _endIdx);
				//System.out.println("��ȯ:" + _str);
			}
		}else{
			//�������� ���� => �н�
			//... -1, 0, +1, ....
		}

		try{
			_ri = Integer.parseInt(_str);
		}catch(Exception e){
			System.out.println("#### "+_comment+" => ���� -> �����κ�ȯ�ȵ� [" + _str + "]");
		}
		return _ri;
	}

	public static String getStringInt(String _str){
		String _strOriginal = _str;
		int _startIdx = -1;
		int _endIdx = -1;
		//System.out.println(_str);

		if(_str.indexOf("(") >= 0 && _str.indexOf(")") >= 0){
			//()�� ���ؼ� ��������
			//(-1), (0), (1), ....
			if(_str.indexOf("(+") >= 0)
				_startIdx = _str.indexOf("(+")+1;
			else
				_startIdx = _str.indexOf("(");
			_endIdx = _str.indexOf(")");
			if(_startIdx >= 0){
				//System.out.println("����:" + _str);
				_str = _str.substring(_startIdx + 1, _endIdx);
				//System.out.println("��ȯ:" + _str);
			}
		//}else if(_str.indexOf("Q") >= 0){
		//	//Q��ũ�� �ִ°�
		//	//Q11, Q22, Q13, ...
		//	_startIdx = _str.indexOf("Q");
		//	_endIdx = _str.length();
		//	if(_startIdx >= 0){
		//		//System.out.println("����:" + _str);
		//		_str = _str.substring(_startIdx + 1, _endIdx);
		//		//System.out.println("��ȯ:" + _str);
		//	}
		}else{
			//�������� ���� => �н�
			//... -1, 0, +1, ....
		}

		try{
			int _ri = Integer.parseInt(_str);
		}catch(Exception e){
			_str = _strOriginal;
		}

		return _str;
	}

	public static String getBraceString(String _str){
		int _startIdx = -1;
		int _endIdx = -1;

		if(_str.indexOf("(") >= 0 && _str.indexOf(")") >= 0){
			//()�� ���ؼ� ��������
			//(-1), (0), (1), ....
			if(_str.indexOf("(+") >= 0)
				_startIdx = _str.indexOf("(+")+1;
			else
				_startIdx = _str.indexOf("(");
			_endIdx = _str.indexOf(")");
			if(_startIdx >= 0){
				//System.out.println("����:" + _str);
				_str = _str.substring(_startIdx + 1, _endIdx);
				//System.out.println("��ȯ:" + _str);
			}
		}else{
			//�������� ���� => �н�
			//... -1, 0, +1, ....
		}
		return _str;
	}


	/*
	public static int parseInequality(String _str){
		int _ri = 3;
		if(_str.equals("==")){
			//System.out.println(_str + " : ==");
			_ri = 3;
		}else if(_str.equals(">")){
			//System.out.println(_str + " : >");
			_ri = 1;
		}else if(_str.equals(">=")){
			//System.out.println(_str + " : >=");
			_ri = 2;
		}else if(_str.equals("<=")){
			//System.out.println(_str + " : <=");
			_ri = 4;
		}else if(_str.equals("<")){
			//System.out.println(_str + " : <");
			_ri = 5;
		}else if(_str.equals("!=")){
			//System.out.println(_str + " : !=");
			_ri = 6;
		}
		return _ri;
	}

	public static String parseInequality2(byte _kind){
		String _str = "";
		switch(_kind){
			case 3:
				_str = "==(3)";
				break;
			case 1:
				_str = ">(1)";
				break;
			case 2:
				_str = ">=(2)";
				break;
			case 4:
				_str = "<=(4)";
				break;
			case 5:
				_str = "<(5)";
				break;
			case 6:
				_str = "!=(6)";
				break;
		}
		return _str;
	}
	/**/

	//��¿�
	public static void debug(byte str){
		debug(""+str);
	}
	public static void debug(short str){
		debug(""+str);
	}
	public static void debug(int str){
		debug(""+str);
	}
	public static void debug(String str){
		System.out.println(str);
	}
	public void printHex(byte data){
		System.out.print(toHexa(data));
	}
	public void printHex2(byte data){
		System.out.print(toHexa(data)+" ");
	}
	public String byteToHexa(byte data){
		StringBuffer buf = new StringBuffer(Integer.toHexString((int)data & 0x000000FF));
		int len = buf.length();
		for(int i = 0 ; i < 2 - len ; i++)
			buf.insert(0,'0');
		return "0x"+buf.toString();
	}
	public String shortToHexa(short data){
		StringBuffer buf = new StringBuffer(Integer.toHexString((int)data & 0x0000FFFF));
		int len = buf.length();
		for(int i = 0 ; i < 4 - len ; i++)
			buf.insert(0,'0');
		return "0x"+buf.toString();
	}
	public String intToHexa(int data){
		StringBuffer buf = new StringBuffer(Integer.toHexString(data));
		int len = buf.length();
		for(int i = 0 ; i < 8 - len ; i++)
			buf.insert(0,'0');
		return "0x"+buf.toString();
	}

	public String toHexa(byte data){
		StringBuffer buf = new StringBuffer(Integer.toHexString((int)data & 0x000000FF));
		int len = buf.length();
		for(int i = 0 ; i < 2 - len ; i++)
			buf.insert(0,'0');
		//return "0x"+buf.toString();
		return buf.toString();
	}
	public char toAscii(byte data){
		return (char)data;
	}
}
