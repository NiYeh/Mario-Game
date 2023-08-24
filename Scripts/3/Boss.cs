using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // 動畫組件
    private Animator animator;
    // 取到sprite renderer的引用
    private SpriteRenderer sprite;
    // 方向 1:右邊 -1:左邊
    private int dir = 1;
    // 開始時X軸座標
    private float startPosX;
    // 是否已經被打败
    private bool isLose = false;
    // 障碍物
    [SerializeField] GameObject Obstacles;

    // 一開始就執行
    void Start()
    {
        // 取到sprite renderer
        sprite = GetComponent<SpriteRenderer>();
        // 獲取動畫組件
        animator = GetComponent<Animator>();

        // 讓音樂管理器查背景音樂
        AudioManager3.Instance.CheckBG();
    }

    // 每一幀都執行一次
    void Update()
    {
        // 通過方向位移
        transform.Translate(transform.right * Time.deltaTime * 1.5f * dir);

        // 如果X軸超過了 開始座標X+0.7
        if (transform.position.x > startPosX + 50f)
        {
            // 方向相反
            dir *= -1;

            if (dir == -1)
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

            if (dir == -1)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }

        int a = Random.Range(0, 1000);
        if (a == 777 && UIBoss.Life.LifeNum != 0)
        {
            animator.SetBool("Attack", true);
            if(UIBoss.Life.LifeNum < 3)
            {
                UIBoss.Life.LifeNum += 1;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        Lose();
    }

    // 碰撞進入 身體部分是碰撞體
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Ground" || collision.gameObject.tag == "Turn")
        {
            // 方向相反
            dir *= -1;

            if (dir == -1)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
        }
    }

    private void Lose()
    {
        if (UIBoss.Life.LifeNum <= 0)
        {
            isLose = true;
            // 如果被打败
            if (isLose)
            {
                dir = 0;
                animator.SetTrigger("isLose");

                // 查找游戏对象
                GameObject myObject = GameObject.Find("Boss");
                myObject.tag = "Untagged";

                Obstacles.SetActive(false);
            }
        }
    }
}
