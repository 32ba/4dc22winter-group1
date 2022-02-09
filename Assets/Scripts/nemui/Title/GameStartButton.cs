using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    private Button gameStartButton;

    private void Start()
    {
        gameStartButton = this.GetComponent<Button>();
        gameStartButton.onClick.AddListener(OnClickGameStartButton);
    }
    
    private void OnClickGameStartButton()
    {
        SceneManager.LoadScene("tutorial");
    }
}
