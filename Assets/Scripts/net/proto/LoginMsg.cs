//注册
public class MsgRegister:MsgBase {
	public MsgRegister() {protoName = "MsgRegister";}
	//客户端发
	public string id = "";
	public string pw = "";
	//服务端回（0-成功，1-失败）
	public int result = 0;
}


//登陆
public class MsgLogin:MsgBase {
	public MsgLogin() {protoName = "MsgLogin";}
	//客户端发
	public string id = "";
	public string pw = "";
	//服务端回（0-成功，1-失败）
	public int result = 0;
	public PlayerSimpleInfo[] playerSimpleInfos;
}

//房间信息
[System.Serializable]
public class PlayerSimpleInfo{
	public long playerId = 0;		//角色id
}

[System.Serializable]
public class PlayerEntity{
	public long playerId;
    public long userId;
    public string playerName;
    public long coin;
    public long gem;
    public long exp;
    public int level;
    public string scene;
    public int areaId;
    public float x;
    public float y;
    public float z;
    public float ex;
    public float ey;
    public float ez;
    public long hp;
    public long mp;
    public long maxHp;
    public long maxMp;
}
public class MsgCreatePlayer:MsgBase{
	public string playerName;
    public long playerId;
    public int result = 0;
    public PlayerEntity playerEntity;
	public MsgCreatePlayer(){
		protoName = "MsgCreatePlayer";
	}
}

[System.Serializable]
public class ItemEntity{
	public long id;
    public long playerId;
    public int itemId;
    public int num;
    public string attribute;
}
public class MsgEnterGame:MsgBase{
	public string playerName;
    public long playerId;
    public int result = 0;
    public PlayerEntity[] playerEntities;
	public ItemEntity[] items;
	public MsgEnterGame(){
		protoName = "MsgEnterGame";
	}
}

public class MsgOutGame : MsgBase {
    public long playerId;
    public MsgOutGame(){
        protoName = "MsgOutGame";
    }

}

//踢下线（服务端推送）
public class MsgKick:MsgBase {
	public MsgKick() {protoName = "MsgKick";}
	//原因（0-其他人登陆同一账号）
	public int reason = 0;
}

// public class MsgEnterGame:MsgBase {
// 	public MsgEnterGame() {protoName = "MsgEnterGame";}
// 	public long playerId;
// 	//服务端回（0-成功，1-失败）
// 	public int result = 0;
// }