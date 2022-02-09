using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Button gameStartButton = default;
    [SerializeField] private Button creditButton = default;

    private void Start()
    {
        gameStartButton = gameStartButton.GetComponent<Button>();
        gameStartButton.onClick.AddListener(OnClickGameStartButton);

        creditButton = creditButton.GetComponent<Button>();
        creditButton.onClick.AddListener(OnClickCreditButton);
    }
    
    private void OnClickGameStartButton()
    {
        SceneManager.LoadScene("tutorial");
    }

    private void OnClickCreditButton()
    {
        SceneManager.LoadScene("Credit");
    }
}
