using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // ���j���[�ɖ߂鏈��
        SceneManager.LoadScene("Home");
    }
}
