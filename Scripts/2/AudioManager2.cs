using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    // 單例模式
    public static AudioManager2 Instance;
    // 音效撥放器組件
    private AudioSource audioSource;
    // 音效片段樹組
    // 0死亡 1跳 2踩敵人 3吃金幣 4叫声 5增加生命
    public AudioClip[] audioClips;

    // 一開始就執行
    void Awake()
    {
        // 確定場景是否存在音效管理器
        // 如果沒有
        if (Instance == null)
        {
            // 標誌自身是
            Instance = this;
            // 讓Unity 切換場景時銷毀自身
            DontDestroyOnLoad(gameObject);

            // 查找音效播放器組件
            audioSource = GetComponent<AudioSource>();
        }
        // 如果沒有
        else
        {
            // 說明是多餘的 銷毀自己
            Destroy(gameObject);
        }
    }

    // 檢查背景音樂
    public void CheckBG()
    {
        // 如果背景沒有在播放中
        if (audioSource.isPlaying == false)
        {
            // 播放背景音樂
            audioSource.Play();
        }
    }

    public void MusicStop()
    {
        // 暂停背景音樂
        audioSource.Stop();
    }

    // 檢查背景音樂 播放第幾個 是否要關掉背景音樂
    public void Play(int index, bool bgOver = false)
    {
        // 如果需要關閉背景音樂
        if (bgOver)
        {
            // 停止背景音樂
            audioSource.Stop();
        }
        // 播放一次指定的音效
        audioSource.PlayOneShot(audioClips[index]);
    }

}
