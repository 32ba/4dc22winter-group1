using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenjoManager : MonoBehaviour
{
    public static TenjoManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private bool tenjoFlag = false;

    public void UpdateUI(GachaParams gachaParameter, Text tenjoText)
    {
        if (tenjoFlag)
        {
            tenjoText.text = $"";
        }
        else
        {
            tenjoText.text = $"����{gachaParameter.tenjou - DataManager.GetGachaCount()}��Ŗ{�}�O���m��I";
        }
    }

    public bool CheckTenjo(GachaParams gachaParameter)
    {
        if(DataManager.GetGachaCount() >= gachaParameter.tenjou)
        {
            DataManager.AddGachaCount(-gachaParameter.tenjou);
            tenjoFlag = true;
            
            return true;
        }
        return false;
    }

    public bool IsHitTenjo()
    {
        return tenjoFlag;
    }
}
