using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class mca : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move2(Vector3 deposition){
        if(deposition ==null){
            deposition =Vector3.zero;
        }
        Debug.Log("move");
        transform.position += deposition;
    }
    public void start(){
        Debug.Log("start");
    }
}
