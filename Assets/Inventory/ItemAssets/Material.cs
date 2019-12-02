using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Material : Item
{
    public Material(int id, string name, string type, string quality, int buyPrice, int sellPrice, string desc, bool stackable, int stackMax, string slug) 
    :base(id,name,type,quality,buyPrice,sellPrice,desc,stackable,stackMax,slug){

    }
    public Material() : base(){

    }
}
