using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Consumable : Item
{
    public Consumable(int id, string name, string type, string quality, int buyPrice, int sellPrice, string desc, bool stackable, int stackMax, string slug, int hp, int mp) 
    :base(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug){
        this.hp = hp;
        this.mp = mp;

    }
    public Consumable() : base(){

    }
    public int hp{get;set;}
    public int mp{get;set;}
}
