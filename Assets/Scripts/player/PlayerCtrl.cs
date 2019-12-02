using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
[RequireComponent(typeof(PlayerAtt))]
public class PlayerCtrl : MonoBehaviour
{
    

    public float lastSendSyncTime = 0;
    public float mySpeed = 5;
    private static int attackState = Animator.StringToHash("Base Layer.attack");
    private Animator animator;
    private AnimatorStateInfo stateInfo;
    private Transform parent;
    
    private Vector3 m_GroundNormal;
    float m_ForwardAmount;
    float m_TurnAmount;
    float m_GroundCheckDistance = 0.1f;
    private bool isGround;
    private Rigidbody rig;
    
    void Start(){
        

        parent = transform.parent;
        animator = transform.GetComponent<Animator>();
        rig = transform.GetComponent<Rigidbody>();
        
    }

    

    public void OnMove(Vector2 v2){
        
        rig.constraints = ~(RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezePositionZ);
        float speed = Vector2.Distance(v2,Vector2.zero);
        animator.SetFloat("speed",speed);
        // transform.LookAt(new Vector3(transform.position.x+v2.x,transform.position.y,transform.position.z+v2.y));
        //摇杆向量
        // Debug.Log(v2.x +" " + v2.y);

        Vector3 vector3 = new Vector3(v2.x,0,v2.y);
        //摇杆旋转角度
        float yAngle = Vector3.Angle(Vector3.forward, vector3);
        if(v2.x<0){
            yAngle = -yAngle;
        }
        //摄像机为正方向旋转得到的人物相对世界的旋转
        Quaternion q =  Camera.main.transform.rotation*Quaternion.AngleAxis(yAngle, Vector3.up);
        Vector3 eulerAngles = q.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        q.eulerAngles = eulerAngles;
        // Debug.Log(v2.x+" "+v2.y);
        
        // transform.Translate(Vector3.forward*mySpeed*Time.deltaTime,Space.Self);
        // parent.position = transform.position;
        // CheckGroundStatus();
        // vector3 = Vector3.ProjectOnPlane(vector3, m_GroundNormal);
        // vector3.y=0;

        // transform.localRotation = Quaternion.LookRotation(vector3);

        transform.rotation = q;
        rig.constraints = RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezeRotationX;
        // Debug.Log(vector3.ToString());
        transform.Translate(Vector3.forward*vector3.magnitude*mySpeed*Time.deltaTime);
        // vector3 = child.InverseTransformDirection(vector3);
        // Debug.Log(vector3.ToString());
        // rig.MovePosition(rig.position+transform.forward*Time.deltaTime*7);
        

        SyncUpdate();
        
    }

    private void SyncUpdate(){
        //时间间隔判断
		if(Time.time - lastSendSyncTime < 0.05f){
			return;
		}
		lastSendSyncTime = Time.time;
		//发送同步协议
		MsgSyncPlayer msg = new MsgSyncPlayer();
		msg.x = transform.position.x;
		msg.y = transform.position.y;
		msg.z = transform.position.z;
		msg.ex = transform.eulerAngles.x;
		msg.ey = transform.eulerAngles.y;
		msg.ez = transform.eulerAngles.z;

		NetManager.Send(msg);
    }
    private void CheckGroundStatus(){
        RaycastHit hit;
        if(Physics.Raycast((transform.position+Vector3.up*0.1f),Vector3.down,out hit,0.1005f)){
            animator.applyRootMotion = false;
            m_GroundNormal = hit.normal;
            isGround = true;
            rig.useGravity = false;
        }else{
            animator.applyRootMotion = true;
            m_GroundNormal = Vector3.up;
            isGround = false;
            rig.useGravity = true;
            rig.velocity = new Vector3(0,rig.velocity.y,0);
        }

        
    }
    void OnCollisionStay(Collision other){
        if(other.gameObject.name == "Terrain"){
            rig.useGravity = false;
        }
    }

    void OnCollisionExit(Collision other){
        rig.useGravity = true;
        rig.velocity = new Vector3(0,rig.velocity.y,0);
    }

    public void OnMoveEnd(){
        animator.SetFloat("speed",0);
        // child.localRotation = Quaternion.Euler(Vector3.zero);
        // transform.localRotation = Quaternion.Euler(Vector3.zero);
        // transform.localPosition = Vector3.zero;
    }

    public void OnAttack(){
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if(state.nameHash != attackState){

            MsgAcAttack msg = new MsgAcAttack();
            NetManager.Send(msg);
            
            animator.SetTrigger("attack");
            ETCInput.SetAxisEnabled("Horizontal",false);
            ETCInput.SetAxisEnabled("Vertical",false);

            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            for(int i = 0; i< monsters.Length; i++){
                if(Vector3.Distance(monsters[i].transform.position, transform.position)<=2){
                    Enemy enemy = monsters[i].GetComponent<Enemy>();
                    enemy.Damage(30);
                }
            }
        }
    }

    public void OnOpenJoystick(){
        ETCInput.SetAxisEnabled("Horizontal",true);
            ETCInput.SetAxisEnabled("Vertical",true);
    }
}
