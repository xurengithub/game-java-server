using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
/// <summary>
///
/// </summary>
public class te2 : MonoBehaviour
{
    
    private void OnEnable(){
        
        EasyTouch.On_TouchStart += OnTouchStart;
        EasyTouch.On_TouchUp += OnTouchEnd;
        EasyTouch.On_Swipe += OnSwipe;
    }
    private void OnDisable(){
        EasyTouch.On_TouchStart -= OnTouchStart;
        EasyTouch.On_TouchUp -= OnTouchEnd;
        EasyTouch.On_Swipe -= OnSwipe;
    }
    public void m(Vector2 v2){
        Debug.Log(v2.x+" "+v2.y);
    }

    public void OnTouchStart(Gesture gesture){
        Debug.Log("start"+gesture.startPosition);
    }
    public void OnTouchEnd(Gesture gesture){
        Debug.Log("end"+gesture.startPosition);
    }
    public void OnSwipe(Gesture gesture){
        Debug.Log("swipe"+gesture.swipe);
        // gesture.pickedObject;
    }
}
