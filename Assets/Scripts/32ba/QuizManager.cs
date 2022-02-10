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
        bool isQuestionCsvLoaded = CsvReader.Read(questionFile, _questionData, '	');
        Debug.Log(isQuestionCsvLoaded ? "問題CSVの読み込みに成功しました" : "問題CSVの読み込みに失敗しました");
        _questionId = UnityEngine.Random.Range(0, _questionData.Count);
        SetQuestion(_questionData, _questionId);
        DelayAsync(1.0f, () => {
            choicesA.SetActive(true);
            choicesB.SetActive(true);
            choicesC.SetActive(true);
            choicesD.SetActive(true);
            timeLimitBarObject.SetActive(true);
            _isEnableTimer = true;
        }).Forget();
    }

    private void Update()
    {
        if (_isEnableTimer)TimeLimitCounter(5.0f);
    }

    public void OnClickAnswerButton(string answer)
    {
        if (_isAlreadyAnswered) return;
        _isAlreadyAnswered = true;
        _isEnableTimer = false;
        DelayAsync(1.0f, () => {afterAnsweringPanelObject.SetActive(true);}).Forget();
        if (AnswerQuestion(_questionData, _questionId, answer))
        {
            correctTextObject.SetActive(true);
            HighlightCorrectAnswer(_questionData, _questionId);
            //ToDo:正解したらポイントを付与する
        }
        else
        {
            //ToDo:不正解なら正しい答えをハイライトして次へすすむ
            incorrectTextObject.SetActive(true);
            HighlightCorrectAnswer(_questionData, _questionId);
        }
    }

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

    private void SetQuestion(List<string[]> data, int id)
    {
        questionText.text = _questionData[id][0];
        choicesA.GetComponentInChildren<Text>().text = data[id][1];
        choicesB.GetComponentInChildren<Text>().text = data[id][2];
        choicesC.GetComponentInChildren<Text>().text = data[id][3];
        choicesD.GetComponentInChildren<Text>().text = data[id][4];
    }
    
    private bool AnswerQuestion(List<string[]> data, int id, string answer)
    {
        var isCorrect = ((data[id][5] == answer || data[id][5] == "X") && answer != "TimeOut");
        Debug.Log(isCorrect ? "正解" : "不正解");
        return isCorrect;
    }

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

    private void TimeLimitCounter(float seconds)
    {
        _countTime += Time.deltaTime;
        _progress = _countTime / seconds;
        timeBarImage.fillAmount = _progress;
        if(_progress >= 1f)OnClickAnswerButton("TimeOut");
    }
    private async UniTask DelayAsync(float seconds, UnityAction callback)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        callback?.Invoke();
    }
}
