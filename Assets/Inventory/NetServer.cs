using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
/// <summary>
///
/// </summary>
public class NetServer : MonoBehaviour
{
    const int BUFFER_SIZE = 1024;
    public int buffCount = 0;
    public int buffRemain = BUFFER_SIZE;
    const int INT32 = 4;
    public int msgLength = 0;

    public byte[] lenBytes = new byte[INT32];
    public byte[] readBuff = new byte[BUFFER_SIZE];
    [SerializeField]
    public string id;
    public Socket socket;
    //消息列表
    private List<string> msgList = new List<string>();
    // private Queue queue = new Queue();
    //玩家列表
    public Dictionary<string,GameObject> players = new Dictionary<string, GameObject>();


    void Awake(){
        Connect();
    }

    

    private void Connect(){
        //Socket
        socket = new Socket(AddressFamily.InterNetwork,
                                 SocketType.Stream, ProtocolType.Tcp);
        //Connect
        socket.Connect("127.0.0.1", 8090);
        id = socket.LocalEndPoint.ToString();
        Debug.Log("id="+id);
        //Recv
        socket.BeginReceive(readBuff, buffCount, buffRemain, SocketFlags.None, ReceiveCb, null);
        Debug.Log("connect succ!");
    }
    private void ReceiveCb(IAsyncResult ar)
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

    private void ProcessData(){
        if(buffCount < INT32)return;
        //数据处理
            ArrayUtils.copy(readBuff,lenBytes,INT32);
            msgLength = ArrayUtils.byteArrayToInt(lenBytes);

            if(msgLength + INT32<= buffCount){

                string msg = System.Text.Encoding.UTF8.GetString(readBuff, INT32, msgLength-1);
                msgList.Add(msg);
                int count = buffCount - msgLength - INT32;
                ArrayUtils.copy(readBuff, INT32 + msgLength, readBuff, 0, count);
                buffCount = count;

                if(buffCount > 0){
                    ProcessData();
                }
            }
    }

    private void HandleMsg(){
        if(msgList.Count<=0)return;
        string msg = msgList[0];
        msgList.RemoveAt(0);
        string[] arr = msg.Split('%');
        
        switch(arr[0]){
            
        }

    }

    public void sendMsg(string msg){
        
        msg += "\n";
        
        byte[] bytes = System.Text.Encoding.Default.GetBytes(msg);
        byte[] lenB = ArrayUtils.intToByteArray(bytes.Length);
        byte[] b3 = new byte[bytes.Length+lenB.Length];
        
        Buffer.BlockCopy(lenB,0,b3,0,lenB.Length);
        Buffer.BlockCopy(bytes,0,b3,lenB.Length,bytes.Length);
        
        if (socket != null && socket.Connected){
            socket.Send(b3);
        }
    }
}
