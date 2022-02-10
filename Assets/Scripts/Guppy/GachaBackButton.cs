using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaBackButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (GameClearManager.instance.IsGameClear())
        {
            GameClearManager.instance.GameClearEvent();
        }
        else
        {
            BackToMenu();
        }
    }

    void BackToMenu()
    {
        // メニューに戻る処理
    }
}
