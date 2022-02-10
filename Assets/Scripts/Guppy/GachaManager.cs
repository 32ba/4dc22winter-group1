using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class GachaWeight
{
    public GachaItem item;
    public int weight;
}

public enum GachaState
{
    HOME,
    START,
    SHOW_RESULT,
    FINISH
}

public class GachaManager : MonoBehaviour
{
    public List<GachaWeight> gachaList;

    public GachaResultUI resultUI;
    public GachaAnimation gachaAnimator;

    public GameObject gachaResultUIObject;
    public GameObject gachaVideoObject;
    public GameObject gachaHomeUIObject;

    public VideoPlayer gachaVideoPlayer;

    private Gacha gacha;
    private GachaState gachaState;

    private bool doSkip = false;
    private List<GachaItem> gachaResults;

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
            if (gachaVideoPlayer.isPlaying == false)
            {
                newState = GachaState.SHOW_RESULT;
            }
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            if (!gachaAnimator.IsAnimPlaying() || doSkip)
            {
                doSkip = false;
                if (gachaAnimator.HasNext())
                {
                    gachaAnimator.Next();
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
            gachaAnimator.FinishAnimation();
            ShowGachaResult(resultUI, gachaResults);
        }
    }

    private void UpdateUI(GachaState state)
    {
        if(state == GachaState.HOME)
        {
            gachaResultUIObject.SetActive(false);
            gachaVideoObject.SetActive(false);
            gachaHomeUIObject.SetActive(true);
        }
        else if(state == GachaState.FINISH)
        {
            gachaResultUIObject.SetActive(true);
            gachaVideoObject.SetActive(false);
            gachaHomeUIObject.SetActive(false);
        }
        else if(state == GachaState.START)
        {
            gachaResultUIObject.SetActive(false);
            gachaVideoObject.SetActive(true);
            gachaHomeUIObject.SetActive(false);
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            gachaResultUIObject.SetActive(false);
            gachaVideoObject.SetActive(false);
            gachaHomeUIObject.SetActive(false);
        }
    }

    private void UpdateInput(GachaState state)
    {
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

        ChangeState(GachaState.START);
        gachaVideoPlayer.Play();
    }

    private void SetUpGachaResult(List<GachaItem> results)
    {
        gachaResults = results;

        foreach (GachaItem item in results)
        {
            gachaAnimator.AddGachaItem(item);
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
