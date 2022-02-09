using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    [SerializeField] private Button backTitleButton = default;

    private void Start()
    {
        backTitleButton = backTitleButton.GetComponent<Button>();
        backTitleButton.onClick.AddListener(OnClickBackTitleButton);
    }
    
    private void OnClickBackTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
