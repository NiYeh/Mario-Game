using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemys3 : MonoBehaviour
{
    // 取到sprite renderer的引用
    private SpriteRenderer sprite;
    // 動畫
    private Animator animator;
    // 方向 1:右邊 -1:左邊
    private int dir = 1;
    // 開始時X軸座標
    private float startPosX;
    // 是否已經死亡
    public bool isDie = false;

    // 一開始就執行
    void Start()
    {
        // // 取到sprite renderer
        sprite = GetComponent<SpriteRenderer>();
        // 獲取動畫組件
        animator = GetComponent<Animator>();
        // 開始時X軸的座標
        startPosX = transform.position.x;
    }

    // 每一幀都執行一次
    void Update()
    {
        // 如果已經死亡
        if (isDie)
        {
            // 甚麼都不做 直接跳出Update 後面的都不執行
            return;
        }

        // 到這裡說明沒死

        // 通過方向位移
        transform.Translate(transform.right * Time.deltaTime * 0.8f * dir);

        // 如果X軸超過了 開始座標X+0.7
        if (transform.position.x > startPosX + 50f)
        {
            // 方向相反
            dir *= -1;

            if(dir == -1)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            
        }
        // 如果X軸超過了 開始座標X-0.7
        else if (transform.position.x < startPosX - 50f)
        {
            // 方向相反
            dir *= -1;

            if(dir == -1)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
    }

    // 碰撞進入 身體部分是碰撞體
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground" || collision.gameObject.tag == "Turn")
        {
            // 方向相反
            dir *= -1;

            if(dir == -1)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
    }

    // 碰撞進入 只有頭上頂著的是觸發器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 碰到玩家
        if (collision.gameObject.tag == "Player")
        {
            // 播放死亡動畫
            animator.SetTrigger("dead");
            // 播放第三個音效
            AudioManager3.Instance.Play(2);
            // 往下偏移 配合死亡動畫
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.0387f, 0);
            // 已經死亡
            isDie = true;

            BoxCollider2D[] collider2Ds;
            // 得到所有的碰撞體
            collider2Ds = GetComponents<BoxCollider2D>();
            // 循環所有的碰撞體
            foreach (var item in collider2Ds)
            {
                // 挨個銷毀
                Destroy(item);
            }
            // 銷毀個體
            Destroy(GetComponent<Rigidbody2D>());
            // 1秒後銷毀自己
            Destroy(gameObject, 1F);
        }
    }
}
