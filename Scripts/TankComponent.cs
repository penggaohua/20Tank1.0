using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankComponent : MonoBehaviour
{
  
    string componentName;
    public   string ComponentName{get{ return componentName; }set{ componentName = value; } }

    string componentNameEN;
    public string ComponentNameEN { get { return componentNameEN; } set { componentNameEN = value; } }
    int lv;
    public int Lv { get { return lv; } set { lv = value; } }
    string desc;
    public string Desc { get { return desc; } set { desc = value; } }
    float increase;

    public float Increase { get{ return increase; } set { increase = value; } }

    float initValue;
    public float InitValue { get { return initValue; } set { initValue = value; } }



    private void Start()
    {
       // GameManager.instance.LevelUpHandle += updateLevel;
    }

    // Start is called before the first frame update
    public void SetName()
    {
        transform.Find("name_text").GetComponent<Text>().text = componentName;
        transform.Find("lv_text").GetComponent<Text>().text ="lv"+lv;
    }
    //更新等级信息
    public void updateLevel()
    {

        if (lv >= 10)
        {
            UIManager.instance.PopTips("升级失败，已是最高等级");
            return;
        }
        if (GameManager.instance.money - 100 < 0)
        {
            UIManager.instance.PopTips("金币不足");
        }
        else
        {
            GameManager.instance.money -= 100;
            UIManager.instance.moneyText.text = (GameManager.instance.money).ToString();
            GameManager.instance.updateLeve(componentNameEN);
            lv = lv + 1;
            transform.Find("lv_text").GetComponent<Text>().text = "lv" + (lv);
           // GameManager.instance.UpdateData();//更新各项数值
            UIManager.instance.PopTips("升級成功");
           // Debug.Log(ComponentName + "升级成功");
                    
        }

    }


    public void GetInstanceAndOpen()
    {
      //  Debug.Log("点击了这个" + this);
        GameManager.instance.selectComponent = this;
        UIManager.instance.OpenLevelup();
        UIManager.instance.DestroyTips();
    }

    private void OnDestroy()
    {
        UIManager.instance.DestroyTips();
     }

}
