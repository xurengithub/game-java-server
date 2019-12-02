using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class WayLine
{
    public Vector3[] wayPoints{get;set;}
    public bool isUsable{get;set;}

    public WayLine(int waPointCount){
        wayPoints = new Vector3[waPointCount];
        isUsable = true;
    }
}
