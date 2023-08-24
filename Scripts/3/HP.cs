using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{

    [SerializeField] GameObject BossHP;
    // 是否死亡
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        // 讓音樂管理器查背景音樂
        AudioManager3.Instance.CheckBG();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 碰到玩家
        if (collision.gameObject.tag == "Player")
        {
            // 播放第三個音效
            AudioManager3.Instance.Play(2);

            UIBoss.Life.LifeNum -= 1;
            if (UIBoss.Life.LifeNum <= 0)
            {
                isDead = true;
                if (isDead)
                {
                    BossHP.SetActive(false);
                }
                else
                {
                    BossHP.SetActive(true);
                }
            }
        }
    }
}
