using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    // 單例模式
    public static UIManager Instance;
    public static UIManager Life;
    // 金幣UI 文本組件
    private Text goldText;
    // 當前金幣數量
    private int goldNum;

    // 生命 文本组件
    public Text lifeText;
    // 当前生命数量
    public int lifeNum = 3;

    // 金幣數量的屬性 修改時會自動更新UI
    public int GoldNum
    {
        get => goldNum;
        set
        {
            // 修改當前金幣數量
            goldNum = value;
            // 更新UI的文本提示
            goldText.text = "X" + goldNum;
        }
    }
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
        Instance = this;
        Life = this;
        // 金幣的Text組件查找
        goldText = transform.Find("Gold/Text").GetComponent<Text>();
        lifeText = transform.Find("Heart/Text").GetComponent<Text>();
    }
}

