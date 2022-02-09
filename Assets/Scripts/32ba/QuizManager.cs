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
    public Text questionText;
    
    private List<string[]> _questionData = new List<string[]>();
    private bool _isQuestionCsvLoaded = false;
    private int _questionId;
    
    // Start is called before the first frame update
    void Start()
    {
        _isQuestionCsvLoaded = CsvReader.Read(questionCsv, _questionData);
        Debug.Log(_isQuestionCsvLoaded ? "問題CSVの読み込みに成功しました" : "問題CSVの読み込みに失敗しました");
        _questionId = UnityEngine.Random.Range(0, _questionData.Count);
        SetQuestion(_questionData, _questionId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetQuestion(List<string[]> data, int id)
    {
        questionText.text = _questionData[id][0];
        choicesA.GetComponentInChildren<Text>().text = _questionData[id][1];
        choicesB.GetComponentInChildren<Text>().text = _questionData[id][2];
        choicesC.GetComponentInChildren<Text>().text = _questionData[id][3];
        choicesD.GetComponentInChildren<Text>().text = _questionData[id][4];
    }
}
