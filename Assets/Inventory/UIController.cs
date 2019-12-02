using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class UIController : MonoBehaviour
{
    // private KnapsackManager knapsackManager;
    // private ShopManager shopManager;
    // private GameObject knapsack;
    // private GameObject shopPanel;
    public void init()
    {
        PanelManager.Open<GameMainPanel>();
        PanelManager.Open<KnapsackPanel>();
       
        
        PanelManager.Hide("KnapsackPanel");
        PanelManager.Open<ShopPanel>();
        PanelManager.Hide("ShopPanel");
        PanelManager.Open<ToolTipPanel>();
        PanelManager.SetPosition("ToolTipPanel",ToolTipPanel.hidePos);
        
    }

    // Update is called once per frame
 
    void OnGUI(){
        if(Event.current.rawType == EventType.KeyDown){
            Event e = Event.current;
            AB(e);
        }
    }

    bool AB(Event e){
        bool EventDown = (e.modifiers & EventModifiers.Control)!=0;
            switch(e.keyCode){
                case KeyCode.E:
                if(EventDown){
                    e.Use();
                    if(PanelManager.IsHide("KnapsackPanel")){
                        PanelManager.Hide("KnapsackPanel");
                    }else{
                        PanelManager.UnHide("KnapsackPanel");
                    }
                }
                return true;
                case KeyCode.W:
                if(EventDown){
                    e.Use();
                    if(PanelManager.IsHide("ShopPanel")){
                        PanelManager.Hide("ShopPanel");
                    }else{
                        PanelManager.UnHide("ShopPanel");
                    }
                }
                return true;
        }
        return false;
    }


}
