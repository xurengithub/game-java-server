using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
/// <summary>
///
/// </summary>
public static class NetManager2
{
    const int BUFFER_SIZE = 1024;
    public static int buffCount = 0;
    public static int buffRemain = BUFFER_SIZE;
    const int INT32 = 4;
    public static int msgLength = 0;

    public static byte[] lenBytes = new byte[INT32];
    public static byte[] readBuff = new byte[BUFFER_SIZE];
    [SerializeField]
    public static string id;
    public static Socket socket;
    //消息列表
    public static List<string> msgList = new List<string>();
    private static Queue queue = new Queue();
    //玩家列表
    public static Dictionary<string,GameObject> players = new Dictionary<string, GameObject>();

    public static void Connect(){
        //Socket
        socket = new Socket(AddressFamily.InterNetwork,
                                 SocketType.Stream, ProtocolType.Tcp);
        //Connect
        socket.Connect("154.8.172.144", 8090);
        id = socket.LocalEndPoint.ToString();
        Debug.Log("id="+id);
        //Recv
        socket.BeginReceive(readBuff, buffCount, buffRemain, SocketFlags.None, ReceiveCb, null);
        Debug.Log("connect succ!");
    }

    private static void ReceiveCb(IAsyncResult ar)
    {
        try
        {
            int count = socket.EndReceive(ar);
            buffCount+=count;
            ProcessData();
            buffRemain = BUFFER_SIZE - buffCount;
            //继续接收	
            socket.BeginReceive(readBuff, buffCount, buffRemain, SocketFlags.None, ReceiveCb, null);
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
            // socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }

    private static void ProcessData(){
        if(buffCount < INT32)return;
        //数据处理
            ArrayUtils.copy(readBuff,lenBytes,INT32);
            msgLength = ArrayUtils.byteArrayToInt(lenBytes);

            if(msgLength + INT32<= buffCount){

                string msg = System.Text.Encoding.UTF8.GetString(readBuff, INT32, msgLength);
                msgList.Add(msg);
                int count = buffCount - msgLength - INT32;
                ArrayUtils.copy(readBuff, INT32 + msgLength, readBuff, 0, count);
                buffCount = count;

                if(buffCount > 0){
                    ProcessData();
                }
            }
    }

    public static void SendAction(float h1, float v1, int jump1){
        string str = "ACTION%";
        str += id + "%";
        str += h1 + "%";
        str += v1 + "%";
        str += jump1;

        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        byte[] lenB = ArrayUtils.intToByteArray(bytes.Length);
        byte[] b3 = new byte[bytes.Length+lenB.Length];
        
        Buffer.BlockCopy(lenB,0,b3,0,lenB.Length);
        Buffer.BlockCopy(bytes,0,b3,lenB.Length,bytes.Length);
        // foreach(byte b in b3){
        //     print(b);
        // }
        
        // String s = System.Text.Encoding.Default.GetString (b3);
        if (socket != null && socket.Connected){
            socket.Send(b3);
            // Debug.Log("发送" + str);
        }
        
        
    }

    public static void Send(byte[] bytes){
        socket.Send(bytes);
    }
}
