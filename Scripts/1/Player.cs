using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // 動畫組件
    private Animator animator;
    // 是否在地面
    private bool isOnGround = false;
    // 是否在Turn
    private bool isOnTurn = false;
    // 是否在Box
    private bool isOnBox = false;
    // 开始游戏按钮
    [SerializeField] GameObject playButton;

    // 开始按钮
    [SerializeField] GameObject ReplayButton;

    // 一開始就執行
    void Start()
    {
        // 獲取動畫組件
        animator = GetComponent<Animator>();

        // 讓音樂管理器查背景音樂
        AudioManager.Instance.CheckBG();
    }

    // 每一幀都執行一次
    void Update()
    {
        // 獲取玩家左右鍵的值 -1, 0, 1
        float h = Input.GetAxis("Horizontal");
        // 如果不等於零, 意味著玩家有移動
        if (h != 0)
        {
            // 移動 方向 * 上一幀花費的時間 * 玩家按鍵
            transform.Translate(transform.right * Time.deltaTime * h * 4);
            // 如果往右
            if (h > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (h < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            // 跑步動畫
            animator.SetBool("run", true);
        }

        // 玩家沒移動
        else
        {
            // 退出跑步
            animator.SetBool("run", false);
        }

        // 如果在地面 同時按w 跳躍
        if (isOnGround && Input.GetKeyDown(KeyCode.W) || isOnGround && Input.GetKeyDown(KeyCode.UpArrow) || isOnGround && Input.GetKeyDown(KeyCode.Space) || 
        isOnTurn && Input.GetKeyDown(KeyCode.W) || isOnTurn && Input.GetKeyDown(KeyCode.UpArrow) || isOnTurn && Input.GetKeyDown(KeyCode.Space) || 
        isOnBox && Input.GetKeyDown(KeyCode.W) || isOnBox && Input.GetKeyDown(KeyCode.UpArrow) || isOnBox && Input.GetKeyDown(KeyCode.Space))
        {
            // 給組件加上一個向上的力
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 180 * 2);
            // 讓音效管理器產生第二個音效
            AudioManager.Instance.Play(1);
        }
    }
    // 碰撞進入則調用
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // 如果碰到遊戲物體 標籤是Ground
        if (collision.gameObject.tag == "Ground")
        {
            // 關閉跳躍動畫
            animator.SetBool("jump", false);
            // 當前在地板
            isOnGround = true;
        }

        // 如果碰到遊戲物體 Turn
        if (collision.gameObject.tag == "Turn")
        {
            // 關閉跳躍動畫
            animator.SetBool("jump", false);
            // 當前在Turn
            isOnTurn = true;
        }

        // 如果碰到遊戲物體 Box
        if (collision.gameObject.tag == "Box")
        {
            // 關閉跳躍動畫
            animator.SetBool("jump", false);
            // 當前在Turn
            isOnBox = true;
        }

        // 如果碰到的是敵人
        else if (collision.gameObject.tag == "enemy1")
        {
            // UI 生命减1
            UIManager.Life.LifeNum -= 1;

            if (UIManager.Life.LifeNum <= 0)
            {
                // 播放死亡動畫
                animator.SetTrigger("dead");
                // 添加一個向上的動力
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
                // 銷毀碰撞體
                Destroy(GetComponent<CapsuleCollider2D>());
                // 播放第一個背景音效 並去掉背景音樂
                AudioManager.Instance.Play(0, true);
                // 3.5秒後觸發遊戲結束
                Invoke("GameOver", 3.5f);
            }
            else
            {
                AudioManager.Instance.Play(4);
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * -100);
            }
        }

        // 如果碰到的是死亡线
        else if (collision.gameObject.tag == "DieLine")
        {
            // 播放死亡動畫
            animator.SetTrigger("dead");
            // 添加一個向上的動力
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            // 銷毀碰撞體
            Destroy(GetComponent<CapsuleCollider2D>());
            // 播放第一個背景音效 並去掉背景音樂
            AudioManager.Instance.Play(0, true);
            // 3.5秒後觸發遊戲結束
            Invoke("GameOver", 3.5f);
        }
    }

    //  遊戲結束
    public void GameOver()
    {
        Time.timeScale = 0f;
        ReplayButton.SetActive(true);
    }

    //  重新开始
    public void Replay()
    {
        Time.timeScale = 1f;
        // 重新加載當前場景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 碰撞退出則調用
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 如果碰撞離開的遊戲物體 標籤是Ground
        if (collision.gameObject.tag == "Ground")
        {
            // 播放跳躍動畫
            animator.SetBool("jump", true);
            // 不在地面
            isOnGround = false;
        }

        // 如果碰撞離開的遊戲物體 標籤是Turn
        if (collision.gameObject.tag == "Turn")
        {
            // 播放跳躍動畫
            animator.SetBool("jump", true);
            // 不在地面
            isOnTurn = false;
        }

        // 如果碰撞離開的遊戲物體 標籤是Box
        if (collision.gameObject.tag == "Box")
        {
            // 播放跳躍動畫
            animator.SetBool("jump", true);
            // 不在地面
            isOnBox = false;
        }
    }

    // 觸發進入則調用
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果觸發遊戲標籤是 Gold
        if (collision.gameObject.tag == "Gold")
        {
            // UI 金幣數量增加1
            UIManager.Instance.GoldNum += 1;
            // 銷毀金幣
            Destroy(collision.gameObject);
            // 播放第四個音效
            AudioManager.Instance.Play(3);
        }

        // 如果觸發遊戲標籤是 flower
        else if (collision.gameObject.tag == "flower")
        {
            // UI 生命數量增加1
            UIManager.Instance.LifeNum += 1;
            // 銷毀花朵
            Destroy(collision.gameObject);
            // 播放第五個音效
            AudioManager.Instance.Play(5);
        }

        // 觸發勝利檢測對象
        else if (collision.gameObject.tag == "WinObject")
        {
            // 暂停背景音樂
            AudioManager.Instance.MusicStop();

            // 得到當前是第幾關
            int currLV = SceneManager.GetActiveScene().buildIndex;

            // 當前關卡是最後一關
            if (currLV == SceneManager.sceneCountInBuildSettings - 1)
            {
                // 去第一關
                SceneManager.LoadScene(0);
            }
            else
            {
                // 去下一關
                SceneManager.LoadScene(currLV + 1);
            }
        }
    }
}
