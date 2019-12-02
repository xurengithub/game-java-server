using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class testRota : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Quaternion q= Quaternion.Euler(0,30,0);
        gameObject.transform.Rotate(q.eulerAngles);
        Debug.Log(transform.localEulerAngles.y);
        Debug.Log(transform.eulerAngles.y);
        Transform t =transform.GetChild(0);
        Debug.Log(t.eulerAngles.y);
        Debug.Log(t.localEulerAngles.y);
        // gameObject.transform.rotation.SetLookRotation(q.eulerAngles);
        // Debug.Log(gameObject.transform.forward.x+" "+gameObject.transform.forward.y+" "+gameObject.transform.forward.z);

         Debug.Log(Vector3.Angle(Vector3.forward,transform.forward));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
