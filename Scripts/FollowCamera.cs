using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{


    float vx, vy, vz;
    public Transform targetTransform;
    public float angleSpeed=1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        //炮塔指向相机指向的方向
        //todo：处理误差
        transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, Time.deltaTime);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        //Debug.Log("世界角度"+transform.eulerAngles);
    
    }

}