using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
///
/// </summary>
public class PlayerAtt : MonoBehaviour
{

    public static UnityAction PlayerDie;
    public static UnityAction<long, long> PlayerHp;
    public static UnityAction<long, long> PlayerMp;
    [SerializeField]
    private long hp;
    [SerializeField]
    private long maxHp;
    [SerializeField]
    private long mp;
    [SerializeField]
    private long maxMp;
    
    public void Init(long hp, long maxHp, long mp, long maxMp){
        this.hp = hp;
        this.maxHp = maxHp;
        this.mp = mp;
        this.maxMp = maxMp;
        PlayerHp(hp, maxHp);
        PlayerMp(mp, maxMp);
    }

    //减hp
    public void ReduceHp(long rhp){
        if(hp <= 0){
            //已经死亡
            return;
        }
        hp-=rhp;
        if(hp <= 0){
            PlayerDie();
            hp = 0;
        }
        PlayerHp(hp, maxHp);
    }
    //减mp
    public void ReduceMp(long rmp){
        if(mp <= 0){
            //无魔法
            return;
        }

        if(mp < rmp){
            //魔法不够
            return;
        }
        mp -= rmp;
        PlayerMp(mp, maxMp);
    }
    //加hp
    public void AddHp(long ahp){
        hp+=ahp;
        if(hp > maxHp){
            hp = maxHp;
        }
        PlayerHp(hp, maxHp);
    }
    //加mp
    public void AddMp(long amp){
        mp+=amp;
        if(mp > maxMp){
            mp = maxMp;
        }
        PlayerMp(mp, maxMp);
    }
}
