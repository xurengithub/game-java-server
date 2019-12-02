using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Item
{
    public Item(){
        this.id = -1;
    }
    public Item(int id, string name, string type, string quality, int buyPrice, int sellPrice, string desc, bool stackable, int stackMax, string slug){
        this.id = id;
        this.name = name;
        this.type = type;
        this.quality = quality;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
        this.desc = desc;
        this.stackable = stackable;
        this.stackMax = stackMax;
        this.sprite = Resources.Load<Sprite>("Items/"+slug);

    }
    public int id{get;set;}
    public string name{get;set;}
    public string type{get;set;}
    public string quality{get;set;}
    public int buyPrice{get;set;}
    public int sellPrice{get;set;}
    public string desc{get;set;}
    public bool stackable{get;set;}
    public int stackMax{get;set;}
    public Sprite sprite{get;set;}
}
public class ItemConsts
{

    public static string QULITY_POOR = "粗糙";//灰色
    public static string QULITY_COMMON = "普通";//白色
    public static string QULITY_UNCOMMON = "优秀";//绿色
    public static string QULITY_RARE = "精良";//蓝色
    public static string QULITY_EPIC = "史诗";//紫色
    public static string QULITY_LEGENDARY = "传说";//橙色
    // public static string QULITY_HEIRLOOM = "神器";//金色

    public static string COLOR_QULITY_POOR = "grey";//灰色
    public static string COLOR_QULITY_COMMON = "white";//白色
    public static string COLOR_QULITY_UNCOMMON = "green";//绿色
    public static string COLOR_QULITY_RARE = "blue";//蓝色
    public static string COLOR_QULITY_EPIC = "purple";//紫色
    public static string COLOR_QULITY_LEGENDARY = "orange";//橙色
    // public static string COLOR_QULITY_HEIRLOOM = "神器";//金色
    public static int a=1;
}