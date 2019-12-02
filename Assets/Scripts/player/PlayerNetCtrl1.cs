using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
[RequireComponent(typeof(PlayerAtt))]
public class PlayerNetCtrl1 : MonoBehaviour
{
    //预测信息，哪个时间到达哪个位置
	private Vector3 lastPos;
	private Vector3 lastRot;
	private Vector3 forecastPos;
	private Vector3 forecastRot;
	private float lastTurretY;	private float forecastTurretY;
	private float forecastGunX;
	private float forecastTime;

    private Animator animator;
    private Rigidbody rig;
	private static int attackState = Animator.StringToHash("Base Layer.attack");
    void Start(){
		lastPos = transform.position;
		forecastPos = transform.position;
		lastRot = transform.rotation.eulerAngles;
        animator = transform.GetComponent<Animator>();
        rig = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    new void Update()
    {
        ForecastUpdate();
		// ActionUpdate();
    }

	public void SyncAction(MsgAcAttack acAttack){
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if(state.nameHash != attackState){
            
            animator.SetTrigger("attack");

        }
	}

    public void SyncPos(MsgSyncPlayer msg){
        // rig.constraints = ~(RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezePositionX|RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezePositionZ);
        //预测位置
        // float speed = Vector2.Distance(v2,Vector2.zero);
        // animator.SetFloat("speed",speed);

        // Vector3 vector3 = new Vector3(v2.x,0,v2.y);

        // transform.localRotation = Quaternion.LookRotation(vector3);


		Vector3 pos = new Vector3(msg.x, msg.y, msg.z);
		Vector3 rot = new Vector3(msg.ex, msg.ey, msg.ez);
		// forecastPos = pos + 2*(pos - lastPos);
		// forecastRot = rot + 2*(rot - lastRot);
		forecastPos = pos;	//跟随不预测
		forecastRot = rot;
		
		//更新
		lastPos = pos;
		lastRot = rot;
		forecastTime = Time.time;
        // rig.constraints = RigidbodyConstraints.FreezeRotationY|RigidbodyConstraints.FreezeRotationZ|RigidbodyConstraints.FreezeRotationX;
    }

    public void ForecastUpdate(){
        //时间
		float t =  (Time.time - forecastTime)/0.05f;
		t = Mathf.Clamp(t, 0f, 1f);
		//位置
		Vector3 pos = transform.position;
		if(pos == forecastPos){
			animator.SetFloat("speed",0);
		}else{
			animator.SetFloat("speed",1);
		}
		pos = Vector3.Lerp(pos, forecastPos, t);
		transform.position = pos;
		//旋转
		Quaternion quat = transform.rotation;
		Quaternion forcastQuat = Quaternion.Euler(forecastRot);
		quat = Quaternion.Lerp(quat, forcastQuat, t) ;
		transform.rotation = quat;
		
    }
}
