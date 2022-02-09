using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _32ba;

public class QuizManager : MonoBehaviour
{
    public TextAsset questionCSV;

    private List<string[]> _questionData = new List<string[]>();
    private bool _isQuestionCSVLoaded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _isQuestionCSVLoaded = CsvReader.Read(questionCSV, _questionData);
        Debug.Log(_isQuestionCSVLoaded ? "問題CSVの読み込みに成功しました" : "問題CSVの読み込みに失敗しました");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
