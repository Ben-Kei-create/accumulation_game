using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private float minX = -2.3f;
    private float maxX = 2.3f;
    public float speed = 5f;

    private Rigidbody2D rb;
    private bool isFalling = false;

    public float fallSpeed = 1f; // Gravity Scaleの値
    private HeightTracker heightTracker; // HeightTrackerへの参照

    public GameObject ballBPrefab; // ballBのプレハブをアサイン
    public GameObject ballCPrefab; // ballCのプレハブをアサイン

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
        // HeightTrackerを取得
        heightTracker = FindObjectOfType<HeightTracker>();
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
            Debug.Log("Squareに衝突しました。高さ: " + currentHeight);

            // heightTrackerのUpdateHeightメソッドを呼び出して最高地点を更新
            if (heightTracker != null)
            {
                heightTracker.UpdateHeight(currentHeight);
            }
        }

        // Ballタグ同士の衝突でballB生成
        if (gameObject.CompareTag("Ball") && collision.gameObject.CompareTag("Ball"))
        {
            Vector3 midpoint = (transform.position + collision.transform.position) / 2;
            Instantiate(ballBPrefab, midpoint, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        // Ball2タグ同士の衝突でballC生成
        else if (gameObject.CompareTag("Ball2") && collision.gameObject.CompareTag("Ball2"))
        {
            Vector3 midpoint = (transform.position + collision.transform.position) / 2;
            Instantiate(ballCPrefab, midpoint, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
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
