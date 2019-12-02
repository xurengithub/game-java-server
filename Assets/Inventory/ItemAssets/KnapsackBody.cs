using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// </summary>
public class KnapsackBody : MonoBehaviour
{
    public GameObject slotPrefab;
    public GameObject itemPrefab;
    public int num = 25;
    public List<GameObject> slots = new List<GameObject>();
    public List<Item> items = new List<Item>();
    private KnapsackProperties knapsackProperties;
    public void Init()
    {
        
        for(int i = 0; i < num; i++){
            GameObject slot = Instantiate(slotPrefab);
            slot.transform.SetParent(this.gameObject.transform);
            slot.GetComponent<Slot>().slotID = i;
            slots.Add(slot);
            items.Add(new Item());
        }
        knapsackProperties = this.transform.parent.GetComponentInChildren<KnapsackProperties>();
        ShopPanel.Cost += this.Cost2;

        
    }

    public void Cost2(int id){
        AddItem(id);
        Item item = ItemsConfigManager.FindItemCfgById(id);
        knapsackProperties.Cost(item.buyPrice);
    }
    public void AddItem(int id){
        Item item = ItemsConfigManager.FindItemCfgById(id);
        if(item.stackable == true && CheckItemExist(id, 0)){
            for(int i = 0;i < items.Count; i++){
                if(items[i].id == id){
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    if(data.count < item.stackMax){
                        data.count++;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.count.ToString();
                        
                    }else if(!CheckItemExist(id, i+1)){
                        addNewItem(item);
                        break;
                        
                    }
                }
            }
        }else{
            addNewItem(item);
        }
        
        
    }

    bool CheckItemExist(int id, int offset){
        for(int i = offset;i < items.Count; i++){
            if(items[i].id == id){
                return true;                    
            }
        }
        return false;
    }
    public void addNewItem(Item item){
        for(int j = 0; j < items.Count; j++){
            if(items[j].id == -1){
                items[j] = item;
                GameObject itemObj = Instantiate(itemPrefab,slots[j].transform.position,Quaternion.identity,slots[j].transform);
                // itemObj.transform.SetParent(slots[j].transform);
                itemObj.name = items[j].name;
                itemObj.GetComponent<Image>().sprite = items[j].sprite;
                itemObj.GetComponent<ItemData>().item = items[j];
                itemObj.GetComponent<ItemData>().slotIndex = j;
                break;
            }
        }
    }
}
