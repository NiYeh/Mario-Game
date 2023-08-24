using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraC : MonoBehaviour
{
    // 玩家 面板賦值
    public Transform Player;
    // X軸 允許最右邊的座標
    public float MaxX;
    // X軸 允許最左邊的座標
    public float MinX;

    // Y軸 允許最右邊的座標
    public float MaxY;
    // Y軸 允許最左邊的座標
    public float MinY;

    // 每一幀都執行一次
    void Update()
    {
        // 跟隨玩家 但只跟隨X軸座標
        Vector3 newPos = new Vector3(Player.position.x, Player.position.y, transform.position.z);
        // 檢查有沒有超過邊界
        newPos.x = Mathf.Clamp(newPos.x, MaxX, MinX);
        newPos.y = Mathf.Clamp(newPos.y, MaxY, MinY);
        // 讓攝像機等於新座標
        transform.position = newPos;
    }
}
