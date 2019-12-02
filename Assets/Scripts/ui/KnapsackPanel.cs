using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class KnapsackPanel : BasePanel
{
    
    public GameObject bodyPanel;
    private KnapsackBody knapsackBody;
    private KnapsackProperties knapsackProperties;
    public override void OnInit(){
        skinPath = "KnapsackPanel";
        layer = PanelManager.Layer.Panel;
    }
    
    public override void OnShow(params object[] para){
        knapsackBody = skin.transform.GetComponentInChildren<KnapsackBody>();
        knapsackBody.Init();
        knapsackProperties = skin.transform.GetComponentInChildren<KnapsackProperties>();
        knapsackProperties.Init();
        SelectPlayerPanel.synItems += SynItems;
        SelectPlayerPanel.synProperties += SynProperties;
        // bodyPanel = skin.transform.
    }
    public override void OnClose(){
        SelectPlayerPanel.synItems -= SynItems;
        SelectPlayerPanel.synProperties -= SynProperties;
    }

    private void SynItems(ItemEntity[] items){
        for(int i = 0; i < items.Length; i++){
            for(int j=0;j<items[i].num;j++){
                knapsackBody.AddItem(items[i].itemId);    
            }
            
        }
    }

    private void SynProperties(PlayerEntity playerEntity){
        knapsackProperties.Give(playerEntity.coin);
    }
    
}
