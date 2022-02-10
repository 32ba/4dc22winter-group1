using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Live2D.Cubism.Rendering;

[System.Serializable]
public class GachaWeight
{
    public GachaItem item;
    public int weight;
}

public enum GachaState
{
    HOME,
    WAIT,
    START,
    SHOW_RESULT,
    FINISH
}

public class GachaManager : MonoBehaviour
{
    public List<GachaWeight> gachaList;

    public GachaResultUI resultUI;
    public GachaAnimation gachaResultAnimation;

    public GameObject gachaResultUIObject;
    public CubismRenderController gachaRenderController;
    public GameObject gachaHomeUIObject;

    public Animator gachaAnimAnimator;
    public float gachaAnimationTime = 1.0f;

    private Gacha gacha;
    private GachaState gachaState;

    private bool doSkip = false;
    private List<GachaItem> gachaResults;

    private float animationTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gacha = new Gacha();

        gachaState = GachaState.HOME;
        gachaResults = new List<GachaItem>();

        foreach (GachaWeight weightData in gachaList)
        {
            gacha.RegisterItem(weightData.item, weightData.weight);
        }

        OnChangeState(gachaState);
    }

    // Update is called once per frame
    void Update()
    {
        GachaState newState = UpdateState(gachaState);
        if(gachaState != newState)
        {
            OnChangeState(newState);
            gachaState = newState;
        }
        UpdateInput(gachaState);

        animationTime = Mathf.Max(animationTime - Time.deltaTime, 0.0f);
    }

    /*
     * ステートを強制的に変更する
     */
    private void ChangeState(GachaState newState)
    {
        OnChangeState(newState);
        gachaState = newState;
    }

    /*
     * ガチャアニメを一括して更新する
     * アニメステートに変更があればその値を返す
     */
    private GachaState UpdateState(GachaState state)
    {
        GachaState newState = state;

        if(state == GachaState.START)
        {
            if(animationTime <= 0.0f)
            {
                newState = GachaState.SHOW_RESULT;
            }
            /*
            if (gachaVideoPlayer.isPlaying == false)
            {
                newState = GachaState.SHOW_RESULT;
            }
            */
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            if (!gachaResultAnimation.IsAnimPlaying() || doSkip)
            {
                doSkip = false;
                if (gachaResultAnimation.HasNext())
                {
                    gachaResultAnimation.Next();
                }
                else
                {
                    newState = GachaState.FINISH;
                }
            }
        }

        return newState;
    }

    private void OnChangeState(GachaState state)
    {
        UpdateUI(state);
        if(state == GachaState.FINISH)
        {
            gachaResultAnimation.FinishAnimation();
            ShowGachaResult(resultUI, gachaResults);
        }
    }

    private void UpdateUI(GachaState state)
    {
        if(state == GachaState.HOME)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(true);
        }
        else if(state == GachaState.FINISH)
        {
            gachaResultUIObject.SetActive(true);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(false);
        }
        else if(state == GachaState.WAIT)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 1f;
            gachaHomeUIObject.SetActive(false);
            gachaAnimAnimator.SetBool("Start", false);
        }
        else if(state == GachaState.START)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 1f;
            gachaHomeUIObject.SetActive(false);

            animationTime = gachaAnimationTime;
            gachaAnimAnimator.SetBool("Start", true);
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(false);
            gachaAnimAnimator.SetBool("Start", false);
        }
    }

    private void UpdateInput(GachaState state)
    {
        if(state == GachaState.WAIT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeState(GachaState.START);
            }
        }
        if(state == GachaState.SHOW_RESULT)
        {
            if (Input.GetMouseButtonDown(0))
            {
                doSkip = true;
            }
        }
    }

    public void StartGacha(int count)
    {
        List<GachaItem> results = DoGacha(count);
        SetUpGachaResult(results);

        ChangeState(GachaState.WAIT);
    }

    private void SetUpGachaResult(List<GachaItem> results)
    {
        gachaResults = results;

        foreach (GachaItem item in results)
        {
            gachaResultAnimation.AddGachaItem(item);
        }
    }

    private void ShowGachaResult(GachaResultUI resultUI, List<GachaItem> items)
    {
        resultUI.Clear();

        foreach(GachaItem item in items)
        {
            resultUI.AddResult(item);
        }
    }

    private List<GachaItem> DoGacha(int count)
    {
        List<GachaItem> gachaResults = new List<GachaItem>();

        for (int i = 0; i < count; i++)
        {
            GachaItem result = gacha.GetResult();
            gachaResults.Add(result);
        }

        return gachaResults;
    }
}
