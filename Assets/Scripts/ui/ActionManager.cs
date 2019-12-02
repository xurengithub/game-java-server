using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class ActionManager : MonoBehaviour
{
    public static void Init(){
        NetManager.AddMsgListener("MsgSyncPos", OnMsgSyncPos);
    }

    public static void OnMsgSyncPos(MsgBase msgBase){
        MsgSyncPlayer msg = (MsgSyncPlayer) msgBase;
        // if(msg.playerId == GameMain.playerId){
        //     return;
        // }
        // GameMain.players[msg.playerId].GetComponent<testMove>();

    } 
}
