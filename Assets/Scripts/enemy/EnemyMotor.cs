using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class EnemyMotor : MonoBehaviour
{
    public WayLine line;
    private int currentPointIndex;
    public float moveSpeed = 3;
    private float tolerance = 0.5f;

    private bool isGo = true;
    public void MoveForward(){
        this.transform.Translate(0,0,moveSpeed*Time.deltaTime);
    }

    public void LookRotation(Vector3 position){
        this.transform.LookAt(position);
    }

    public bool PathFinding(){
        if(line ==null)return false;
        //如果到了
        if(isGo){
            if(currentPointIndex >= line.wayPoints.Length){
                currentPointIndex = line.wayPoints.Length-1;
                isGo = false;
                return true;
            }
            //向下一個露點移動

            //转向目标点
            LookRotation(line.wayPoints[currentPointIndex]);
            //向前移动
            MoveForward();
            if(Vector3.Distance(this.transform.position, line.wayPoints[currentPointIndex]) < tolerance){

                currentPointIndex++;
            }
        }else{
            
            if(currentPointIndex < 0){
                currentPointIndex = 0;
                isGo = true;
                return true;
            }
            //向下一個露點移動

            //转向目标点
            LookRotation(line.wayPoints[currentPointIndex]);
            //向前移动
            MoveForward();
            if(Vector3.Distance(this.transform.position, line.wayPoints[currentPointIndex]) < tolerance){

                currentPointIndex--;
            }
        }

        return true;
    }
}
