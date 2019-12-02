using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
/// <summary>
///
/// </summary>
public class ToolTipPanel : BasePanel
{
    public static Vector3 hidePos = new Vector3(-1000,-1000,0);
    private Text tipText;
    private Image tipImage;
    private ContentSizeFitter contentSizeFitter;
    public override void OnInit(){
        skinPath = "ToolTipPanel";
        layer = PanelManager.Layer.Panel;
    }
    
    public override void OnShow(params object[] para){
        tipText = skin.transform.GetComponentInChildren<Text>();
        tipImage = skin.transform.GetChild(0).GetComponent<Image>();
        contentSizeFitter = skin.GetComponent<ContentSizeFitter>();
        ItemData.OnEnter += this.OnEnter;
        ItemData.OnExit += this.OnExit;

        ShopItemData.OnEnter += this.OnEnter;
        ShopItemData.OnExit += this.OnExit;
    }
    public override void OnClose(){
        ItemData.OnEnter -= this.OnEnter;
        ItemData.OnExit -= this.OnExit;

        ShopItemData.OnEnter -= this.OnEnter;
        ShopItemData.OnExit -= this.OnExit;
    }

    public void OnEnter(Transform transform,int UIType){
        ItemData data = transform.GetComponent<ItemData>();
        Item item = null;
        if(data == null){
            ShopItemData data2 = transform.GetComponent<ShopItemData>();
            item = data2.item;
        }else{
            item = data.item;
        }

        if(item == null) return;
        if(item.id == -1) return;
        string text = "";

        if(UIType == 0){
            text = GetTooltipText(item);
        }else if(UIType == 1){
            text = GetShopTipText(item);
        }
        
        
        tipText.text = text;
        tipImage.sprite = item.sprite;
        contentSizeFitter.SetLayoutVertical();
        
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.FindGameObjectWithTag("Canvas").GetComponent<RectTransform>(),Input.mousePosition,null,out position);
        // Debug.Log(position.x);
        // Debug.Log(Input.mousePosition.x);
        // StartCoroutine(Asyn(position));
        PanelManager.SetPosition(GetType().ToString(), position);
        // PanelManager.UnHide(name);
    }
    IEnumerator Asyn(Vector2 position){
        yield return new WaitForSeconds(0.05f);
        PanelManager.SetPosition(GetType().ToString(), position);
    }
    public void OnExit(){
        // PanelManager.Hide(name);
        PanelManager.SetPosition(GetType().ToString(), hidePos);
    }

    public string GetTooltipText(Item item){
        if(item == null) return "";
        StringBuilder sb = new StringBuilder();
        string color = GetColorByQulity(item.quality);
        sb.AppendFormat("<color={0}>{1}.{2}</color>\n",color,item.name,item.quality);
        sb.AppendFormat("<color=yellow>{0}</color>\n\n",item.desc);
        switch(item.type){
            case "Equipment":
            Equipment equip = (Equipment)item;
            sb.AppendFormat("<color=white>力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n装备类型：{4}\n\n</color>",equip.strength,equip.intellect,equip.agility,equip.stamina,equip.equipType);
            break;
            case "Weapon":
            Weapon weapon = (Weapon)item;
            sb.AppendFormat("<color=white>伤害：{0}\n武器类型：{1}\n\n</color>",weapon.damage,weapon.weaponType);
            break;
            case "Material":
            break;
            case "Consumable":
            Consumable consumable = (Consumable)item;
            sb.AppendFormat("<color=white>HP：{0}\nMP：{1}\n\n</color>",consumable.hp,consumable.mp);
            break;
            default:
            break;

        }
        sb.AppendFormat("<color=white>出售价格：{0}</color>\n",item.sellPrice);
        return sb.ToString();
    }

    private string GetShopTipText(Item item){
        if(item == null) return "";
        StringBuilder sb = new StringBuilder();
        string color = GetColorByQulity(item.quality);
        sb.AppendFormat("<color={0}>{1}.{2}</color>\n",color,item.name,item.quality);
        sb.AppendFormat("<color=yellow>{0}</color>\n\n",item.desc);
        switch(item.type){
            case "Equipment":
            Equipment equip = (Equipment)item;
            sb.AppendFormat("<color=white>力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n装备类型：{4}\n\n</color>",equip.strength,equip.intellect,equip.agility,equip.stamina,equip.equipType);
            break;
            case "Weapon":
            Weapon weapon = (Weapon)item;
            sb.AppendFormat("<color=white>伤害：{0}\n武器类型：{1}\n\n</color>",weapon.damage,weapon.weaponType);
            break;
            case "Material":
            break;
            case "Consumable":
            Consumable consumable = (Consumable)item;
            sb.AppendFormat("<color=white>HP：{0}\nMP：{1}\n\n</color>",consumable.hp,consumable.mp);
            break;
            default:
            break;

        }
        return sb.ToString();
    }
    private string GetColorByQulity(string qulity){
        string color = ItemConsts.COLOR_QULITY_COMMON;
        if(qulity.Equals(ItemConsts.QULITY_POOR)){
            color = ItemConsts.COLOR_QULITY_POOR;
        }else if(qulity.Equals(ItemConsts.QULITY_COMMON)){
            color = ItemConsts.COLOR_QULITY_COMMON;
        }else if(qulity.Equals(ItemConsts.QULITY_UNCOMMON)){
            color = ItemConsts.COLOR_QULITY_UNCOMMON;
        }else if(qulity.Equals(ItemConsts.QULITY_RARE)){
            color = ItemConsts.COLOR_QULITY_RARE;
        }else if(qulity.Equals(ItemConsts.QULITY_EPIC)){
            color = ItemConsts.COLOR_QULITY_EPIC;
        }else if(qulity.Equals(ItemConsts.QULITY_LEGENDARY)){
            color = ItemConsts.COLOR_QULITY_LEGENDARY;
        }
        
        return color;
    }
}
