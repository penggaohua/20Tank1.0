using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject tips;
    public Transform tipsPosition;

    public GameObject levelupPanel;
    int engine;
    public Text engin_lv_text;
    // Start is called before the first frame update


    //面板
    public Text left_title;
    public Text left_desc;



    public Text right_title;
    public Text right_desc;
    public Text moneyText;

    private GameObject Tips;
    private List<GameObject> tipsList;
    private TankComponent currentTankComponent;
    
    private void Awake()
    {
        instance = this;
        levelupPanel.SetActive(false);
        GameManager.instance.LevelUpHandle += UpdateData;
        moneyText.text = GameManager.instance.money.ToString();
    }


    //打开升级面板
    public void OpenLevelup()
    {
        levelupPanel.SetActive(true);
        UpdateData();
    }
    //关闭升级面板
    public void CloseLevelup()
    {
       levelupPanel.SetActive(false);
    }

    void UpdateData()
    {

        int level = GameManager.instance.selectComponent.Lv;
        float initValue = GameManager.instance.selectComponent.InitValue;
        string desc = GameManager.instance.selectComponent.Desc;
        string name = GameManager.instance.selectComponent.ComponentName;
        float increase = GameManager.instance.selectComponent.Increase;

        //升级界面的左边
        left_title.text= name +"lv"+level;
       // print("初始值" + initValue);
        left_desc.text = desc + ":" + (initValue + (level - 1) * increase);

        //升级界面的右边
        if(GameManager.instance.selectComponent.Lv>=10)
        {
            right_title.text = name + "lv:MAX";
            right_desc.text = desc + ": MAX"; 
        }
        else
        {
            right_title.text = name + "lv" + (level + 1);
            right_desc.text = GameManager.instance.selectComponent.Desc + ":"+(initValue + level*increase).ToString("f2");     
        }     
    }

    public void PopTips(string message)
    {
        tips.GetComponent<Text>().text=message;
        Tips = Instantiate(tips, tipsPosition.position, tipsPosition.rotation);
        Tips.transform.parent = tipsPosition;
      //  Debug.Log("弹出tips");
        currentTankComponent = GameManager.instance.selectComponent;
    }
    private void OnDisable()
    {
        GameManager.instance.LevelUpHandle -= UpdateData;
    }
    public void DestroyTips()
    {
        if (currentTankComponent != GameManager.instance.selectComponent)
            Destroy(Tips);


        //todo 销毁多个tips
        //切换界面时销毁tips
        //if (currentTankComponent != GameManager.instance.selectComponent)
        //{
        //    if (tipsList.Count>0)
        //    {
        //        for(int i = 0; i<tipsList.Count;i++)
        //            Destroy(tipsList[i]);
        //    }
        //}
    }
}
