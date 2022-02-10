using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearManager : MonoBehaviour
{
    public static GameClearManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // 裏エンドに移動するか(チュートリアルシーンのみこれをtrueにする)
    public bool isExtraEnd = false;

    private bool gameClear;

    public void SetGameClear(bool state)
    {
        gameClear = true;
    }

    public bool IsGameClear()
    {
        return gameClear;
    }

    public void GameClearEvent()
    {
        if (isExtraEnd)
        {
            Debug.Log("ExtraEND!!!");
            // 裏エンドに移動
        }
        else
        {
            Debug.Log("NormalEND!!!");
            // 通常エンドに移動
        }
    }
}
