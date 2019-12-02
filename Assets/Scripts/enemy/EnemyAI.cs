using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMotor))]
[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyAnimation))]
[RequireComponent(typeof(HeadLabel))]
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        Attack,
        PathFinding
    }

    private State currState = State.PathFinding;
    private EnemyAnimation anim;
    private EnemyMotor motor;

    private GameObject target;
    private float SearchTargetIntervalTime = 3;
    private float lastSearchTime = 0;
    private float sightDistance = 6;

    private float attackDistance = 1;

    private float attackInterval = 2;
    private float lastAttackTime = 0;
    private void Start(){
        anim = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
    }

    private float atkTimer;
    void FixedUpdate(){
        UpdateTarget();
        switch(currState){
            case State.PathFinding:
                PathFinding();
            break;
            case State.Attack:
                Attack();
            break;
        }
        
    }

    void UpdateTarget(){
        float interval = Time.time - lastSearchTime;
        if(interval < SearchTargetIntervalTime){
            return;
        }

        lastSearchTime = Time.time;
        if(target == null){
            NoTarget();            
        }else{
            HasTarget();
        }
        
        
    }

    void NoTarget(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < players.Length; i++){
            if(Vector3.Distance(players[i].transform.position,transform.position) <= sightDistance){
                currState = State.Attack;
                target = players[i];
                break;
            }
        }
    }

    void HasTarget(){
        //如果目标死亡
        if(Vector3.Distance(transform.position, target.transform.position)>sightDistance){
            target = null;
            currState = State.PathFinding;
            Debug.Log("丢失目标");
        }
    }
    void PathFinding(){
        // anim.action.Play(anim.runName);
            if(motor.PathFinding() == false){
                currState =State.Attack;
            }
    }

    void Attack(){
        // if(atkTimer<=Time.time){

        //         atkTimer = Time.time+1;
        //     }
        if(target ==null){
            currState = State.PathFinding;
            return;
        }
        //如果目标死亡

        if(Vector3.Distance(transform.position, target.transform.position) < attackDistance){
            Damage();
        }else{
            MoveToTarget();
        }
    }

    void Damage(){
        float interval = Time.time - lastAttackTime;
        if(interval < attackInterval){
            return;
        }
        lastAttackTime = Time.time;
        Debug.Log(name+" 怪物攻击");
        PlayerAtt att = target.GetComponent<PlayerAtt>();
        att.ReduceHp(5);
    }

    void MoveToTarget(){
        motor.LookRotation(target.transform.position);
        motor.MoveForward();
    }
}
