using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class testTrans : MonoBehaviour
{
    public float speed = 5;
    Transform t;
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        t.Translate(Vector3.forward*speed*Time.deltaTime,Space.World);
    }
}
