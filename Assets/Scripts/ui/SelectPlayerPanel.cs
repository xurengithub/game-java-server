using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using HedgehogTeam.EasyTouch;
/// <summary>
///
/// </summary>
public class SelectPlayerPanel : BasePanel
{
    public static UnityAction<ItemEntity[]> synItems;
    public static UnityAction<PlayerEntity> synProperties;
    // public static UnityAction<PlayerEntity[]> synDownAllPlayersPos;
    // Start is called before the first frame update
    private long playerId;

    private Button createBtn;
    private Button enterBtn;

    private Transform toggleGroupTrans;
    private ToggleGroup toggleGroup;

    private List<Toggle> toggles;

    private List<long> playerIds;

    private GameObject togglePre;
    public override void OnInit() {
		skinPath = "SelectPlayerPanel";
		layer = PanelManager.Layer.Panel;
	}

	//显示
	public override void OnShow(params object[] args) {
        togglePre = ResManager.LoadPrefab("Toggle");
        toggles = new List<Toggle>();
		//寻找组件
		createBtn = skin.transform.Find("CreatePlayerBtn").GetComponent<Button>();
        enterBtn = skin.transform.Find("EnterGameBtn").GetComponent<Button>();
        toggleGroupTrans = skin.transform.Find("PlayersList");
        toggleGroup = toggleGroupTrans.GetComponent<ToggleGroup>();
        // toggles = toggleGroupTrans.GetComponentsInParent<Toggle>();
        
        createBtn.onClick.AddListener(OnCreateClick);
        enterBtn.onClick.AddListener(OnEnterClick);

        NetManager.AddMsgListener("MsgEnterGame", OnMsgEnterGame);
        NetManager.AddMsgListener("MsgCreatePlayer",OnMsgCreatePlayer);
        LoginPanel.playersDataEnter += playersDataEnter;
	}

	//关闭
	public override void OnClose() {
		//网络协议监听
		NetManager.RemoveMsgListener("MsgEnterGame", OnMsgEnterGame);
        NetManager.RemoveMsgListener("MsgCreatePlayer",OnMsgCreatePlayer);
        LoginPanel.playersDataEnter -= playersDataEnter;
	}

    public void OnMsgEnterGame(MsgBase msgBase){
        MsgEnterGame msg = (MsgEnterGame)msgBase;
        if(msg.result == 0){
            //载入游戏数据
            GameObject.FindGameObjectWithTag("Canvas").AddComponent<UIController>().init();
            GameMain.playerId = msg.playerId;
            //获取同区域所有玩家位置信息
            NewPlayerObj(msg.playerEntities);
            synItems(msg.items);
            // for(int i = 0; i < msg.playerEntities.Length; i++){
                
            //     if(msg.playerEntities[i].playerId == msg.playerId){
            //         synProperties(msg.playerEntities[i]);
            //     }
            // }
            
            Close();
        }
        
    }

    public void OnMsgCreatePlayer(MsgBase msgBase){
        MsgCreatePlayer msg = (MsgCreatePlayer)msgBase;
        if(msg.result == 0){
            // msg.playerEntity;
            //载入游戏数据然后才close
            Debug.Log("playerId "+msg.playerName+" enterGame");
            // GameObject.FindGameObjectWithTag("Canvas").AddComponent<UIController>().init();
            PanelManager.GameStartPanelInit();
            synProperties(msg.playerEntity);
            Close();
        }
    }

    
    public void OnCreateClick(){
        MsgCreatePlayer msg = new MsgCreatePlayer();
        NetManager.Send(msg);
    }

    public void OnEnterClick(){
        foreach(var toggle in toggles){
            if(toggle.isOn){
                Text t = toggle.GetComponentInChildren<Text>();
                long playerId = long.Parse(t.text);

                MsgEnterGame msgEnterGame = new MsgEnterGame();
                msgEnterGame.playerId = playerId;
                NetManager.Send(msgEnterGame);
            }
        }
        
    }
    

    private void playersDataEnter(PlayerSimpleInfo[] infos){
        foreach(var p in infos){
            Toggle tog = Instantiate(togglePre,toggleGroupTrans).GetComponent<Toggle>();
            toggles.Add(tog);
            tog.group = toggleGroup;
            // toggleGroup.RegisterToggle(tog);
            Text t = tog.GetComponentInChildren<Text>();
            t.text = p.playerId.ToString();
        }
    }

    private void NewPlayerObj(PlayerEntity[] playerEntities){
        foreach(PlayerEntity entity in playerEntities){
            Vector3 pos = new Vector3(entity.x, entity.y, entity.z);
            Quaternion q = Quaternion.Euler(entity.ex, entity.ey, entity.ez);
            GameObject playerObj =  Instantiate(GameMain.pre, pos, q);
            GameMain.AddPlayer(entity.playerId, playerObj);
            // GameMain.players.Add(entity.playerId, playerObj);
            if(entity.playerId == GameMain.playerId){//主角增加控制和资产
                InitCtrl(playerObj);
                synProperties(entity);
                //初始化hpmp
                playerObj.GetComponent<PlayerAtt>().Init(entity.hp, entity.maxHp, entity.mp, entity.maxMp);
            }else{//其他玩家由网络驱动
                playerObj.AddComponent<PlayerNetCtrl1>();
            }
        }
    }

    private void InitCtrl(GameObject playerObj){
        // testMove move = playerObj.GetComponent<testMove>();

        // ETCJoystick j = ETCInput.GetControlJoystick("MyJoystick");
        // // ETCTouchPad t = ETCInput.GetControlTouchPad("TouchPad");

        // ETCButton b2 = ETCInput.GetControlButton("AttackBtn");
        // b2.onDown.AddListener(move.OnAttack);
        // j.onMove.AddListener(move.OnMove);
        // j.onMoveEnd.AddListener(move.OnMoveEnd);
        PlayerCtrl PlayerCtrl = playerObj.AddComponent<PlayerCtrl>();
        ETCJoystick j = ETCInput.GetControlJoystick("MyJoystick");
        // ETCTouchPad t = ETCInput.GetControlTouchPad("TouchPad");

        ETCButton b2 = ETCInput.GetControlButton("AttackBtn");
        b2.onDown.AddListener(PlayerCtrl.OnAttack);
        j.onMove.AddListener(PlayerCtrl.OnMove);
        j.onMoveEnd.AddListener(PlayerCtrl.OnMoveEnd);

        // playerObj.transform.position = Camera.main.transform.parent.position;
        // playerObj.transform.parent = Camera.main.transform.parent;

        follow f = Camera.main.GetComponent<follow>();
        Transform cameraPoint = TransformHelper.GetChild(playerObj.transform, "cameraPoint");
        f.SetTarget(cameraPoint.gameObject);
        littleMapCamera subCamera=  GameObject.FindGameObjectWithTag("LittleMapCamera").GetComponent<littleMapCamera>();
        subCamera.SetTarget(cameraPoint.gameObject);
    }
    
}
