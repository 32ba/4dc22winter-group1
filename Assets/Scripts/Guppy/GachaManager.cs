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
    IDLE,
    START,
    SHOW_RESULT,
}

public class GachaManager : MonoBehaviour
{
    public List<GachaWeight> gachaList;

    public GachaResultUI resultUI;
    public GachaAnimation gachaAnimator;

    public GameObject gachaResultUIObject;
    public GameObject gachaVideoObject;

    public VideoPlayer gachaVideoPlayer;

    private Gacha gacha;
    private GachaState gachaState;

    private bool doSkip = false;

    // Start is called before the first frame update
    void Start()
    {
        gacha = new Gacha();

        gachaState = GachaState.IDLE;

        foreach (GachaWeight weightData in gachaList)
        {
            gacha.RegisterItem(weightData.item, weightData.weight);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gachaState = UpdateAnimation(gachaState);

        UpdateUI(gachaState);
        UpdateInput(gachaState);
    }

    /*
     * ガチャアニメを一括して更新する
     * アニメステートに変更があればその値を返す
     */
    private GachaState UpdateAnimation(GachaState state)
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
                    gachaAnimator.FinishAnimation();
                    newState = GachaState.IDLE;
                }
            }
        }

        return newState;
    }

    private void UpdateUI(GachaState state)
    {
        if(state == GachaState.IDLE)
        {
            gachaResultUIObject.SetActive(true);
            gachaVideoObject.SetActive(false);
        }
        else if(state == GachaState.START)
        {
            gachaResultUIObject.SetActive(false);
            gachaVideoObject.SetActive(true);
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            gachaResultUIObject.SetActive(false);
            gachaVideoObject.SetActive(false);
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

        gachaState = GachaState.START;
        gachaVideoPlayer.Play();
    }

    private void SetUpGachaResult(List<GachaItem> results)
    {
        foreach (GachaItem item in results)
        {
            gachaAnimator.AddGachaItem(item);
        }

        resultUI.ShowResult(results);
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
