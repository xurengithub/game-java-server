using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class ani2 : MonoBehaviour
{
    public GameObject effectPre;
    public Transform effectPoint;
    private GameObject o;
    // public Animator animator;
    // void Start()
    // {
    //     animator = transform.GetComponent<Animator>();
    // }

    public void OnOpenJoystick(){
        ETCInput.SetAxisEnabled("Horizontal",true);
            ETCInput.SetAxisEnabled("Vertical",true);
    }
    public void AttackEffect(){
        o= Instantiate(effectPre,effectPoint.position,Quaternion.identity);
        o.transform.parent = effectPoint;
    }

    public void AttackEffectEnd(){
        Destroy(o);
    }
}
