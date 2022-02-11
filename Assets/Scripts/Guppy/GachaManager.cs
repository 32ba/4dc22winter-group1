using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Live2D.Cubism.Rendering;

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
    public GachaParams gachaParameter;
    // public GachaItem tenjoItem;
    // public int requirePoint = 1000;

    // public List<GachaWeight> gachaList;

    public GachaResultUI resultUI;
    public GachaAnimation gachaResultAnimation;

    public GameObject gachaResultUIObject;
    public CubismRenderController gachaRenderController;
    public GameObject gachaHomeUIObject;
    public GameObject skipButton;
    public GameObject retryButton;
    public GameObject backButton;
    public Text startGachaButtonText;
    public Text retryGachaButtonText;

    public Animator gachaAnimAnimator;
    public float gachaAnimationTime = 1.0f;

    public AudioSource gachaSE;

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

        foreach (GachaWeight weightData in gachaParameter.gachaData)
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
     * �X�e�[�g�������I�ɕύX����
     */
    private void ChangeState(GachaState newState)
    {
        OnChangeState(newState);
        gachaState = newState;
    }

    /*
     * �K�`���A�j�����ꊇ���čX�V����
     * �A�j���X�e�[�g�ɕύX������΂��̒l��Ԃ�
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
            skipButton.SetActive(false);
            backButton.SetActive(true);

            startGachaButtonText.text = gachaParameter.GachaButtonText_Home();
        }
        else if(state == GachaState.FINISH)
        {
            gachaResultUIObject.SetActive(true);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(false);
            skipButton.SetActive(false);
            backButton.SetActive(true);

            startGachaButtonText.text = gachaParameter.GachaButtonText_Retry();

            if (GameClearManager.instance.IsGameClear())
            {
                // �N���A�����𖞂������烊�g���C�ł��Ȃ��Ȃ�
                retryButton.SetActive(false);
            }

            TenjoManager.instance.UpdateUI();
        }
        else if(state == GachaState.WAIT)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 1f;
            gachaHomeUIObject.SetActive(false);
            gachaAnimAnimator.SetBool("Start", false);
            skipButton.SetActive(false);
            backButton.SetActive(false);
        }
        else if(state == GachaState.START)
        {
            // ����炷
            gachaSE.Play();

            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 1f;
            gachaHomeUIObject.SetActive(false);
            skipButton.SetActive(false);
            backButton.SetActive(false);

            animationTime = gachaAnimationTime;
            gachaAnimAnimator.SetBool("Start", true);
        }
        else if(state == GachaState.SHOW_RESULT)
        {
            gachaResultUIObject.SetActive(false);
            gachaRenderController.Opacity = 0f;
            gachaHomeUIObject.SetActive(false);
            gachaAnimAnimator.SetBool("Start", false);
            skipButton.SetActive(true);
            backButton.SetActive(false);
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
        if(!gachaParameter.CanPlay(DataManager.GetPoint()))
        {
            Debug.Log("�|�C���g������܂���");

            return;
        }
        DataManager.UsePoint(gachaParameter.requirePoint);

        List<GachaItem> results = DoGacha(count);
        SetUpGachaResult(results);

        ChangeState(GachaState.WAIT);
    }

    public void SkipGacha()
    {
        gachaResultAnimation.SkipAll();
        ChangeState(GachaState.FINISH);
    }

    private void SetUpGachaResult(List<GachaItem> results)
    {
        gachaResults = results;

        foreach (GachaItem item in results)
        {
            if (item.isGameClearItem)
            {
                GameClearManager.instance.SetGameClear(true);
            }
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
            DataManager.AddGachaCount(1);
            GachaItem result = gacha.GetResult();

            if(TenjoManager.instance.CheckTenjo())
            {
                // �K�`���V��A�C�e��
                result = gachaParameter.tenjouItem;
            }
            gachaResults.Add(result);
        }

        return gachaResults;
    }
}
