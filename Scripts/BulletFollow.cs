using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : Bullet
{
    [HideInInspector]
    public  Transform targetTransform;
    public float bulletSpeed =100f;
    public  Transform Target
    {
  
        set {
            Debug.Log("调用target");
            targetTransform = value;
        }
    }
    // Start is called before the first frame update
 

    // Update is called once per frame
    void  FixedUpdate()
    {
        //跟踪目标
        if (targetTransform)
        {
            transform.LookAt(targetTransform);
        }
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
            
    }

    void Spark()
    {

    }

}
