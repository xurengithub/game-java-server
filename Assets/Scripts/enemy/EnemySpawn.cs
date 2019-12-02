using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    //怪物列表
    public WayLine[] lines;
    public GameObject[] enemyTypes;

    public int maxCount = 5;
    public int startCount = 2;

    private int spawnedCount;

    public int maxDelay;

    void Start(){
        CalculateWayLines();
        for(int i = 0; i < startCount; i++){
            GenerateEnemy();
        }
    }

    private void CreateEnemy(){
        //选择一条可用路线
        WayLine[] usableLines = FindUsaleWayLines();
        WayLine way = usableLines[Random.Range(0, usableLines.Length)];
        //延迟时间随机
        int index = Random.Range(0,enemyTypes.Length);
        GameObject enemy = Instantiate(enemyTypes[index], way.wayPoints[0],Quaternion.identity) as GameObject;

        EnemyManager.monsters.Add(enemy);

        EnemyMotor motor = enemy.GetComponent<EnemyMotor>();
        Enemy e = enemy.GetComponent<Enemy>();
        e.spawn = this;
        motor.line = way;
        way.isUsable = false;
    }
    public void GenerateEnemy(){
        spawnedCount++;
        if(spawnedCount >= maxCount){
            return;
        }
        Invoke("CreateEnemy",Random.Range(1,maxDelay));
    }

    public void CalculateWayLines(){
        lines = new WayLine[this.transform.childCount];
        for(int i = 0; i < lines.Length; i++){
            
            Transform wayline = transform.GetChild(i);
            int childCount = wayline.childCount;
            lines[i] = new WayLine(childCount);
            for(int j = 0; j < childCount; j++){
                lines[i].wayPoints[j] = wayline.GetChild(j).position;
            }
        }
    }

    WayLine[] FindUsaleWayLines(){
        List<WayLine> list = new List<WayLine>(lines.Length);
        foreach(var line in lines){
            if(line.isUsable){
                list.Add(line);
            }
        }

        return list.ToArray();
        // for(int i=0;i<lines.Length;i++){
        //     if(lines[i].isUsable == true){
        //         return lines[i];
        //     }
        // }
        // return null;
    }
}
