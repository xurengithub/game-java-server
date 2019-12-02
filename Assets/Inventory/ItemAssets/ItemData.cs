using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
///
/// </summary>
public class ItemData : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public static UnityAction<Transform,int> OnEnter;
    public static UnityAction OnExit;
    public Item item;
    public int slotIndex;

    public int count = 1;
    public bool isOnClickOrDrag = false;
    private KnapsackBody knapsackBody;
    // Start is called before the first frame update
    void Start()
    {
        knapsackBody = GameObject.Find("BodyPanel").GetComponent<KnapsackBody>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(item != null){
            this.transform.SetParent(transform.parent.parent.parent);
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            isOnClickOrDrag = true;
            // GetComponent<Image>().raycastTarget = false;
        }
    }
    public void OnDrag(PointerEventData eventData){
        if(item != null){
            isOnClickOrDrag = true;
            this.transform.position = eventData.position;
        }
    }
    public void OnEndDrag(PointerEventData eventData){
        isOnClickOrDrag = false;
        GameObject slot =  knapsackBody.slots[slotIndex];
        this.transform.SetParent(slot.transform);
        this.transform.position = this.transform.parent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // GetComponent<Image>().raycastTarget = true;
    }
    public void OnPointerEnter(PointerEventData eventData){
        if(OnEnter != null){
            OnEnter(transform, 0);
        }
    }
    public void OnPointerExit(PointerEventData eventData){
        if(OnExit != null){
            OnExit();
        }
    }

}
