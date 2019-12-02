using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
public class Move : MonoBehaviour
{
    //动画的字段
    public float animSpeed = 1.5f;				
	public float lookSmoother = 3.0f;			
	public bool useCurves = true;				
	public float useCurvesHeight = 0.5f;		
	public float forwardSpeed = 7.0f;
	public float backwardSpeed = 2.0f;
	public float rotateSpeed = 2.0f;
	public float jumpPower = 3.0f; 
	private CapsuleCollider col;
	private Rigidbody rb;
    private Transform trans;
	private Vector3 velocity;
	private float orgColHight;
	private Vector3 orgVectColCenter;
	private Animator anim;						
	private AnimatorStateInfo currentBaseState;

    public int sendMsgNum = 0;
    public int recvMsgNum = 0;

    static int idle_cState = Animator.StringToHash("Base Layer.Idle_C");
	static int idle_aState = Animator.StringToHash("Base Layer.Idle_A");
	static int locoState = Animator.StringToHash("Base Layer.Locomotion");
	static int jumpState = Animator.StringToHash("Base Layer.Jump");
	static int cute1State = Animator.StringToHash("Base Layer.Cute1");

    static int attack = Animator.StringToHash("Base Layer.Attack");


    public GameObject prefab;


    void Awake()
    {
        // Connect();
        NetManager2.Connect();
        //请求其他玩家列表
        float x = UnityEngine.Random.Range(0, 30);
        float y = 0.5f;
        float z = UnityEngine.Random.Range(0, 30);
        Vector3 pos = new Vector3(x, y, z);

        Vector3 rot = new Vector3(0,0,0);
        
        AddPlayer(NetManager2.id,pos,rot);

        SendTrans();
    }

    void Start(){
        anim = NetManager2.players[NetManager2.id].GetComponent<Animator>(); //players[id].GetComponent<Animator>();
		col = NetManager2.players[NetManager2.id].GetComponent<CapsuleCollider>();
		rb = NetManager2.players[NetManager2.id].GetComponent<Rigidbody>();
        trans = NetManager2.players[NetManager2.id].GetComponent<Transform>();
		orgColHight = col.height;
		orgVectColCenter = col.center;
    }

    private void SendTrans(){
        GameObject player = NetManager2.players[NetManager2.id];// players[id];
        Vector3 pos = player.transform.position;
        Vector3 rot = player.transform.eulerAngles;
        string str = "TRANS%";
        str += NetManager2.id + "%";
        str += pos.x.ToString() + "%";
        str += pos.y.ToString() + "%";
        str += pos.z.ToString() + "%";
        str += rot.x.ToString() + "%";
        str += rot.y.ToString() + "%";
        str += rot.z.ToString();

        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        byte[] lenB = ArrayUtils.intToByteArray(bytes.Length);
        byte[] b3 = new byte[bytes.Length+lenB.Length];
        
        Buffer.BlockCopy(lenB,0,b3,0,lenB.Length);
        Buffer.BlockCopy(bytes,0,b3,lenB.Length,bytes.Length);
        // foreach(byte b in b3){
        //     print(b);
        // }
        
        // String s = System.Text.Encoding.Default.GetString (b3);
        if (NetManager2.socket != null && NetManager2.socket.Connected){
            NetManager2.socket.Send(b3);
            sendMsgNum++;
            // Debug.Log("发送" + str);
        }

        
    }


    private void SendLeave(){

        string str = "LEAVE%";
        str += NetManager2.id;

        byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
        byte[] lenB = ArrayUtils.intToByteArray(bytes.Length);
        byte[] b3 = new byte[bytes.Length+lenB.Length];

        Buffer.BlockCopy(lenB,0,b3,0,lenB.Length);
        Buffer.BlockCopy(bytes,0,b3,lenB.Length,bytes.Length);

        if (NetManager2.socket != null && NetManager2.socket.Connected){
            NetManager2.socket.Send(b3);
            sendMsgNum++;
            // Debug.Log("发送" + str);
        }
        
    }

    private void AddPlayer(string id, Vector3 position, Vector3 q){
        GameObject player = Instantiate(prefab,position,Quaternion.identity);
        player.transform.eulerAngles = q;
        // TextMesh textMesh = player.GetComponentInChildren<TextMesh>();
        // textMesh.text = id;
        NetManager2.players.Add(id, player);
        Debug.Log("玩家"+id+"进入游戏");
    }

    private void RemovePlayer(string id){
        if(NetManager2.players.ContainsKey(id)){
            Destroy(NetManager2.players[id]);
            NetManager2.players[id] = null;
        }

    }

    void Update()
    {
        
        for (int i = 0; i < NetManager2.msgList.Count; i++)
        {
            HandleMsg();
        }
        
        // PlayerCtrl();
    }

    private void HandleMsg(){
        if(NetManager2.msgList.Count<=0)return;
        string msg = NetManager2.msgList[0];
        // Debug.Log("接收"+msg);
        NetManager2.msgList.RemoveAt(0);
        string[] arr = msg.Split('%');
        // for(int i=0;i<arr.Length;i++){
        //     Debug.Log(arr[i]);
        // }
        switch(arr[0]){
            case "TRANS":OnRecvTrans(arr[1],arr[2],arr[3],arr[4],arr[5],arr[6],arr[7]);
            break;
            case "LEAVE":OnRecvLeave(arr[1]);
            break;
            case "ACTION":OnAction(arr[1],arr[2],arr[3],arr[4]);
            break;
        }

    }

    private void OnAction(string id,string h1, string v1, string jump1){
        if (id.Equals(NetManager2.id))return;
        // Debug.Log("其他玩家移动");
        float h = float.Parse(h1);				
		float v = float.Parse(v1);
        int jump = int.Parse(jump1);
        
        Rigidbody rb = NetManager2.players[id].GetComponent<Rigidbody>();
        Animator anim = NetManager2.players[id].GetComponent<Animator>();	
		anim.SetFloat("Speed", v);							
		anim.SetFloat("Direction", h); 						
		anim.speed = animSpeed;								
		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	
		rb.useGravity = true;
		
		
		
		Vector3 velocity = new Vector3(0, 0, v);
        Transform transform = NetManager2.players[id].GetComponent<Transform>();
		velocity = transform.TransformDirection(velocity);
		if (v > 0.1) {
			velocity *= forwardSpeed;	
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}
	
		

		
		if (jump == 1) {	
			if (currentBaseState.nameHash == locoState){
				if(!anim.IsInTransition(0))
				{
					rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("Jump", true);
				}
			}
		}
		
		transform.localPosition += velocity * Time.deltaTime;
		transform.Rotate(0, h * rotateSpeed, 0);	
		if (currentBaseState.nameHash == locoState){
			if(useCurves){
				resetCollider();
			}
		}
        CapsuleCollider col = NetManager2.players[id].GetComponent<CapsuleCollider>();
		if(currentBaseState.nameHash == jumpState)
		{
			if(!anim.IsInTransition(0))
			{
				
				if(useCurves){
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	
					
					Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);
						}
						else{
							
							resetCollider();
						}
					}
				}
				anim.SetBool("Jump", false);
			}
		}
		else if (currentBaseState.nameHash == idle_cState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (jump == 1) {
				anim.SetBool("Cute1", true);
			}

			
			
		}
		else if (currentBaseState.nameHash == idle_aState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (jump == 1) {
				anim.SetBool("Cute1", true);
			}

		}
    }

    private void OnRecvTrans(string id,string x, string y,string z,string xx, string yy,string zz){
        if (id.Equals(NetManager2.id))return;
        float x1 = float.Parse(x);
        float y1 = float.Parse(y);
        float z1 = float.Parse(z);
        
        float x2 = float.Parse(xx);
        float y2 = float.Parse(yy);
        float z2 = float.Parse(zz);
        Vector3 pos = new Vector3(x1,y1,z1);
        Vector3 rot = new Vector3(x2,y2,z2);
        if(NetManager2.players.ContainsKey(id)){
            Debug.Log("更新"+id+"位置");
            GameObject player = NetManager2.players[id];
            player.transform.position = pos;
            player.transform.eulerAngles = rot;
        }else{
            AddPlayer(id,pos,rot);
            SendTrans();
        }
        
    }

    private void OnRecvLeave(string id){
        RemovePlayer(id);
    }

    // private void Connect(){
    //     //Socket
    //     socket = new Socket(AddressFamily.InterNetwork,
    //                              SocketType.Stream, ProtocolType.Tcp);
    //     //Connect
    //     socket.Connect("154.8.172.144", 8090);
    //     id = socket.LocalEndPoint.ToString();
    //     Debug.Log("id="+id);
    //     //Recv
    //     socket.BeginReceive(readBuff, buffCount, buffRemain, SocketFlags.None, ReceiveCb, null);
    //     Debug.Log("connect succ!");
    // }

    // private void ReceiveCb(IAsyncResult ar)
    // {
    //     try
    //     {
    //         int count = socket.EndReceive(ar);
    //         ccc = count;
    //         buffCount+=count;
    //         ProcessData();
    //         buffRemain = BUFFER_SIZE - buffCount;
    //         //继续接收	
    //         socket.BeginReceive(readBuff, buffCount, buffRemain, SocketFlags.None, ReceiveCb, null);
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.Log(e.StackTrace);
    //         // socket.Shutdown(SocketShutdown.Both);
    //         socket.Close();
    //     }
    // }

    // private void ProcessData(){
    //     if(buffCount < INT32)return;
    //     //数据处理
    //         ArrayUtils.copy(readBuff,lenBytes,INT32);
    //         msgLength = ArrayUtils.byteArrayToInt(lenBytes);

    //         if(msgLength + INT32<= buffCount){

    //             string msg = System.Text.Encoding.UTF8.GetString(readBuff, INT32, msgLength);
    //             NetManager2.msgList.Add(msg);
    //             recvMsgNum++;
    //             int count = buffCount - msgLength - INT32;
    //             ArrayUtils.copy(readBuff, INT32 + msgLength, readBuff, 0, count);
    //             buffCount = count;

    //             if(buffCount > 0){
    //                 ProcessData();
    //             }
    //         }
    // }

    public void PlayerCtrl(Vector2 moveV2){
        float h = moveV2.x;//Input.GetAxis("Horizontal");				
		float v = moveV2.y;//Input.GetAxis("Vertical");
        if(v>0){
            v = 1;
        }
		anim.SetFloat("Speed", v);							
		anim.SetFloat("Direction", h);
		
		anim.speed = animSpeed;
		rb.useGravity = true;						
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);	
		
		
		
		
		velocity = new Vector3(0, 0, v);
		velocity = trans.TransformDirection(velocity);
		if (v > 0.1) {
			velocity *= forwardSpeed;	
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}
	
		

		int jump = 0;
		if (Input.GetButtonDown("Jump")) {	
            jump = 1;
			if (currentBaseState.nameHash == locoState){
				if(!anim.IsInTransition(0))
				{
					rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool("Jump", true);
				}
			}
		}
		
		// Debug.Log(h+" "+rotateSpeed);
		trans.localPosition += velocity * Time.deltaTime;
		trans.Rotate(0, h * rotateSpeed, 0);	
		if (currentBaseState.nameHash == locoState){
			if(useCurves){
				resetCollider();
			}
		}
		if(currentBaseState.nameHash == jumpState)
		{
			if(!anim.IsInTransition(0))
			{
				
				if(useCurves){
					float jumpHeight = anim.GetFloat("JumpHeight");
					float gravityControl = anim.GetFloat("GravityControl"); 
					if(gravityControl > 0)
						rb.useGravity = false;	
					
					Ray ray = new Ray(trans.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit();
					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > useCurvesHeight)
						{
							col.height = orgColHight - jumpHeight;			
							float adjCenterY = orgVectColCenter.y + jumpHeight;
							col.center = new Vector3(0, adjCenterY, 0);
						}
						else{
							
							resetCollider();
						}
					}
				}
				anim.SetBool("Jump", false);
			}
		}
		

		
		else if (currentBaseState.nameHash == idle_cState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
                jump = 1;
			}

			
			
		}
		else if (currentBaseState.nameHash == idle_aState)
		{
			if(useCurves){
				resetCollider();
			}
			
			if (Input.GetButtonDown("Jump")) {
				anim.SetBool("Cute1", true);
                jump = 1;
			}

		}
		else if (currentBaseState.nameHash == cute1State)
		{
			
			if(!anim.IsInTransition(0))
			{
				anim.SetBool("Cute1", false);
			}
		}
        if(h != 0f || v != 0f ||jump != 0){
            SendAction(h,v,jump);
            SendTrans();
        }
        
    }

    private void SendAction(float h1, float v1, int jump1){
        string str = "ACTION%";
        str += NetManager2.id + "%";
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
        if (NetManager2.socket != null && NetManager2.socket.Connected){
            NetManager2.socket.Send(b3);
            sendMsgNum++;
            // Debug.Log("发送" + str);
        }
        
        
    }

    void resetCollider()
	{
		
		col.height = orgColHight;
		col.center = orgVectColCenter;
	}
    void OnApplicationQuit(){
        SendLeave();
        // socket.Shutdown(SocketShutdown.Both);
        NetManager2.socket.Close();
    }

    public void OnAttack(){
            if(currentBaseState.nameHash != attack){
                anim.SetTrigger("Atk");
            }
        
    }

}
