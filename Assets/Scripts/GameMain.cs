using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {
	public static GameObject pre;
	public static long playerId;

	private static Dictionary<long, GameObject> players = new Dictionary<long, GameObject>();

	// Use this for initialization
	void Start () {
		//初始化人物预设
		pre = ResManager.LoadPrefab("Yuka_mecanim 1");
		//初始化物品配置
		ItemsConfigManager.init();
		//网络监听
		NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);
		NetManager.AddMsgListener("MsgKick", OnMsgKick);
		NetManager.AddMsgListener("MsgSyncPlayer",OnMsgSyncPlayer);
		NetManager.AddMsgListener("MsgOutGame", OnMsgOutGame);
		NetManager.AddMsgListener("MsgAcAttack", OnMsgAcAttack);
		//初始化
		PanelManager.Init();
		//打开登陆面板
		PanelManager.Open<LoginPanel>();
		
	}

	public static void AddPlayer(long playerId1, GameObject player){
		if(players.ContainsKey(playerId1)){
			return;
		}
			players.Add(playerId1, player);
		
	}

	public static void RemovePlayer(long playerId1){
		if(players.ContainsKey(playerId1)){
				players.Remove(playerId1);
			
		}
	}
	
	//销毁gameobject
	public static void ClearPlayers(){
			foreach(GameObject g in players.Values){
				Destroy(g);
			}
			players.Clear();
		
	}
	// Update is called once per frame
	void Update () {
		NetManager.Update();
	}

	//关闭连接
	void OnConnectClose(string err){
		Debug.Log("断开连接");
	} 

	void OnMsgSyncPlayer(MsgBase msgBase){
		MsgSyncPlayer msg = (MsgSyncPlayer) msgBase;
		if(msg.playerId == playerId){
			return;
		}
		if(!players.ContainsKey(msg.playerId)){
			Vector3 pos = new Vector3(msg.x, msg.y, msg.z);
            Quaternion q = Quaternion.Euler(msg.ex, msg.ey, msg.ez);
            GameObject playerObj =  Instantiate(GameMain.pre, pos, q);
            AddPlayer(msg.playerId, playerObj);
			playerObj.AddComponent<PlayerNetCtrl1>();
		}
		players[msg.playerId].GetComponent<PlayerNetCtrl1>().SyncPos(msg);
		// players[msg.playerId].GetComponent<>();
	}

	void OnMsgAcAttack(MsgBase msgBase){
		MsgAcAttack msg = (MsgAcAttack) msgBase;
		if(msg.playerId == playerId){
			return;
		}

		players[msg.playerId].GetComponent<PlayerNetCtrl1>().SyncAction(msg);
	}

	void OnMsgOutGame(MsgBase msgBase){
		MsgOutGame msg = (MsgOutGame) msgBase;
		if(msg.playerId == playerId){
			return;
		}
		if(players!=null&&players.ContainsKey(msg.playerId)){
			GameObject pl = players[msg.playerId];
			Destroy(pl);
			players.Remove(msg.playerId);
		}
	}

	//被踢下线
	void OnMsgKick(MsgBase msgBase){
		PanelManager.Open<TipPanel>("被踢下线");
	}

	void OnApplicationQuit()
    {
		MsgOutGame msg = new MsgOutGame();
        NetManager.Send(msg);
    }
}
