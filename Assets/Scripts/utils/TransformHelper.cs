using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class TransformHelper
{
    public static Transform GetChild(Transform parentTF, string childName){
        Transform childTF = parentTF.Find(childName);
        if(childTF !=null)return childTF;
        int count = parentTF.childCount;
        for(int i=0;i<count;i++){
            childTF = GetChild(parentTF.GetChild(i),childName);
            if(childTF !=null){
                return childTF;
            }
        }
        return null;
    }
}
