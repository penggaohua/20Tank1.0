using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;

public class GameManager : MonoBehaviour
{
    //字段名：[lv ,值]
    public static GameManager instance;


    public int money = 350;

    #region Tank屬性
    public delegate void LevelUp();
    public event LevelUp LevelUpHandle;


    [Header("player属性")]
    public float maxHp = 1000;
    public float upAngle = 10f;
    public float downAngle = 16f;

    //最大速度
    public float maxSpeed ;
    public int engine_level = 1;
    private float maxSpeed_increase ;

    //炮塔角速度
    public float angleSpeed ;
    public int rotate_machine_level = 1;
    private float angleSpeed_increase ;


    //坦克加速度
    public float accelerate ;
    public int acclerate_mechine_level = 1;
    private float accelerate_increase ;



    //火炮伤害
    public float cannon_damage ;
    public int cannon_level = 1;
    private float cannon_damage_increase;



    //机枪伤害
    public float gun_damage ;
    public int gun_level = 1;
    private float gun_damage_increase ;


    //火箭伤害
    public float rocket_damage;
    public int rocket_level = 1;
    private float rocket_damage_increase ;
    #endregion
    public TankComponent selectComponent;



    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        string js = File.ReadAllText(Application.dataPath + "/Data/tankAttribute.json");
       // print(js);
        JsonData jsData = JsonMapper.ToObject(js);

        maxSpeed = float.Parse(jsData[0]["INITVALUE"].ToString());
        angleSpeed = float.Parse(jsData[1]["INITVALUE"].ToString());
        accelerate = float.Parse(jsData[2]["INITVALUE"].ToString());
        cannon_damage = float.Parse(jsData[3]["INITVALUE"].ToString());
        gun_damage = float.Parse(jsData[4]["INITVALUE"].ToString());
        rocket_damage = float.Parse(jsData[5]["INITVALUE"].ToString());

        maxSpeed_increase = float.Parse(jsData[0]["INCREASE"].ToString());
        angleSpeed_increase = float.Parse(jsData[1]["INCREASE"].ToString());
        accelerate_increase = float.Parse(jsData[2]["INCREASE"].ToString());
        cannon_damage_increase = float.Parse(jsData[3]["INCREASE"].ToString());
        gun_damage_increase = float.Parse(jsData[4]["INCREASE"].ToString());
        rocket_damage_increase = float.Parse(jsData[5]["INCREASE"].ToString());
    }

    //更新各项属性数据
    
    public void OnClickLeveUpButton()
    {
        Debug.Log(string.Format("<color=yellow>{0}</color>","当前选中的组件" + selectComponent));
        selectComponent.updateLevel();
        //更新lv
        //更新属性数值
        if (LevelUpHandle != null)
        {
            LevelUpHandle();
        }
        else
        {
            print("no handle");
        }
    }
    //更新对应的等级
    public  void updateLeve(string name)
    {
        Debug.Log(string.Format("<color=red>{0}</color>","传入的字段"+name));
        switch (name)
        {
            case "engine":
                engine_level += 1;
                maxSpeed +=  maxSpeed_increase;
                break;
            case "rotateMachine":
                rotate_machine_level += 1;
                angleSpeed += angleSpeed_increase;
                break;
            case "track":
                acclerate_mechine_level += 1;
                accelerate += accelerate_increase;
                break;
            case "cannon":
                cannon_level += 1;
                cannon_damage += cannon_damage_increase;
                break;
            case "gun":
                gun_level += 1;
                gun_damage += gun_damage_increase;
                break;
            case "rocket":
                rocket_level += 1;
                rocket_damage += rocket_damage_increase;
                break;
            default:
                Debug.Log("component名字不存在:"+ name);
                break;
        }
        Debug.Log("各属性等级" + engine_level + rotate_machine_level + acclerate_mechine_level);
    }
}




