using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    [SerializeField] private Button gachaButton = default;
    [SerializeField] private Button quizButton = default;

    private void Start()
    {
        gachaButton = gachaButton.GetComponent<Button>();
        gachaButton.onClick.AddListener(OnClickGachaButton);

        quizButton = quizButton.GetComponent<Button>();
        quizButton.onClick.AddListener(OnClickQuizButton);
    }
    
    private void OnClickGachaButton()
    {
        SceneManager.LoadScene("PlayGachaAndResult");
    }

    private void OnClickQuizButton()
    {
        SceneManager.LoadScene("quiz");
    }
}
