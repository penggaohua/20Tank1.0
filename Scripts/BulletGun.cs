using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGun : Bullet
{
    public float multiple = 2f;
    // Start is called before the first frame update



    private void Update()
    {
        Trajectory(2f);
    }

    //弹道实现
    void Trajectory(float multiple)
    {
        Vector2 v = Random.insideUnitCircle;
        transform.Rotate(Vector3.right, v.x * multiple);
        transform.Rotate(Vector3.up, v.y * multiple);
        //Debug.Log(randomFireRotaion);
    }

    
}
