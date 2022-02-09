using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
 
    // シングルトンにする
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (GameManager)FindObjectOfType(typeof(GameManager));
 
                if (instance == null)
                {
                    Debug.LogError(typeof(GameManager) + "をアタッチしているGameObjectはありません");
                }
            }
 
            return instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        OnClickEscapeKey();
    }

    private void OnClickEscapeKey()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("アプリが終了しました");
            Application.Quit();
        }else{
            return;
        }
    }
}
