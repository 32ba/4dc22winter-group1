using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using _32ba;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TextAsset questionFile;
    public GameObject choicesA;
    public GameObject choicesB;
    public GameObject choicesC;
    public GameObject choicesD;
    public GameObject correctTextObject;
    public GameObject incorrectTextObject;
    public GameObject afterAnsweringPanel;
    public Text questionText;

    private readonly List<string[]> _questionData = new List<string[]>();
    private int _questionId;
    private bool _isAlreadyAnswered = false;

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
        }).Forget();
    }

    public void OnClickAnswerButton(string answer)
    {
        if (_isAlreadyAnswered) return;
        _isAlreadyAnswered = true;
        DelayAsync(1.0f, () => {afterAnsweringPanel.SetActive(true);}).Forget();
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
                //ToDo:ホーム画面のシーンへ遷移する処理
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
        var isCorrect = (data[id][5] == answer || data[id][5] == "X");
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
    private async UniTask DelayAsync(float seconds, UnityAction callback)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(seconds));
        callback?.Invoke();
    }
}
