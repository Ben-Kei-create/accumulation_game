using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    private float minX = -2.3f;
    private float maxX = 2.3f;
    public float speed = 5f;

    private Rigidbody2D rb;
    private bool isFalling = false;

    public float fallSpeed = 1f; // Gravity Scaleの値

    private heightTracker heightTracker; // heightTrackerへの参照

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
        // heightTrackerを取得
        heightTracker = FindObjectOfType<heightTracker>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && rb != null && !isFalling)
        {
            isFalling = true;
            rb.gravityScale = fallSpeed; 
            rb.velocity = Vector2.zero; 
        }

        if (!isFalling)
        {
            float input = Input.GetAxis("Horizontal");
            Vector3 position = transform.position;
            position.x += input * speed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, minX, maxX);
            transform.position = position;
        }
    }

void OnCollisionEnter2D(Collision2D collision)
{
    // Square, Square2, Square3に衝突したかチェック
    if (collision.gameObject.CompareTag("Square") || 
        collision.gameObject.CompareTag("Square2") || 
        collision.gameObject.CompareTag("Square3"))
    {
        // 衝突したときの高さを取得
        float currentHeight = transform.position.y;
        
        Debug.Log("Squareに衝突しました。高さ: " + currentHeight); // 追加

        // heightTrackerのUpdateHeightメソッドを呼び出して最高地点を更新
        if (heightTracker != null)
        {
            heightTracker.UpdateHeight(currentHeight);
        }
    }

    // 落下したボールとの衝突を検知
    if (collision.gameObject.CompareTag("Ball"))
    {
        // 衝突したボールの高さを取得
        float currentHeight = transform.position.y;
        
        Debug.Log("Ballに衝突しました。高さ: " + currentHeight); // 追加

        // heightTrackerのUpdateHeightメソッドを呼び出して最高地点を更新
        if (heightTracker != null)
        {
            heightTracker.UpdateHeight(currentHeight);
        }
    }
}


    public void ResetBall(Vector3 spawnPosition)
    {
        isFalling = false;
        if (rb != null)
        {
            rb.gravityScale = 0f;
            transform.position = spawnPosition;
        }
    }
}