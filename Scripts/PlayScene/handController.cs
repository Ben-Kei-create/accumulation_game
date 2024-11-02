using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handController : MonoBehaviour
{
    // X座標の範囲
    private float minX = -1f;
    private float maxX = 3f;
    // 移動速度
    public float speed = 5f;

    // プレハブを保持する配列
    public GameObject[] prefabs; // 配列を使用して3種類のプレハブを保持

    private GameObject currentPrefab; // 現在表示されているプレハブ

    void Start()
    {
        // ゲーム開始時にランダムにプレハブを選択して表示
        DisplayRandomPrefab();
    }

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

        // Enterキーが押された場合、次のプレハブを表示
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DisplayRandomPrefab();
        }
    }

    private void DisplayRandomPrefab()
    {
        // 既に表示されているプレハブがあれば削除
        if (currentPrefab != null)
        {
            Destroy(currentPrefab);
        }

        // 新しいプレハブをランダムに選択
        int randomIndex = Random.Range(0, prefabs.Length);
        currentPrefab = Instantiate(prefabs[randomIndex], new Vector3(transform.position.x - 0.5f, 3.5f, transform.position.z), Quaternion.identity);
    }
}
