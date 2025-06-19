using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tank : Machine
{
    [SerializeField]
    public static float maxSpeed = 0.2f;


    private static float speed = 0f;
    private static float angle = 0f;//角速度

    public float rotationSpeed = 0f;


    public Transform tank_body;

    public Text tips;

    private float speed_forward;

    public AudioClip beenHitAC1;
    public AudioClip beenHitAC2;
    public AudioClip beenHitAC3;
    List<AudioClip> clips = new List<AudioClip>();

    public Image image_bullet;
    public Image image_cannon;
    public Image image_rocket;
    public Image targetMark;

    // Start is called before the first frame update
    Ray ray;

    public static float Speed
    {
        set
        {
            speed = value;
        }
        get
        {
            return speed;
        }
    }


    public static float Angle
    {
        set
        {
            angle = value;
        }
        get
        {
            return angle;
        }
    }


    public static float GetSpeedProportion
    {
        get
        {
            if (maxSpeed != 0)
                return speed / maxSpeed;
            else
                return 0;
        }
    }

    void Start()
    {

        clips.Add(beenHitAC1);
        clips.Add(beenHitAC2);
        clips.Add(beenHitAC3);
        rigidbody = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().centerOfMass = new Vector3(GetComponent<Rigidbody>().centerOfMass.x, -1.5F, GetComponent<Rigidbody>().centerOfMass.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TankMove(speed);

        //图片cd的变化
        //机枪子弹变化的两种形式
       // Debug.Log("loading_bullet" + loading_bullet);
        //Debug.Log("loading_bullet 比例" + (1-loading_bullet/CD_bullet));
        //todo:反向填充
        if (isReloading)
            image_bullet.fillAmount = 1 - loading_bullet / CD_bullet;
        else
            image_bullet.fillAmount = 1 - (float)currentBullet / maxBullet;

        //Debug.Log("图片填充"+image_bullet.fillAmount);
        image_cannon.fillAmount = 1 - loading_cannon / CD_cannon;
        image_rocket.fillAmount = 1 - loading_rocket / CD_rocket;

        if (DetectedFromCamera())
        {
            if (DetectedFromCamera().tag == "Enemy")
                targetMark.enabled = true;
            else
                targetMark.enabled = false;
        }
        else
            targetMark.enabled = false;


    }

    //
    void TankMove(float speedCurrent)
    {
        // Debug.Log("tankmove");
        speed = Mathf.Clamp(speedCurrent, -maxSpeed, maxSpeed);
        //Debug.Log("curentSpeed" + speedCurrent.ToString());
        transform.Translate(tank_body.transform.forward*speed);
        // rigidbody.velocity = tank_body.transform.forward * speed;
        rigidbody.MovePosition(rigidbody.transform.position + tank_body.transform.forward * Time.deltaTime * speed);//MovePosition方法使物体移动更平滑

    }

    Transform DetectedFromCamera()
    {
        //ray = new Ray(rayPoint.position, rayPoint.forward);
        ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        bool ishit = Physics.Raycast(ray, out hit);
        if (ishit)
        {
          //  Debug.Log("检测到了什么-->" + hit.collider.tag);
            return hit.transform;
        }
        else
            return null;
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(ray);
    }




    public new void PlayHitSound()
    {
        //受击音效
        int index = Random.Range(0, 2);
        Debug.Log("随机数：" + index);
        AudioSource.PlayClipAtPoint(clips[index], Camera.main.transform.position, 0.5f);
    }

    /// <summary>
    /// 发射热追踪导弹
    /// </summary>
    public void FireHotMissile()
    {
        Debug.Log("rocket被点击");
        if (loading_rocket < CD_rocket)
        {
            Debug.Log("导弹正在装填中");
            return;
        }
        loading_rocket -= CD_rocket;
        Debug.Log("发射热跟踪导弹");
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        GameObject go = Instantiate(hot_missile_prefab, fire_hot_missile_position.position, fire_hot_missile_position.rotation);
        //Debug.Log("目标的 transform" + hit.transform);
        Transform target  = DetectedFromCamera();
        if (target!=null && target.tag == "Enemy")
              go.GetComponent<BulletFollow>().Target = target.transform;
       
    }
}
