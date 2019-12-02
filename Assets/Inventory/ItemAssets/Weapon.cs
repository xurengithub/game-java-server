using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Weapon : Item
{
    public Weapon(int id, string name, string type, string quality, int buyPrice, int sellPrice, string desc, bool stackable, int stackMax, string slug, int damage, string weaponType) 
    :base(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug){
        this.damage = damage;
        this.weaponType = weaponType;

    }
    public Weapon():base(){
        
    }
    public int damage{get;set;}
    public string weaponType{get;set;}

}

// public class WeaponConsts{
//     public static int ITEM_TYPE_WEAPON = 0;
//     public static int ITEM_TYPE_MATERIAL = 1;
//     public static int ITEM_TYPE_EQUIPMENT = 2;
//     public static int ITEM_TYPE_CONSUMABLE = 3;
// }