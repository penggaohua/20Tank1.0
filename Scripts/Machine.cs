using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{

    public float hpMax = 1000f;
    public float currentHp;

    public AudioSource tankAS;

    public AudioClip bulletAC;
    //public AudioClip cannonAC;
    //public AudioClip rocketAC;

    public float volumeScale=1;


    public GameObject cannon_prefab;
    public GameObject bullet_prefab;
    public GameObject hot_missile_prefab;


    public Transform fire_cannon_position;
    public Transform fire_gun_position;
    public Transform fire_hot_missile_position;


    public GameObject bloodPrefab;
    public GameObject blood2Prefab;

    public Transform rayPoint;
    public GameObject flame_prefab;


    public  int maxBullet = 22;
    public  int currentBullet;
    protected Ray ray;

    public float impluse_bullet = 100;
    public float impluse_cannon = 150;
    public float impluse_rocket = 50;


    public  bool isFiring = false;
    public  bool isReloading = false;
    public Rigidbody rigidbody;


    [SerializeField]
    protected Slider hpSlider;
    private Transform tankTurret;
    GameObject bloodGO;

    [Header("CD")]
    public float CD_bullet=4;
    public float CD_cannon=3;
    public float CD_rocket=6;




    protected float loading_bullet=0;
    protected float loading_cannon;
    protected float loading_rocket;


    void Awake()
    {
        currentHp = hpMax;
        tankAS = GetComponent<AudioSource>();
        currentBullet = maxBullet;
        hpSlider = GetComponentInChildren<Slider>();
        tankTurret = this.transform.Find("TankTurret");
        loading_cannon = CD_cannon;
        loading_rocket = CD_rocket;

    }
    private void Start()
    {
        
    }


    private void LateUpdate()
    {
        if (isReloading)
            loading_bullet += Time.deltaTime;
        else
            loading_bullet = 0;

        loading_cannon = clampCD(loading_cannon, CD_cannon);
        loading_rocket = clampCD(loading_rocket, CD_rocket);

        

    }
    float clampCD(float timer,float CD)
    {
        if (timer > CD)
            timer = CD;
        else
            timer += Time.deltaTime;
        return timer;
    }

    // Update is called once per frame


    /// <summary>
    /// 发射炮弹
    /// </summary>
    public void FireCannon()
    {
        Debug.Log("cannon被点击");
        if (loading_cannon < CD_cannon)
        {
            Debug.Log("炮弹正在装填中");
            return;
        }
        loading_cannon -= CD_cannon;
        GameObject go = Instantiate(cannon_prefab, fire_cannon_position.position, fire_cannon_position.rotation);
        go.GetComponent<Rigidbody>().AddForce(fire_cannon_position.forward * impluse_cannon,ForceMode.Impulse);

    }
    
   


    /// <summary>
    /// 机枪扫射
    /// 按住按键后一直发射，直到停止按键或者子弹数=0
    /// 换弹cd
    /// </summary>

    public IEnumerator FireGun()
    {
        while (currentBullet!=0)
       {
           
             //Debug.Log("开火");
            isFiring = true;
            //发出音效
            //tankAS.clip = bulletAC;
            //tankAS.PlayOneShot(bulletAC,volumeScale);

            //发射子弹          
            GameObject go = Instantiate(bullet_prefab, fire_gun_position.position, fire_gun_position.rotation);
            go.GetComponent<Rigidbody>().AddForce(fire_gun_position.forward * impluse_bullet, ForceMode.Impulse);
            //AudioSource.PlayClipAtPoint(bulletAC, fire_gun_position.position, 1f);
            AudioSource.PlayClipAtPoint(bulletAC, Camera.main.transform.position, 1f);
            

            //生成火焰
            Instantiate(flame_prefab, fire_gun_position.position, fire_gun_position.rotation);
            currentBullet -= 1;
            //Debug.Log("剩余子弹" + currentBullet);
            yield return new WaitForSeconds(0.1f);
        }
        isFiring = false;

    }
    /// <summary>
    /// 装弹
    /// </summary>
    /// 
    public IEnumerator Reload(float CD)
    {
        //Debug.Log("子弹装填中");
        isReloading = true;
        yield return new WaitForSeconds(CD);
        currentBullet = maxBullet;
        isFiring = false;
        isReloading = false;
        //Debug.Log("子弹装填完毕");
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }

    //todo ,注意销毁协程
    private void OnDisable()
    {
        StopCoroutine("FireGun");
        isFiring = false;

    }



    public void TakeDamage(float damage)
    {
        Debug.Log("受到伤害:" + damage);
        if (currentHp > 0 && hpSlider)
        {
            currentHp -= damage;
            hpSlider.value = currentHp / hpMax;
        }     


        PlayHitSound();
    }

    public void PlayHitSound()
    {

    }
    /// <summary>
    /// 返回射线检测检测到了什么
    /// </summary>
    /// <returns></returns>
    protected string detectedWhat()
    {

        ray = new Ray(rayPoint.position, rayPoint.forward);
        RaycastHit hit;
        bool ishit = Physics.Raycast(ray, out hit);
        if (ishit)
        {
           // Debug.Log("检测到了什么-->" + hit.collider.tag);
            return hit.collider.tag;
        }
        else
            return "nothing";

    }
    private void OnEnable()
    {
        StopAllCoroutines();
    }

}
