using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopter : Enemy
{
    public GameObject propeller;//螺旋桨
    public Transform propellerAxis;
    public float maxHeight = 5f;//直升机最高高度,再低就不下降
    public float propellerSpeed = 30f;
    public float moveSpeed = 15f;

    public GameObject audioGO;
    bool isArrive = false;
    bool isLookAt = false;
    bool die = false;
    bool isOverPlayer= false;

    Vector3 targetPosition;
    IEnumerator reload_bullet;
    IEnumerator reload_cannon_1;

    IEnumerator fireGun;

    Vector2 initVector2;
    Vector2 currentVector2;
    public float roundSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
         targetPosition =  GetTargetPosition(120);
         initVector2 = new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z);

    }
    // Update is called once per frame
    void Update()
    {
        //螺旋桨转动
        propeller.transform.RotateAround(propellerAxis.position,propeller.transform.up, propellerSpeed);
        isPassPlayer();


        //todo:浆转动变慢，声音跟着变化
        if (currentHp <= 0)
        {          
            GetComponent<Rigidbody>().useGravity = true;   
        }

        //持续开火  --子弹
        if (detectedWhat() == "Player" && currentBullet == maxBullet && isFiring == false)
        {
            Debug.Log("直升机机枪扫射");
            isFiring = true;
            fireGun = FireGun();
            StartCoroutine(fireGun);
        }
        if (currentBullet == 0 && isReloading == false)
        {
            isReloading = true;
            reload_bullet = Reload(4);
            StartCoroutine(reload_bullet);
        }


        //持续开火  --导弹
        if (detectedWhat() == "Player" && isReloading == false)
        {
            FireCannon();

            reload_cannon_1 = Reload(8);
            StartCoroutine(reload_cannon_1);
        }
        if (isArrive == false )
        {
            if(isOverPlayer == false)
                LookAtTarget(target.position, 3f);
            else
                LookAtTarget(targetPosition, 3f);
            MoveTowardTarget(targetPosition, moveSpeed);
        }
        else//到达目标
        {
            targetPosition = GetTargetPosition(120);
            Debug.Log("生成一个目标位置" + targetPosition);
            isArrive = false;
            isOverPlayer = false;

        }

        if (GetDistance(targetPosition) < 1)
        {
            initVector2 = new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z);
            isArrive = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("直升机碰撞的是："+other.tag);
        if (other.tag == "Untagged")
            return;
        BaseDie();
        audioGO.SetActive(false);
        GetComponent<CapsuleCollider>().enabled = false;
        //todo：解决多次碰撞的问题
    }

    private void  SeekPlayer()
    {

        float distance = GetDistance();
        if (distance > moveDistance)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
            if (transform.position.y < maxHeight)
            {
                transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            }
        }
    }

    Vector3 GoPosition(int randomRange)
    {
        float px = target.transform.position.x + Random.Range(-randomRange, randomRange);
        float pz = target.transform.position.z + Random.Range(-randomRange, randomRange);
        float py = transform.position.y;
        Vector3 newPosition = new Vector3(px, py, pz);
        //    Debug.Log("newPosition" + newPosition);
        return newPosition;
    }
    /// <summary>
    /// 看向目标位置
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="angleSpeed"></param>
    private  void LookAtTarget(Vector3  targetPosition, float angleSpeed)
    {
        //tankTurret.transform.LookAt(targetPosition);
        Quaternion q = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * angleSpeed);      
    }

    /// <summary>
    /// 
    /// 直升飞机绕着目标转圈
    /// </summary>
    /// <param name="angleSpeed"></param>
    void  GoRound(float angleSpeed)
    {
        transform.RotateAround(target.position, Vector3.up, Time.deltaTime* roundSpeed);
    }

    /// <summary>
    /// 直升飞机停下来转向
    /// </summary>
    void StopAndTurn()
    {

    }
    /// <summary>
    /// 直升飞机的移动
    /// </summary>
    /// <param name="targetPoint"></param>
    /// <param name="moveSpeed"></param>
    void MoveTowardTarget(Vector3 targetPoint,float moveSpeed )
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
    }

    /// <summary> 
    /// 用来计算直升飞机和目标的对点
    /// </summary>
    /// <returns></returns>
    Vector3 GetTargetPosition(float distance)
    {
        Vector2 v1 = new Vector2(target.position.x, target.position.z);
        Vector2 v2 = new Vector2(transform.position.x, transform.position.z);
        Debug.Log("v1"+v1);
        Debug.Log("v2" + v2);
        Vector2 v3 = v1 - v2;
        Debug.Log(v3);
        if (v3.magnitude == 0)
            return transform.position;
        float magnitude = v3.magnitude;
        float  sina = ((float)v3.y / magnitude);
        float  cosa = ((float)v3.x / magnitude);
        Debug.Log("sina" + sina);
        Debug.Log("cosa" + cosa);

        float targetX =  distance * cosa + target.transform.position.x;
        float targetZ =  distance * sina + target.transform.position.z;

        float targetY = transform.position.y;
        Vector3 targetPosition = new Vector3(targetX, targetY, targetZ);
        Debug.Log("算出的目标点是:" + targetPosition);
        return targetPosition;

    }

    float GetDistance(Vector3 targetPoint )
    {
        Vector3 v = targetPoint - transform.position;
        float distance = v.magnitude;
        return distance;
    }

    void  isPassPlayer()
    {
        currentVector2 =  new Vector2(transform.position.x - target.transform.position.x, transform.position.z - target.transform.position.z);
        float angle = Vector2.Angle(currentVector2, initVector2);
        if (angle > 90)
             isOverPlayer = true;
        else
            isOverPlayer = false;

    }
}
