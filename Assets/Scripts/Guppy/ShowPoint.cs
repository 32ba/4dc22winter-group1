using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPoint : MonoBehaviour
{
    public Text pointText;

    void Update()
    {
        pointText.text = $"{DataManager.GetPoint()}‰~";
    }
}
