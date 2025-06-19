using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankController : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    private bool isPress = false;
    public float maxSpeed = 1f;
    private  float accelerate = 0.1f;//加速度
    private float torque = 10f;//角速度
    public float angle = 10f;//角速度

    private float decelerate = 0.008f;//刹车的减速度

    private GameObject tankBody;
    public Control control =Control.up;
    public float tankCurrentSpeed = 0f;

    private Tank tank;

    IEnumerator firGunCoroutine;
    IEnumerator reloadCoroutine;

    Rigidbody rb;
    private void Awake()
    {
        tankBody = GameObject.Find("/TankPlayer/TankBody"); 
    }

    private void Start()
    {
        tank = GetComponentInParent<Tank>();
        firGunCoroutine = tank.FireGun();
        reloadCoroutine = tank.Reload(tank.CD_bullet);
        rb = tankBody.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {




        if (isPress)
        {
            switch (control)
            {
                case Control.up:
                    Tank.Speed += Time.deltaTime * accelerate;
                    break;
                case Control.down:
                    Tank.Speed -= Time.deltaTime * accelerate;
                    break;
                case Control.brake:
                    Brake();
                    Debug.Log("刹车");
                    break;
                case Control.left:
                    Debug.Log("左转弯");
                    Turn(-angle);
                    break;
                case Control.right:
                    Debug.Log("右转弯");
                    Turn(angle);
                    break;
                case Control.gun:
                    //Debug.Log("isfiring" + Tank.isFiring);
                    break;

                default: break;
            }
        }
        else
        {
            StopCoroutine(firGunCoroutine);
            tank.isFiring = false;
        }

      
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPress = true;
        //print("down");
        //print("是否装填" + tank.isReloading);
        //if(control==Control.gun && Tank.isReloading == false)
        if(control==Control.gun)
        {
            if (tank.currentBullet > 0 && tank.isReloading == false && tank.isFiring == false)
            {
                firGunCoroutine = tank.FireGun();
                StartCoroutine(firGunCoroutine);//开火
            }

            if (tank.currentBullet == 0 && tank.isReloading == false && tank.isFiring==false )
            {
                reloadCoroutine = tank.Reload(tank.CD_bullet);
                StartCoroutine(reloadCoroutine);//装弹
            }               

        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
       // print("up");

        if (control == Control.gun && firGunCoroutine!=null)
        {
            StopCoroutine(firGunCoroutine);
        }
    }


    //刹车
    public void Brake()
    {
        Debug.Log("brake.....");
        if (Tank.Speed > 0)
        {
            Tank.Speed = (Tank.Speed - decelerate) > 0 ? (Tank.Speed - decelerate) : 0;
        }
        else if (Tank.Speed < 0)
        {
            Tank.Speed = (Tank.Speed + decelerate) < 0 ? (Tank.Speed + decelerate) : 0;
        }
       // Debug.Log(Tank.Speed);
        // TankMove(tankCurrentSpeed);
        // tankTransform.transform.Translate(new Vector3(0, 0, tankCurrentSpeed));

    }

    public void Turn(float angle)
    {
    //    Debug.Log("调用turn");
       // rb.AddRelativeTorque(tankBody.transform.up * torque);
        tankBody.transform.Rotate(tank.transform.up * angle*Time.deltaTime);
    }
    
}
public  enum Control
{
    up,
    down,
    left,
    right,
    brake,
    gun
}