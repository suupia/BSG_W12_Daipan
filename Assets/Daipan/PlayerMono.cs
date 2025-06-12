using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのHPと移動を管理するクラス
/// </summary>
public class PlayerMono : MonoBehaviour
{
    [SerializeField] GameObject playerObject;

    public float NormalHp
    {
        get { return _normalHp; }
        set { if (value >= 0) _normalHp = value; }
    }


    private float _moveSpeed;   // 移動速度
    private float _normalHp;    // 現在のHP

    // transformを操作して移動させる
    public void Move(float moveAmount)
    {
        Vector2 moveDir = CalculateDirection() * moveAmount * _moveSpeed;   // 移動方向 x 移動量 x 移動速度
        playerObject.transform.position += new Vector3(moveDir.x, moveDir.y, 0);
    }

    // playerの移動方向を取得
    private Vector2 CalculateDirection()
    {
        return playerObject.transform.forward;
    }
}
