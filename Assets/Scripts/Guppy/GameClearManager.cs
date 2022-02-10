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

    // ���G���h�Ɉړ����邩(�`���[�g���A���V�[���݂̂����true�ɂ���)
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
            // ���G���h�Ɉړ�
        }
        else
        {
            Debug.Log("NormalEND!!!");
            // �ʏ�G���h�Ɉړ�
        }
    }
}
