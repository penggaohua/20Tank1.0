using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;

public class TankInfo : MonoBehaviour
{
    public Text[] texts;
    // Start is called before the first frame update
    void Start()
    {
        UpdateInfo();
        GameManager.instance.LevelUpHandle += UpdateInfo;
    }

    // Update is called once per frame
    void Update()
    {       
    }
    public void UpdateInfo()
    {
      //  GameManager.instance.UpdateData();
        if (GameManager.instance.selectComponent)
        {
            if (GameManager.instance.selectComponent.Lv >10 || GameManager.instance.money - 100 < 0)
                return;
        }
        string js = File.ReadAllText(Application.dataPath + "/Data/tankAttribute.json");
        JsonData jsData = JsonMapper.ToObject(js);       
        texts[0].text = jsData[0]["DESC"].ToString() + ":" + GameManager.instance.maxSpeed;
        //print("坦克最大速度"+GameManager.instance.maxSpeed);
        //print("引擎等级"+GameManager.instance.engine_level);
        texts[1].text = jsData[1]["DESC"].ToString() + ":" + GameManager.instance.angleSpeed.ToString("F2");
        texts[2].text = jsData[2]["DESC"].ToString() + ":" + GameManager.instance.accelerate.ToString("F2");
        texts[3].text = jsData[3]["DESC"].ToString() + ":" + GameManager.instance.cannon_damage.ToString("F2");
        texts[4].text = jsData[4]["DESC"].ToString() + ":" + GameManager.instance.gun_damage.ToString("F2");
        texts[5].text = jsData[5]["DESC"].ToString() + ":" + GameManager.instance.rocket_damage.ToString("F2");
       // Debug.Log("更新坦克信息");
    }

    private void OnDisable()
    {
        GameManager.instance.LevelUpHandle -= UpdateInfo;
    }
}
//maxSpeed += (engine_level-1) * maxSpeed_increase;
//angleSpeed +=(rotate_machine_level-1) * angleSpeed_increase;
//accelerate += (acclerate_mechine_level-1) * accelerate_increase;
//cannon_damage += (gun_level-1) * gun_damage_increase;
//rocket_damage += (rocket_level-1) * rocket_damage_increase;
