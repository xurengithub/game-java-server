using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class littleMapCamera : MonoBehaviour
{
    private GameObject target;
    private Transform targetTrans;

    // Update is called once per frame
    void LateUpdate()
    {
        if(target == null)return;
        float x = targetTrans.position.x;
        float z = targetTrans.position.z;
        transform.position = new Vector3(x,transform.position.y,z);
    }

    public void SetTarget(GameObject target){
        this.target = target;
        this.targetTrans = target.transform;
    }
}
