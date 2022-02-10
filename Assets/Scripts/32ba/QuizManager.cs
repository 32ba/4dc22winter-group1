using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UniRx;
using _32ba;


public class QuizManager : MonoBehaviour
{
    public TextAsset questionFile;
    public GameObject choicesA;
    public GameObject choicesB;
    public GameObject choicesC;
    public GameObject choicesD;
    public GameObject correctTextObject;
    public GameObject incorrectTextObject;
    public GameObject afterAnsweringPanelObject;
    public GameObject timeLimitBarObject;
    public Image timeBarImage;
    public Text questionText;

    private readonly List<string[]> _questionData = new List<string[]>();
    private int _questionId;
    private bool _isAlreadyAnswered = false;
    private bool _isEnableTimer = false;
    private float _countTime = 0;
    private float _progress = 0;

    // Start is called before the first frame update
    void Start()
    {
        bool isQuestionCsvLoaded = CsvReader.Read(questionFile, _questionData, '	');//テキストファイルから問題をリストへ読み込み
        Debug.Log(isQuestionCsvLoaded ? "問題CSVの読み込みに成功しました" : "問題CSVの読み込みに失敗しました");
        _questionId = UnityEngine.Random.Range(0, _questionData.Count);//何問目を出題するかを決める
        SetQuestion(_questionData, _questionId);//問題文、回答を各テキストフィールドへ反映
        DelayAsync(1.0f, () => {
            choicesA.SetActive(true);
            choicesB.SetActive(true);
            choicesC.SetActive(true);
            choicesD.SetActive(true);
            timeLimitBarObject.SetActive(true);//回答ボタンとタイムリミットを表示するバーを表示
            _isEnableTimer = true;//タイマー有効化
        }).Forget();
    }

    private void Update()
    {
        if (_isEnableTimer)TimeLimitCounter(5.0f);//タイマーが有効な間、タイマーを実行
    }

    /// <summary>
    /// 回答ボタンを押した時に呼ばれる関数
    /// </summary>
    /// <param name="answer">押されたボタンに対応するキー</param>
    public void OnClickAnswerButton(string answer)
    {
        if (_isAlreadyAnswered) return;//すでに回答済みなら、その後はボタンを反応させないようにする
        _isAlreadyAnswered = true;
        _isEnableTimer = false;
        DelayAsync(1.0f, () => {afterAnsweringPanelObject.SetActive(true);}).Forget(); //次へ進むボタンを表示
        if (AnswerQuestion(_questionData, _questionId, answer))
        {
            //正解なら正しい答えをハイライトし、丸の記号を出し、ポイントを加算
            correctTextObject.SetActive(true);
            HighlightCorrectAnswer(_questionData, _questionId);
            DataManager.AddPoint(500);
        }
        else
        {
            //不正解なら正しい答えをハイライトし、バツマークを出す
            incorrectTextObject.SetActive(true);
            HighlightCorrectAnswer(_questionData, _questionId);
        }
    }

    /// <summary>
    /// クイズ回答後に出てくるボタンを制御する関数
    /// </summary>
    /// <param name="mode">押されたボタンのモード</param>
    public void OnClickUIButton(string mode)
    {
        switch (mode)
        {
            case "next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case "home":
                SceneManager.LoadScene("Home");
                break;
        }
    }

    /// <summary>
    /// 問題を読み込み、各テキストフィールドに文章を反映させるクラス
    /// </summary>
    /// <param name="data">問題データ</param>
    /// <param name="id">問題番号</param>
    private void SetQuestion(List<string[]> data, int id)
    {
        questionText.text = _questionData[id][0];
        choicesA.GetComponentInChildren<Text>().text = data[id][1];
        choicesB.GetComponentInChildren<Text>().text = data[id][2];
        choicesC.GetComponentInChildren<Text>().text = data[id][3];
        choicesD.GetComponentInChildren<Text>().text = data[id][4];
    }
    
    /// <summary>
    /// クイズに答えて、それが正解か不正解かをboolで返す関数
    /// </summary>
    /// <param name="data">問題データ</param>
    /// <param name="id">問題番号</param>
    /// <param name="answer">回答</param>
    /// <returns></returns>
    private bool AnswerQuestion(List<string[]> data, int id, string answer)
    {
        var isCorrect = ((data[id][5] == answer || data[id][5] == "X") && answer != "TimeOut");
        Debug.Log(isCorrect ? "正解" : "不正解");
        return isCorrect;
    }

    /// <summary>
    /// 正しい選択肢のボタンをハイライトするクラス
    /// </summary>
    /// <param name="data">クイズデータ</param>
    /// <param name="id">問題番号</param>
    private void HighlightCorrectAnswer(List<string[]> data, int id)
    {
        var choicesAButton = choicesA.GetComponent<Button>();
        var choicesBButton = choicesB.GetComponent<Button>();
        var choicesCButton = choicesC.GetComponent<Button>();
        var choicesDButton = choicesD.GetComponent<Button>();
        choicesAButton.interactable = false;
        choicesBButton.interactable = false;
        choicesCButton.interactable = false;
        choicesDButton.interactable = false;
        
        var correctAnswer = data[id][5];
        switch (correctAnswer)
        {
            case "A":
                choicesAButton.interactable = true;
                break;
            case "B":
                choicesBButton.interactable = true;
                break;
            case "C":
                choicesCButton.interactable = true;
                break;
            case "D":
                choicesDButton.interactable = true;
                break;
            case "X":
                choicesAButton.interactable = true;
                choicesBButton.interactable = true;
                choicesCButton.interactable = true;
                choicesDButton.interactable = true;
                break;
        }
    }

    /// <summary>
    /// タイマーとタイムリミットを表示するバーの制御をするクラス
    /// </summary>
    /// <param name="seconds">タイマーの秒数を指定</param>
    private void TimeLimitCounter(float seconds)
    {
        _countTime += Time.deltaTime;
        _progress = _countTime / seconds;
        timeBarImage.fillAmount = _progress;
        if(_progress >= 1f)OnClickAnswerButton("TimeOut");
    }
    /// <summary>
    /// 指定された秒数後に任意のアクションを実行するクラス
    /// </summary>
    /// <param name="seconds">待つ秒数</param>
    /// <param name="callback">実行したい任意のアクション</param>
    private async UniTask DelayAsync(float seconds, UnityAction callback)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        callback?.Invoke();
    }
}
