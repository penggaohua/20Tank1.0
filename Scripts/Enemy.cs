using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Machine
{
    public Transform target;
    public float moveDistance = 20;
    public float attackRange = 50f;
    [Header("tank用炮塔，直生机用transform")]
    public Transform tankTurret;


    [Header("死亡相关特效")]
    public GameObject tankExplosionPrefab;
    public GameObject diesmokeExplodeEffect;

    public float speed = 5f;
    bool isDetectedPlayer = false;



    void Start()
    {

     
    }

    private void FixedUpdate()
    {
        
    }

    //看着目标
    protected void LookAtTarget(Transform firePosition,float angleSpeed)
    {
        if (target)
        {
            //Debug.Log("看着目标");
            Quaternion q = Quaternion.LookRotation(target.position - firePosition.position);
            tankTurret.rotation = Quaternion.Slerp(tankTurret.rotation, q, Time.deltaTime * angleSpeed);
        }
        //else { Debug.Log("目标已经销毁"); }
    }


	
	//检测距离
	protected float GetDistance( )
	{
        if(target)
        {       
		    Vector3 v = target.position - transform.position;
            float distance = v.magnitude;
            //print("和目标距离:" + distance);
           // print("我的当前位置：" + transform.position);
		    return distance;
        }else
        {
            Debug.Log("目标已经销毁");
            return -1f; }
    }


    protected void   BaseDie()
    {
        Debug.Log("死了 ，播放效果");
        //冒烟
        GameObject go = Instantiate(diesmokeExplodeEffect, transform.position, Quaternion.identity);
        go.transform.parent = transform.transform;
        //爆炸
        Instantiate(tankExplosionPrefab, transform.position, Quaternion.identity);
        hpSlider.gameObject.SetActive(false);
        transform.tag = "Die";
        this.enabled = false;

    }
}
