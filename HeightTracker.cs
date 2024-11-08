using System.Collections; // IEnumeratorを使用するために必要
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIを使うために必要

public class HeightTracker : MonoBehaviour
{
    public Text heightText; // テキストコンポーネントへの参照
    public GameObject hand; // 手のオブジェクトへの参照
    private float highestPoint = 0f; // 最高地点を保持する変数（初期値を0に設定）
    private const float squareYPosition = 0f; // SquareのY座標を基準に設定
    public Camera mainCamera; // MainCameraへの参照
    private float cameraMoveIncrement = 2f; // カメラ移動の増分
    private float lastCameraYPosition; // 最後にカメラを移動させたY座標
    public float moveSpeed = 1f; // スムーズ移動の速度

    public float offsetX = 289f; // X座標のオフセット
    public float offsetY = 883f; // Y座標のオフセット

    // Startメソッドで初期値を設定
    void Start()
    {
        lastCameraYPosition = mainCamera.transform.position.y; // 初期カメラ位置を記録
    }

    // 最高地点を更新するメソッド
    public void UpdateHeight(float newHeight)
    {
        // Squareの基準からの高さを計算
        float relativeHeight = newHeight - squareYPosition;

        if (relativeHeight > highestPoint)
        {
            highestPoint = relativeHeight; // 新しい最高地点を記録
        }

        // テキストを更新
        UpdateHeightText();

        // handのY座標と最高地点との差が1.5m以下になった時、カメラを移動
        if (Mathf.Abs(hand.transform.position.y - highestPoint) <= 1.5f &&
            mainCamera.transform.position.y <= lastCameraYPosition + cameraMoveIncrement)
        {
            StartCoroutine(MoveCameraAndHand());
        }
    }

private void UpdateHeightText()
{
    heightText.text = "最高地点: " + Mathf.Max(highestPoint, 0f).ToString("F2") + "m"; // 小数点以下2桁で表示

    // // テキストサイズを固定する
    // heightText.fontSize = 3; // 適切なサイズに設定（14は例）

    // // heightTextの位置をカメラの上に設定（オフセットを適用）
    Vector3 textPosition = heightText.transform.position;
    // textPosition.x = mainCamera.transform.position.x + offsetX; // カメラのX座標にオフセットを加える
    // textPosition.y = mainCamera.transform.position.y + offsetY; // カメラのY座標にオフセットを加える
    heightText.transform.position = textPosition;
}

    // カメラの移動を制御するメソッド
    private IEnumerator MoveCameraAndHand()
    {
        // 移動する先の位置を計算
        Vector3 targetCameraPosition = mainCamera.transform.position + new Vector3(0f, cameraMoveIncrement, 0f);
        Vector3 targetHandPosition = hand.transform.position + new Vector3(0f, cameraMoveIncrement, 0f);
        
        float elapsedTime = 0f;

        // スムーズに移動
        while (elapsedTime < moveSpeed)
        {
            // Lerpを使ってカメラと手の位置を更新
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCameraPosition, (elapsedTime / moveSpeed));
            hand.transform.position = Vector3.Lerp(hand.transform.position, targetHandPosition, (elapsedTime / moveSpeed));
            
            elapsedTime += Time.deltaTime;
            yield return null; // 次のフレームまで待機
        }

        // 最終位置を設定
        mainCamera.transform.position = targetCameraPosition;
        hand.transform.position = targetHandPosition;

        lastCameraYPosition = targetCameraPosition.y; // 最後のカメラY座標を更新
    }
}
