using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGun : Enemy

{
    IEnumerator fireGun;
    IEnumerator reload;

    public float angleSpeed=3f;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        LookAtTarget(fire_gun_position,2);
        //检测到了主角，并且子弹满就开枪
        if(detectedWhat()=="Player" && currentBullet == maxBullet && isFiring == false)
        {
            isFiring = true;
            fireGun = FireGun();
            StartCoroutine(fireGun);
        }
        if(currentBullet==0 && isReloading ==false)
        {
            isReloading = true;
            reload = Reload(4);
            StartCoroutine(reload);
        }
        //Debug.Log("当前血量" + currentHp);
        if(currentHp<=0)
        {
            Die();
        }
    }

    void Die()
    {
        BaseDie();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyGun>().enabled = false;
        GetComponent<AIEnemeyTank>().enabled = false;
      
    }

}
