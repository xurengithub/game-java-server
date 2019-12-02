using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///
/// </summary>
public class ShopContent : MonoBehaviour
{
    public GameObject shopSlotPrefab;

    private List<GameObject> shopSlots = new List<GameObject>();
    void Start()
    {
        initShop(ItemsConfigManager.shopItems);
    }

    private void initShop(List<Item> shopItems){
        for(int i = 0; i < shopItems.Count; i++){
            shopSlots.Add(Instantiate(shopSlotPrefab));
            shopSlots[i].transform.SetParent(this.transform);
            Image itemImg = shopSlots[i].transform.GetChild(1).GetComponent<Image>();
            itemImg.sprite = shopItems[i].sprite;
            Text priceText = shopSlots[i].transform.GetChild(3).GetComponent<Text>();
            priceText.text = shopItems[i].buyPrice.ToString();
            ShopItemData itemData =  shopSlots[i].transform.GetChild(1).GetComponent<ShopItemData>();
            itemData.slotIndex = i;
            itemData.item = shopItems[i];
        }
    }
}
