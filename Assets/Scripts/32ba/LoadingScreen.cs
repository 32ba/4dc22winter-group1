using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;


public class LoadingScreen : MonoBehaviour
{
    public Text loadingText;
    public GameObject rotateImageObject;
    private string _text = "Loading";
    private int _step = 0;
    
    private void OnEnable()
    {
        Observable.Interval(TimeSpan.FromSeconds(0.4f)).TakeUntilDisable(this).Subscribe(_ => TextController());
        Observable.Interval(TimeSpan.FromSeconds(0.01f)).TakeUntilDisable(this).Subscribe(_ => RotateImage());
        //ToDo:横にサンマが跳ねてるLive2Dを入れたら見栄えが良さそう
    }

    private void OnDisable()
    {
        _text = "Loading";
        loadingText.text = _text;
        rotateImageObject.transform.rotation =  Quaternion.identity;
    }

    private void TextController()
    {
        _text += ".";
        _step++;
        if (_step == 4)
        {
            _step = 0;
            _text = "Loading";
        }
        loadingText.text = _text;
    }

    private void RotateImage()
    {
        rotateImageObject.transform.Rotate(0,0,-2f);
    }
}
