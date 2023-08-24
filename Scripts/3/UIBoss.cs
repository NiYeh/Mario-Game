using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIBoss : MonoBehaviour
{
    public static UIBoss Life;

    // 生命 文本组件
    public Text lifeText;
    // 当前生命数量
    public int lifeNum = 5;
    public int LifeNum
    {
        get => lifeNum;
        set
        {
            // 修改当前生命数量
            lifeNum = value;
            string v = "X" + lifeNum;
            lifeText.text = v;
        }
    }

    // 一開始就執行
    private void Awake()
    {
        Life = this;
        // 生命的Text組件查找
        lifeText = transform.Find("HP/Text").GetComponent<Text>();
    }
}

