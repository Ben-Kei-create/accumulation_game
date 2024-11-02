using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ruleScript : MonoBehaviour
{
    // キューブがクリックされたときに呼ばれるメソッド
    void OnMouseDown()
    {
        // "RuleScene" シーンに移動
        SceneManager.LoadScene("RuleScene");
    }
}