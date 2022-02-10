using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaKakuritsuOverlayController : MonoBehaviour
{
    public GameObject gachaKakuritsuImageObject;
    private void OnEnable()
    {
        gachaKakuritsuImageObject.transform.position = new Vector3(850f,0,0);
    }

    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
