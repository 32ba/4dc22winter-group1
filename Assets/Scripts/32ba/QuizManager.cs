using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _32ba;

public class QuizManager : MonoBehaviour
{
    public TextAsset questionCsv;
    public GameObject choicesA;
    public GameObject choicesB;
    public GameObject choicesC;
    public GameObject choicesD;
    public GameObject correctTextObject;
    public GameObject incorrectTextObject;
    public Text questionText;

    private readonly List<string[]> _questionData = new List<string[]>();
    private int _questionId;
    private bool _isAlreadyAnswered = false;

    // Start is called before the first frame update
    void Start()
    {
        bool isQuestionCsvLoaded = CsvReader.Read(questionCsv, _questionData);
        Debug.Log(isQuestionCsvLoaded ? "問題CSVの読み込みに成功しました" : "問題CSVの読み込みに失敗しました");
        _questionId = UnityEngine.Random.Range(0, _questionData.Count);
        SetQuestion(_questionData, _questionId);
    }

    public void OnClick(string answer)
    {
        if (_isAlreadyAnswered) return;
        _isAlreadyAnswered = true;
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
        var isCorrect = (data[id][5] == answer);
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
                choicesBButton.interactable = true;
                break;
            case "D":
                choicesBButton.interactable = true;
                break;
        }
    }
}
