using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController : MonoBehaviour
{
    // X座標の範囲
    private float minX = -1.8f;
    private float maxX = 2.8f;
    // 移動速度
    public float speed = 5f;

    void Update()
    {
        // 左右の入力を取得
        float input = Input.GetAxis("Horizontal");

        // 現在位置を取得
        Vector3 position = transform.position;

        // X座標を移動
        position.x += input * speed * Time.deltaTime;

        // X座標が範囲内に収まるように制限
        position.x = Mathf.Clamp(position.x, minX, maxX);

        // 新しい位置に設定
        transform.position = position;
    }
}