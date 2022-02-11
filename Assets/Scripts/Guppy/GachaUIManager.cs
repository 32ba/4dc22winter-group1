using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Rendering;

public class GachaUIManager : MonoBehaviour
{
    public GameObject gachaResultUIObject;
    public GameObject gachaHomeUIObject;
    public GameObject skipButton;

    public GameObject retryButton;

    public GameObject backButton;
    public CubismRenderController gachaRenderController;
    public Animator gachaAnimAnimator;

    public Text startGachaButtonText;
    public Text retryGachaButtonText;
    public AudioSource gachaSE;

    public void UpdateUI(GachaState state, GachaParams gachaParameter)
    {
        if (state == GachaState.HOME)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(true);
            skipButton.SetActive(false);
            backButton.SetActive(true);

            startGachaButtonText.text = gachaParameter.GachaButtonText_Home();
        }
        else if (state == GachaState.FINISH)
        {
            gachaResultUIObject.SetActive(true);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(false);
            skipButton.SetActive(false);
            backButton.SetActive(true);

            startGachaButtonText.text = gachaParameter.GachaButtonText_Retry();

            if (GameClearManager.instance.IsGameClear())
            {
                // クリア条件を満たしたらリトライできなくなる
                retryButton.SetActive(false);
            }

            TenjoManager.instance.UpdateUI();
        }
        else if (state == GachaState.WAIT)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 1f;
            gachaHomeUIObject.SetActive(false);
            gachaAnimAnimator.SetBool("Start", false);
            skipButton.SetActive(false);
            backButton.SetActive(false);
        }
        else if (state == GachaState.START)
        {
            // 音を鳴らす
            gachaSE.Play();

            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 1f;
            gachaHomeUIObject.SetActive(false);
            skipButton.SetActive(false);
            backButton.SetActive(false);

            gachaAnimAnimator.SetBool("Start", true);
        }
        else if (state == GachaState.SHOW_RESULT)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(false);
            gachaAnimAnimator.SetBool("Start", false);
            skipButton.SetActive(true);
            backButton.SetActive(false);
        }
    }
}
