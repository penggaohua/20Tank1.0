using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float fingerSpeed = 0.05f;//手指灵敏度
    public float maxUpAngle = 20f;//炮管最大仰角
    public float maxDownAngle = 8f;//炮管最大俯角
    public Transform tankBodyTransform;
    public Transform cannonTransform;
    public Transform turretTransform;
    public Transform cameraRotatePosition;

    public Transform rotatepint;

    private Vector3 localEulerAngle;

    float x;
    float y;
    Vector3 upVector3;


    // Start is called before the first frame update
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                float h1 = Input.GetAxis("Mouse X");
                float v1 = Input.GetAxis("Mouse Y");
                fingerRotate(v1, h1);

            }
        }

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        //  print(v + "**VVVVVVVV***" + v.ToString());
        //print(h + "**HHHHHHHH***" + h.ToString());

        fingerRotate(v, h);
       // Debug.Log("和目标的距离" + GetDistance(tankBodyTransform));
       // transform.localPosition = new Vector3(transform.localPosition.x, 3.04f, transform.localPosition.z);
    }

    void fingerRotate(float v, float h)
    {
        // Debug.Log(transform.eulerAngles.x);
        //Debug.Log(transform.eulerAngles.y);
        //限制炮管不能超过最大仰角和最大俯角
        //if (transform.eulerAngles.x > maxUpAngle && transform.eulerAngles.x < 90 && v > 0)
        //    return;
        //if (transform.eulerAngles.x > maxDownAngle && transform.eulerAngles.x < maxDownAngle && v < 0)
        //    return;

        transform.RotateAround(tankBodyTransform.position, tankBodyTransform.up, -h);
        Vector3 axis = GetNormal(turretTransform.up, cannonTransform.forward);
        //transform.RotateAround(targetTransform.position, axis, v);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        // transform.eulerAngles = new Vector3(cannonTransform.eulerAngles.x, transform.eulerAngles.y, 0);


        //Debug.Log("炮管的角度"+cannonTransform.eulerAngles.x);
        if (cannonTransform.eulerAngles.x > 270)
        {
            if (cannonTransform.eulerAngles.x < 360 - maxUpAngle && v < 0)
            {
                Debug.Log("超出炮管仰角范围");
                return;
            }
        }
        else if (cannonTransform.eulerAngles.x < 90)
        {
            if (cannonTransform.eulerAngles.x > maxDownAngle && v > 0)
            {
                Debug.Log("超出炮管俯角范围");
                return;
            }
        }
        //transform.RotateAround(targetTransform.position, axis, v);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        //transform.eulerAngles = new Vector3(cannonTransform.eulerAngles.x, transform.eulerAngles.y, 0);

        cannonTransform.RotateAround(rotatepint.position, turretTransform.right, v * 0.3f);
        //固定z方向不旋转
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

        //镜头跟随炮管的角度
        transform.RotateAround(cameraRotatePosition.position,transform.right,v*0.3f);

    }



    Vector3 GetNormal(Vector3 v1, Vector3 v2)
    {
        return Vector3.Cross(v1, v2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, tankBodyTransform.position);


    }
    public float GetDistance(Transform targetTransform)
    {

        Vector3 v = targetTransform.position - transform.position;
        float distance = v.magnitude;
        return distance;
    }

}




