using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public float distance = 15;
    public float rot = 0;
    public float rotSpeed = 0.05f;
    public float roll = 30f * Mathf.PI * 2 / 360;
    private GameObject tank;

    private Move move;

    public float mouseWheelSensitivity = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        // SetTarget(GameObject.Find("Yuka_mecanim"));
        // move = GetComponent<Move>();
        if(NetManager2.id != null){
            GameObject target = NetManager2.players[NetManager2.id];
            SetTarget(target);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        roll = Mathf.Clamp(roll,0,Mathf.PI/3);
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
        if (target.transform.Find("cameraPoint") != null)
        {
            this.tank = target.transform.Find("cameraPoint").gameObject;
        }
        else this.tank = target;
    }

    public void RotateX()
    {
        // float w = Input.GetAxis("Mouse X") * rotSpeed;
        float w = ETCInput.GetAxisSpeed("Horizontal2");
        rot -= w;
    }

    public void MouseScroll()
    {
        float fov = Input.GetAxis("Mouse ScrollWheel");
        distance += fov * mouseWheelSensitivity;
        distance = Mathf.Clamp(distance, 5, 30);
    }

    public void RotateY()
    {
        // float w = Input.GetAxis("Mouse Y") * rotSpeed;
        float w = ETCInput.GetAxisSpeed("Vertical2");
        roll -= w;
    }
}
