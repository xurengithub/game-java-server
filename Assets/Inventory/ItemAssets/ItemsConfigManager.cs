using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;
/// <summary>
///
/// </summary>
public static class ItemsConfigManager
{
    private static JsonData jsonData;
    private static List<Item> items = new List<Item>();
    public static List<Item> shopItems = new List<Item>();
    public static void init()
    {   
        string str ="";
        string url = Application.streamingAssetsPath+"/items.json";
        WWW w = new WWW(url);
    while (!w.isDone) { }
        jsonData = JsonMapper.ToObject(w.text);//File.ReadAllText(Application.dataPath + "/items.json"));
        ConstructItems();
        shopItems = items;
    }

    private static void ConstructItems(){
        for(int i = 0; i < jsonData.Count; i++){
            int id = (int)jsonData[i]["id"];
            string name = jsonData[i]["name"].ToString();
            string type = jsonData[i]["type"].ToString();
            string quality = jsonData[i]["quality"].ToString();
            int buyPrice = (int)jsonData[i]["buyPrice"];
            int sellPrice = (int)jsonData[i]["sellPrice"];
            string desc = jsonData[i]["desc"].ToString();
            bool stackable = (bool)jsonData[i]["stackable"];
            int stackMax = (int)jsonData[i]["stackMax"];
            string slug = jsonData[i]["slug"].ToString();

            Item item = null;// = new Item(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug);
            if(type.Equals("Equipment")){
                int strength = (int)jsonData[i]["strength"];
                int intellect = (int)jsonData[i]["intellect"];
                int agility = (int)jsonData[i]["agility"];
                int stamina = (int)jsonData[i]["stamina"];
                string equipType = jsonData[i]["equipType"].ToString();
                item = new Equipment(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug,strength,intellect,agility,stamina,equipType);
            }
            if(type.Equals("Weapon")){
                int damage = (int)jsonData[i]["damage"];
                string weaponType = jsonData[i]["weaponType"].ToString();
                item = new Weapon(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug,damage,weaponType);
            }
            if(type.Equals("Material")){
                item = new Material(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug);
            }
            if(type.Equals("Consumable")){
                int hp = (int)jsonData[i]["hp"];
                int mp = (int)jsonData[i]["mp"];

                item = new Consumable(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug,hp,mp);
            }

            if(item != null){
                items.Add(item);
            }
            
        }
    }
    public static Item FindItemCfgById(int id){
        for(int i = 0; i < items.Count; i++){
            if(items[i].id == id){
                return items[i];
            }
        }

        return null;
    }

}

