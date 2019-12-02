using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class zhizou : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Cube (2)"){
            float z = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(0,0,-z*Time.deltaTime*5));
        }else{
            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            // transform.localRotation;
            transform.Translate(new Vector3(x*Time.deltaTime*5,0,z*Time.deltaTime*5));
        }
        
    }

    void OnTriggerEnter(Collider c){
        Debug.Log(c.name);
    }
}
