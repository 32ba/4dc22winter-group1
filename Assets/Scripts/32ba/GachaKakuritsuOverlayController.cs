using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaKakuritsuOverlayController : MonoBehaviour
{
    public GameObject gachaKakuritsuImageObject;
    private void OnEnable()
    {
        gachaKakuritsuImageObject.transform.position = Vector3.zero;
    }

    public void OnClickCloseButton()
    {
        gameObject.SetActive(false);
    }
}
