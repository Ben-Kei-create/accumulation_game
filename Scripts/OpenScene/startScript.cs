using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // キューブがクリックされたときに呼ばれるメソッド
    void OnMouseDown()
    {
        // "ReadyScene" シーンに移動
        SceneManager.LoadScene("ReadyScene");
    }
}