using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DieLine : MonoBehaviour
{
    // 動畫
    private Animator animator;
    // 方向 1:上面 -1:下面
    private int dir = 1;
    // 開始時X軸座標
    private float startPosX;
    // 開始時Y軸座標
    private float startPosY;
    

    // Start is called before the first frame update
    void Start()
    {
        // 獲取動畫組件
        animator = GetComponent<Animator>();
        // 開始時X軸的座標
        startPosX = transform.position.x;
        // 開始時Y軸的座標
        startPosX = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * 0.8f * dir);
        // 如果Y軸超過了 開始座標Y
        if (transform.position.y > startPosY + 0.4f)
        {
            // 方向相反
            dir *= -1;
        }
        // 如果Y軸超過了 開始座標Y-0.7
        else if (transform.position.y < startPosY - 0.1f)
        {
            // 方向相反
            dir *= -1;
        }
    }
}
