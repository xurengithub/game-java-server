using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class ddd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string s = "[{\"id\":0,\"name\":\"胸甲2\",\"type\":\"Equipment\"},{\"id\":1,\"name\":\"胸甲\",\"type\":\"Equipment\"}]";
        A[] e = JsonHelper.getJsonArray<A>(s);
        Debug.Log(e[1].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class A{
    public int id;
    public string name;
}