using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject bulletExplosionPrefab;
    public GameObject sparkPrefab;
    [SerializeField]
    protected float bulletLifeTime = 10f;
    // public AudioClip explodeAC;


    public float damage = 100f;
    public GameObject bloodPrefab;
    public GameObject blood2Prefab;

    private Camera camera;
    protected AudioSource bulletAS;
    //暴击率
    public float doubleRate = 0.3f;

    private bool isDouble = false;
    //飘字特效
    GameObject bloodGO;

    void Awake()
    {
        bulletAS = GetComponent<AudioSource>();
        camera = Camera.main;
    }

    private void FixedUpdate()
    {
        // transform.Translate(Vector3.forward*bulletSpeed*Time.deltaTime);
        // GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed);
    }






    private void OnEnable()
    {
        Invoke("ExplodeMyself", bulletLifeTime);
    }

    public void ExplodeMyself()
    {
        //Debug.Log("子弹销毁自己");
        //bulletAS.clip = explodeAC;
        //bulletAS.Play();
        //Debug.Log("播放爆炸声");
        Destroy(gameObject);
    }

    //伤害波动和暴击
    protected float GetDamage(float damage)
    {
        float actualDamge = damage * (1 + Random.Range(-0.2f, 0.2f));
        float rollpoint = Random.Range(0, 100) / 100.0f;
        if (rollpoint <= doubleRate)
        {
            actualDamge *= 2;
            isDouble = true;
        }
        actualDamge = (int)actualDamge;
        actualDamge = actualDamge < 0 ? 0 : actualDamge;
        return actualDamge;

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            float actualDamge = GetDamage(damage);
            // Debug.Log("打中的是:"+other.tag);
            other.SendMessage("TakeDamage", actualDamge, SendMessageOptions.DontRequireReceiver);
            if (other.tag == "Enemy" )
            {      
                //伤害飘字
                if (isDouble == false )
                {
                    // Instantiate(bloodPrefab, transform.position, Camera.main.transform.rotation);
                    bloodGO = Instantiate(blood2Prefab, transform.position, Camera.main.transform.rotation);
                    bloodGO.GetComponent<Text>().text = "-" + actualDamge.ToString();
                    bloodGO.transform.SetParent(other.transform);
                    Debug.Log("普通飘字");
                }
                else if(isDouble == true)//暴击
                {
                    //  Instantiate(blood2Prefab, transform.position, Camera.main.transform.rotation);
                    bloodGO = Instantiate(bloodPrefab, transform.position, Camera.main.transform.rotation);
                    bloodGO.GetComponent<Text>().text = "-" + actualDamge.ToString();
                    bloodGO.transform.SetParent(other.transform);
                    Debug.Log("暴击飘字");
                }
            }
            //生成火星
            Instantiate(sparkPrefab, transform.position, Quaternion.identity);

        }
        if (other.tag=="Die")
        {
            Instantiate(sparkPrefab, transform.position, Quaternion.identity);

        }
        //爆炸
        Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

  
}

