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

    public GachaParams gachaParameter;
    public Text tenjoText;

    private bool tenjoFlag = false;

    public void UpdateUI()
    {
        if (tenjoFlag)
        {
            tenjoText.text = $"";
        }
        else
        {
            tenjoText.text = $"あと{gachaParameter.tenjou - DataManager.GetGachaCount()}回でSSSR確定！！！";
        }
    }

    public bool CheckTenjo()
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
