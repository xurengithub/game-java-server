using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
///
/// </summary>
public class ShopItemData : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public static UnityAction<Transform,int> OnEnter;
    public static UnityAction OnExit;
    // public static UnityAction<int> Cost;
    public Item item;
    public int slotIndex;

    public void OnPointerEnter(PointerEventData eventData){
        if(OnEnter != null){
            OnEnter(transform,1);
        }
    }
    public void OnPointerExit(PointerEventData eventData){
        if(OnExit != null){
            OnExit();
        }
    }

    public void ClickBuy(){
        MsgBuyItem msgBuyItem = new MsgBuyItem();
        msgBuyItem.itemId = item.id;
        NetManager.Send(msgBuyItem);
        // if(true){
        //     Debug.Log("buy");
        //     Cost(item.id);
        // }

    }

}

