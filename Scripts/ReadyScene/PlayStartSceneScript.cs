using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // これを追加

public class PlayStartSceneScript : MonoBehaviour
{
    void OnMouseDown()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
