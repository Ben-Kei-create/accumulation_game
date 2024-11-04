using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    // X座標の範囲
    private float minX = -2.3f;
    private float maxX = 2.3f;
    // 移動速度
    public float speed = 5f;

    private Rigidbody2D rb; // Rigidbody2Dを保持する変数
    private bool isFalling = false; // 重力が有効になったかを判定するフラグ

    // 真下に落ちる速度を設定する変数（インスペクターから調整可能）
    public float fallSpeed = 1f; // Gravity Scaleの値

    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        // 初期表示時に重力を無効化（Gravity Scaleを0に設定）
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }

    void Update()
    {
        // エンターキーが押されたときに重力を有効化し、真下に落ちる設定
        if (Input.GetKeyDown(KeyCode.Return) && rb != null && !isFalling)
        {
            isFalling = true;
            rb.gravityScale = fallSpeed; // Gravity ScaleをfallSpeedに設定して真下に落下
            rb.velocity = Vector2.zero; // 速度をリセット
        }

        // 重力が有効になっていない間のみ左右の移動を行う
        if (!isFalling)
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
}
