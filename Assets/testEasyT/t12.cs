using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
/// <summary>
///
/// </summary>
public class t12 : MonoBehaviour
{
    // Start is called before the first frame update
    public ETCJoystick myj;
    void Start()
    {
        // EasyTouch.On_DragEnd += OnDragEnd;
        myj = GameObject.Find("MyJoystick").GetComponent<ETCJoystick>();
        // myj
        // myj.onMove+= OnMove;
        // myj.onMoveEnd
    }
    public void OnMove(){

    }
    // Update is called once per frame
    void Update()
    {
        Gesture g = EasyTouch.current;
        
        if(g !=null&&g.type == EasyTouch.EvtType.On_DragEnd){
            Debug.Log("end");
        }
    }

    public void OnDragEnd(Gesture g){
        Debug.Log(g.pickedObject.name);
        
    }

    public void Down(){
        Debug.Log("donw");
    }

    public void GetnAME(GameObject pic){
        Debug.Log(pic.name);
    }
}
