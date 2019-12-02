using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// </summary>
public class Enemy : MonoBehaviour
{
    public UnityAction<long, long> hpChangedAc;

    public EnemySpawn spawn;
    public long HP = 200;
    public long maxHP = 200;

    void Start(){
        // HeadLabel headLabel = transform.GetComponent<HeadLabel>();
        // hpChangedAc+=headLabel.OnHpChanged(HP, maxHP);
        // hpChangedAc(HP,maxHP);
    }

    public void Damage(long amount){
        Debug.Log("怪物被打了"+amount+"血");
        HP-=amount;
        if(HP<=0){
            Death();
            return;
        }
        hpChangedAc(HP,maxHP);
    }

    public void Death(){
        EnemyManager.monsters.Remove(gameObject);
        //销毁
        Destroy(gameObject,3);
        
        //播放动画
        GetComponent<EnemyMotor>().line.isUsable = true;

        spawn.GenerateEnemy();
    }
}
