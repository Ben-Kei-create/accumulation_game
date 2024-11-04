using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSpawner : MonoBehaviour
{
    public GameObject ballPrefab; // プレハブを設定する変数
    public Transform hand; // 手のTransformを設定する変数
    public heightTracker heightTracker; // heightTrackerへの参照を追加（カメラ移動に必要）

    void Update()
    {
        // エンターキーが押されたときに新しいボールを生成
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SpawnBallAfterDelay(1f)); // 1秒待ってボールを生成するコルーチンを開始
        }
    }

private IEnumerator SpawnBallAfterDelay(float delay)
{
    // 指定された時間待機
    yield return new WaitForSeconds(delay);

    // handの位置を取得
    if (hand == null)
    {
        Debug.LogError("Hand Transform is not set!");
        yield break; // 手のTransformがnullの場合は処理を中断
    }

    Vector3 handPosition = hand.position;

    // Y座標を-5して新しい位置を計算
    Vector3 spawnPosition = new Vector3(handPosition.x, handPosition.y - 1.4f, handPosition.z);

    // プレハブから新しいボールを生成
    if (ballPrefab == null)
    {
        Debug.LogError("Ball prefab is not set!");
        yield break; // プレハブがnullの場合は処理を中断
    }

    GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    
    // ballControllerスクリプトのisFallingをfalseにして初期状態に設定
    ballController ballScript = newBall.GetComponent<ballController>();
    if (ballScript != null)
    {
        ballScript.ResetBall(spawnPosition); // 生成位置を引数として渡す
    }
}

}
