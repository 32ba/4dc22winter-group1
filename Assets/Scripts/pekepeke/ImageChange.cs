using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChange : MonoBehaviour
{

    public GameObject sad;
    public GameObject smile;

    public void ChangeSad()
    {
        sad.SetActive(true);
        smile.SetActive(false);
    }

    public void ChangeSmile()
    {
        sad.SetActive(false);
        smile.SetActive(true);
    }

}
