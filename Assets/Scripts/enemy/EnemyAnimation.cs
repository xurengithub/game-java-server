using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class EnemyAnimation : MonoBehaviour
{
    public string runName;
    public AnimationAction action;
    private void Awake(){
        action = new AnimationAction(GetComponentInChildren<Animator>());
    }

    
}
