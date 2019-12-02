using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
/// <summary>
///
/// </summary>
public class easy2 : MonoBehaviour
{

    // Start is called before the first frame update
    public ETCJoystick mj;
    void Start()
    {
        mj = GameObject.Find("MyJoystick").GetComponent<ETCJoystick>();
    }

    // Update is called once per frame
    void Update()
    {
        Move2();
        Debug.Log(mj.name);
        Gesture g;
        
        // EasyTouch.On_Cancel;

    }

    public void Move2(){
        float x = mj.axisX.axisValue;
        float y = mj.axisY.axisValue;
        Vector3 v = new Vector3(x,0,y);
        Quaternion wa = Quaternion.LookRotation(v);
        transform.localRotation = wa;
        transform.Translate(v);
    }
}
