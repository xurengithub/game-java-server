using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Equipment :Item
{
    public Equipment(int id, string name, string type, string quality, int buyPrice, int sellPrice, string desc, bool stackable, int stackMax, string slug, int strength, int intellect, int agility, int stamina, string equipType) 
    :base(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug){
        this.strength = strength;
        this.intellect = intellect;
        this.agility = agility;
        this.stamina = stamina;
        this.equipType = equipType;
    }
    public Equipment():base(){
        
    }
    public int strength{get;set;}
    public int intellect{get;set;}
    public int agility{get;set;}
    public int stamina{get;set;}
    public string equipType{get;set;}

}
