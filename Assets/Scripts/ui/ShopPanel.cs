using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
///
/// </summary>
public class ShopPanel : BasePanel
{
    public static UnityAction<int> Cost;
    public override void OnInit(){
        skinPath = "ShopPanel";
        layer = PanelManager.Layer.Panel;
    }

    public override void OnShow(params object[] para){
        NetManager.AddMsgListener("MsgBuyItem", OnMsgBuyItem);
    }

    public override void OnClose(){

    }

    public void OnMsgBuyItem(MsgBase msgBase){
        MsgBuyItem msg = (MsgBuyItem) msgBase;
        if(msg.result == 0){
            Cost(msg.itemId);
        }
    }
}
