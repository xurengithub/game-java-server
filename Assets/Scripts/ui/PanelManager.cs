using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class PanelManager{
	//Layer
	public enum Layer{
		Panel,
		Tip,
	}
	//层级列表
	private static Dictionary<Layer, Transform> layers = new Dictionary<Layer, Transform>();
	//面板列表
	public static Dictionary<string, BasePanel> panels = new Dictionary<string, BasePanel>();
	//结构
	public static Transform root;
	public static Transform canvas;
	//初始化
	public static void Init(){
		root = GameObject.Find("Root").transform;
		canvas = root.Find("Canvas");
		Transform panel = canvas.Find("Panel");
		Transform tip = canvas.Find("Tip");
		layers.Add(Layer.Panel, panel);
		layers.Add(Layer.Tip, tip);
	}

	//打开面板
	public static void Open<T>(params object[] para) where T:BasePanel{
		//已经打开
		string name = typeof(T).ToString();
		if (panels.ContainsKey(name)){
			return;
		}
		//组件
		BasePanel panel = root.gameObject.AddComponent<T>();
		panel.OnInit();
		panel.Init();
		//父容器
		Transform layer = layers[panel.layer];
		panel.skin.transform.SetParent(layer, false);
		//列表
		panels.Add(name, panel);
		//OnShow
		panel.OnShow(para);
	}

	//关闭面板
	public static void Close(string name){
		//没有打开
		if(!panels.ContainsKey(name)){
			return;
		}
		BasePanel panel = panels[name];

		//OnClose
		panel.OnClose();
		//列表
		panels.Remove(name);
		//销毁
		GameObject.Destroy(panel.skin);
		Component.Destroy(panel);
	}

	public static void Hide(string name){
		if(!panels.ContainsKey(name)){
			return;
		}
		BasePanel panel = panels[name];
		if(!panel.skin.activeSelf){
			return;
		}
		panel.skin.SetActive(false);
	}

	public static void UnHide(string name){
		if(!panels.ContainsKey(name)){
			return;
		}
		BasePanel panel = panels[name];
		if(panel.skin.activeSelf){
			return;
		}
		panel.skin.SetActive(true);

	}

	public static bool IsHide(string name){
		if(!panels.ContainsKey(name)){
			return false;
		}
		BasePanel panel = panels[name];
		if(!panel.skin.activeSelf){
			return false;
		}
		return true;
	}
	public static void SetPosition(string name,Vector3 pos){
		if(!panels.ContainsKey(name)){
			return;
		}
		BasePanel panel = panels[name];
		panel.skin.transform.localPosition = pos;
		// Debug.Log("tip x:"+panel.skin.transform.localPosition.x);
	}

	public static bool IsOpened(string name){
		if(panels.ContainsKey(name)){
			return true;
		}
		return false;
	}

	public static void GameStartPanelInit()
    {
        Open<GameMainPanel>();
        Open<KnapsackPanel>();
        Hide("KnapsackPanel");
        Open<ShopPanel>();
        Hide("ShopPanel");
        Open<ToolTipPanel>();
        SetPosition("ToolTipPanel",ToolTipPanel.hidePos);
        
    }
	
}
