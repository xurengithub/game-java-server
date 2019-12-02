using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public static class ItemDrop1
{
    public static float[] P = {0.1f,0.2f,0.3f,0.4f};
    public static float[] b = new float[P.Length];
    public static void Init(){
        for(int i = 0; i < P.Length; i++){
            for(int j = 0; j <= i; j++){
                b[i] += P[j];
            }
        }
    }
    
    public static int getNum(){
        int ran = Random.Range(1,100);
        float r = ran*0.01f;

        int num = 0;
        for(int i = 0; i < P.Length; i++){
            if(r>b[i]&&r<=b[i+1]){
                num = i+1;
                break;
            }
        }

        return num;
    }


}
