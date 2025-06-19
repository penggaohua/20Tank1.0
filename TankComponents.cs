using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using System;

public class TankComponents : MonoBehaviour
{

    public TankComponent[] tankComponents;
    // Start is called before the first frame update
    void Awake()
    {

        InitInfo();

    }

    // Update is called once per frame
    void InitInfo()
    {
        string js = File.ReadAllText(Application.dataPath + "/Data/tankAttribute.json");
        print(js);
        JsonData jsData = JsonMapper.ToObject(js);
        for (int i = 0; i < tankComponents.Length; i++)
        {
          //  Debug.Log(jsData[i]["NAME"].ToString());
            tankComponents[i].ComponentNameEN = jsData[i]["NAME"].ToString();
            tankComponents[i].ComponentName = jsData[i]["NAME_CN"].ToString();
            tankComponents[i].Lv = int.Parse(jsData[i]["LV"].ToString());
            tankComponents[i].Desc = jsData[i]["DESC"].ToString();
            tankComponents[i].InitValue = float.Parse(jsData[i]["INITVALUE"].ToString());
            tankComponents[i].Increase = float.Parse(jsData[i]["INCREASE"].ToString());
            tankComponents[i].SetName();
        }

    }

    
}
