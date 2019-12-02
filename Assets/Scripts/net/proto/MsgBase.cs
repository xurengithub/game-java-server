using System;
using UnityEngine;
using System.Linq;

public class MsgBase{
	public string protoName = "null";
	//编码
	public static byte[] Encode(MsgBase msgBase){
		string s = JsonUtility.ToJson(msgBase); 
		return System.Text.Encoding.UTF8.GetBytes(s);
	}

	//解码
	public static MsgBase Decode(string protoName, byte[] bytes, int offset, int count){
		string s = System.Text.Encoding.UTF8.GetString(bytes, offset, count);
		Debug.Log("Decode: "+s);
		MsgBase msgBase = (MsgBase)JsonUtility.FromJson(s, Type.GetType(protoName));
		return msgBase;
	}

	//编码协议名（2字节长度+字符串）
	public static byte[] EncodeName(MsgBase msgBase){
		//名字bytes和长度
		byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(msgBase.protoName);
		int len = nameBytes.Length;
		byte[] lenBytes = ArrayUtils.intToByteArray(len);
		
		// Int16 len = (Int16)nameBytes.Length;
		// //申请bytes数值
		byte[] bytes = new byte[4+len];
		for(int i = 0; i < 4; i++){
			bytes[i] = lenBytes[i];
		}
		// //组装2字节的长度信息
		// bytes[0] = (byte)(len%256);
		// bytes[1] = (byte)(len/256);
		//组装名字bytes
		Array.Copy(nameBytes, 0, bytes, 4, len);

		return bytes;
	}

	//解码协议名（2字节长度+字符串）
	public static string DecodeName(byte[] bytes, int offset, out int count){
		count = 0;
		//必须大于2字节
		if(offset + 4 > bytes.Length){
			return "";
		}
		//读取长度
		// Int16 len = (Int16)((bytes[offset+1] << 8 )| bytes[offset] );
		byte[] lenBytes = new byte[4];
		ArrayUtils.copy(bytes,offset,lenBytes,0,4);
		int len = ArrayUtils.byteArrayToInt(lenBytes);
		//长度必须足够
		if(offset + 4 + len > bytes.Length){
			return "";
		}
		//解析
		count = 4+len;
		string name = System.Text.Encoding.UTF8.GetString(bytes, offset+4, len);
		return name;
	}
}


