using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // 開始時X軸座標
    private float startPosX;
    // 是否已經死亡
    private bool isTrigger = false;
    // 動畫
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // 開始時X軸的座標
        startPosX = transform.position.x;
        // 獲取動畫組件
        animator = GetComponent<Animator>();
        // 讓音樂管理器查背景音樂
        AudioManager.Instance.CheckBG();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 碰撞進入 只有本体下方的是觸發器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 碰到玩家
        if (collision.gameObject.tag == "Player" && isTrigger == false)
        {
            // 播放改变動畫
            animator.SetTrigger("isBox");
            // 播放第三個音效
            AudioManager.Instance.Play(3);
            // UI 金幣數量增加1
            UIManager.Instance.GoldNum += 3;
            // 已經触发
            isTrigger = true;
        }
    }
}
