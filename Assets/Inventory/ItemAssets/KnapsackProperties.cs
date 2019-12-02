using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
/// <summary>
///
/// </summary>
public class KnapsackProperties : MonoBehaviour
{
    
    private long coinNum = 0;
    private Text coinText;
    public void Init()
    {

        // coinNum = 200;
        coinText = transform.GetChild(1).GetComponent<Text>();
        // coinText.text = coinNum.ToString();
    }
    

    public bool Cost(long coinReduce){
        if(coinNum >= coinReduce){
            coinNum -= coinReduce;
            coinText.text = coinNum.ToString();
            return true;
        }
        return false;
    }
    public bool Give(long coinAdd){

        coinNum += coinAdd;
        coinText.text = coinNum.ToString();
        return true;
    }

}
