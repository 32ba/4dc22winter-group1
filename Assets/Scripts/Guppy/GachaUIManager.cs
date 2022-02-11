using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Rendering;

public class GachaUIManager : MonoBehaviour
{
    public GameObject gachaHomeUIObject;
    public GameObject gachaWaitUIObject;
    public GameObject gachaStartUIObject;
    public GameObject gachaShowResultUIObject;
    public GameObject gachaFinishUIObject;

    public GameObject skipButton;
    public GameObject retryButton;
    public GameObject backButtonHome;
    public GameObject backButtonFinish;

    public Text startGachaButtonText;
    public Text retryGachaButtonText;
    public Text tenjoText;
    public AudioSource gachaSE;
    public GachaResultUI resultItemUI;

    public void UpdateUI(GachaState state, GachaParams gachaParameter, bool canSkip = true, bool isTutorial = false, bool isGameClear = false)
    {
        gachaHomeUIObject.SetActive(false);
        gachaWaitUIObject.SetActive(false);
        gachaStartUIObject.SetActive(false);
        gachaShowResultUIObject.SetActive(false);
        gachaFinishUIObject.SetActive(false);

        if (state == GachaState.HOME)
        {
            gachaHomeUIObject.SetActive(true);

            startGachaButtonText.text = gachaParameter.GachaButtonText_Home();

            backButtonHome.SetActive(true);
            if (isTutorial)
            {
                backButtonHome.SetActive(false);
            }
        }
        else if (state == GachaState.WAIT)
        {
            gachaWaitUIObject.SetActive(true);
        }
        else if (state == GachaState.START)
        {
            gachaStartUIObject.SetActive(true);

            gachaSE.Play();
        }
        else if (state == GachaState.SHOW_RESULT)
        {
            gachaShowResultUIObject.SetActive(true);
            skipButton.SetActive(true);

            if (!canSkip)
            {
                skipButton.SetActive(false);
            }
        }
        else if (state == GachaState.FINISH)
        {
            gachaFinishUIObject.SetActive(true);

            startGachaButtonText.text = gachaParameter.GachaButtonText_Retry();

            retryButton.SetActive(true);
            backButtonFinish.SetActive(true);

            // クリア条件を満たしていたらリトライできない
            if (isGameClear)
            {
                retryButton.SetActive(false);
            }

            TenjoManager.instance.UpdateUI(gachaParameter, tenjoText);
        }
    }

    public void ShowGachaResult(List<GachaItem> items)
    {
        resultItemUI.Clear();

        foreach (GachaItem item in items)
        {
            resultItemUI.AddResult(item);
        }
    }
}
