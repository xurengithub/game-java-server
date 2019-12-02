using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
///
/// </summary>
public class Slot : MonoBehaviour,IDropHandler
{
    public int slotID;
    KnapsackBody knapsackBody;
    // Start is called before the first frame update
    void Start()
    {
        knapsackBody = GameObject.Find("BodyPanel").GetComponent<KnapsackBody>();
    }

   

    public void OnDrop(PointerEventData eventData){
        ItemData itemData = eventData.pointerDrag.GetComponent<ItemData>();
        if(knapsackBody.items[slotID].id == -1){
            knapsackBody.items[itemData.slotIndex] = new Item();
            itemData.slotIndex = slotID;
            knapsackBody.items[slotID] = itemData.item;
        }else if(itemData.slotIndex != slotID){
            Transform item = this.transform.GetChild(0);
            item.GetComponent<ItemData>().slotIndex = itemData.slotIndex;
            item.transform.SetParent(knapsackBody.slots[itemData.slotIndex].transform);
            item.transform.position = item.transform.parent.position;

            
            Item tmp = knapsackBody.items[slotID];
            knapsackBody.items[slotID] = knapsackBody.items[itemData.slotIndex];
            
            knapsackBody.items[itemData.slotIndex] = tmp;
            itemData.slotIndex = slotID;
        }
    }
}
