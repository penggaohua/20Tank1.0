using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 originPos;
    public Vector3 direction;
    public float maxDistance=100f;
    Vector3 target;
    void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) //点击鼠标右键
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); 
            RaycastHit hit;                                                     
            bool isHit = Physics.Raycast((Ray)ray, out hit);             
            if (isHit)
            {
                Debug.Log("坐标为：" + hit.point);
                target = hit.point; //检测到碰撞，就把检测到的点记录下来
                Move(target);

            }
        }
       
    }
    void Move(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime);
        }
        //如果物体的位置和目标点的位置距离小于 0.1时直接等于目标点
        else
            transform.position = target;
    }
}


