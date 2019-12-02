using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
/// <summary>
///
/// </summary>
public static class MultiPublisher
{
    public delegate void InsideEventListener(MsgBase msgBase);
	//消息监听列表
	private static Dictionary<string, InsideEventListener> InsideEventListeners = new Dictionary<string, InsideEventListener>();
	//添加消息监听
	public static void AddInsideEventListener(string msgName, InsideEventListener listener){
		//添加
		if (InsideEventListeners.ContainsKey(msgName)){
			InsideEventListeners[msgName] += listener;
		}
		//新增
		else{
			InsideEventListeners[msgName] = listener;
		}
	}
	//删除消息监听
	public static void RemoveInsideEventListener(string msgName, InsideEventListener listener){
		if (InsideEventListeners.ContainsKey(msgName)){
			InsideEventListeners[msgName] -= listener;
		}
	}
	//分发消息
	private static void FireMsg(string msgName, MsgBase msgBase){
		if(InsideEventListeners.ContainsKey(msgName)){
			InsideEventListeners[msgName](msgBase);
		}
	}
}
