

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;
public class follow : MonoBehaviour
{
    public float aaa = 5;
    public float distance = 5;
    public float rot = 0;
    public float rotSpeed = 0.05f;
    public float roll = 30f * Mathf.PI * 2 / 360;
    private GameObject tank;

    private Move move;

    public float mouseWheelSensitivity = 1;
    // Start is called before the first frame update
    // void Start()
    // {
        
    //     // SetTarget(GameObject.Find("Yuka_mecanim 1"));
    //     // move = GetComponent<Move>();
    //     // if(NetManager2.id != null){
    //     //     GameObject target = NetManager2.players[NetManager2.id];
    //     //     SetTarget(target);
    //     // }
    // }

    void Update(){
        // if(distance != aaa){
        //     distance = aaa;
        // }
        
        
        if(tank ==null){
            return;
        }
        RaycastHit hit;
        Vector3 rayTarget = ((transform.position-tank.transform.position).normalized)*aaa;
        if(Physics.Raycast(tank.transform.position, rayTarget, out hit)){
            string name = hit.collider.gameObject.tag;
            if(name != "MainCamera"){
                float currentDistance = Vector3.Distance(hit.point, tank.transform.position);
                if(currentDistance < distance){
                    distance = currentDistance;
                }
            }
        }else{
            distance = aaa;
            Debug.Log(distance);
        }
    }
    void LateUpdate()
    {

        if (tank == null) return;
        if (Camera.main == null) return;
        RotateX();
        RotateY();
        MouseScroll();
        Vector3 targetPos = tank.transform.position;
        Vector3 cameraPos;
        roll = Mathf.Clamp(roll,-0.29f,Mathf.PI/3);
        float d = distance * Mathf.Cos(roll);
        float height = distance * Mathf.Sin(roll);
        cameraPos.x = targetPos.x + d*Mathf.Cos(rot);
        cameraPos.y = targetPos.y + height;
        cameraPos.z = targetPos.z + d*Mathf.Sin(rot);

        transform.position = cameraPos;
        transform.LookAt(tank.transform);
        
    }

    public void SetTarget(GameObject target)
    {
        // if (target.transform.Find("cameraPoint") != null)
        // {
        //     this.tank = target.transform.Find("cameraPoint").gameObject;
        // }
        //else 
        this.tank = target;
    }

    public void RotateX()
    {
        // float w = Input.GetAxis("Mouse X") * rotSpeed;
        float w = ETCInput.GetAxisSpeed("Horizontal3")*rotSpeed;
        // Debug.Log(w);
        rot -= w;
    }

    public void MouseScroll()
    {
        float fov = Input.GetAxis("Mouse ScrollWheel");
        aaa += fov * mouseWheelSensitivity;
        // distance = Mathf.Clamp(distance, 5, 30);
    }

    public void RotateY()
    {
        // float w = Input.GetAxis("Mouse Y") * rotSpeed;
        float w = ETCInput.GetAxisSpeed("Vertical3")*rotSpeed;
        roll -= w;
    }
}
