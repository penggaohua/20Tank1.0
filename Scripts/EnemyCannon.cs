using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCannon : Enemy
{
    /// <summary>
    /// 敌人-火炮
    /// </summary>
    /// 


    protected float timer = 3f;
    public float attackCD = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AutoFire(fire_cannon_position, 1f);
        if(currentHp<=0)
        {
            Die();
        }
        Debug.DrawLine(rayPoint.position, target.transform.position, Color.red);
    }

    protected void AutoFire(Transform firePosition, float angleSpeed)
    {

        float distance = GetDistance();
        timer = timer > attackCD ? attackCD : timer + Time.deltaTime;
        if (timer == attackCD && distance < attackRange && detectedWhat() == "Player")
        {
            FireCannon();
            timer = 0f;
        }
        LookAtTarget(firePosition, angleSpeed);
        
    }
    void Die()
    {
        BaseDie();
        //给炮塔加刚体
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AIEnemeyTank>().enabled = false;
        Rigidbody rig  = tankTurret.gameObject.AddComponent<Rigidbody>();
        rig.AddExplosionForce(1000, transform.position, 3);
    }

}
