using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;

public class TitleController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup = default;
    [SerializeField] private Button gameStartButton = default;
    [SerializeField] private Button creditButton = default;
    private float fadeInTime = 0.3f;
    private float fadeOutTime = 0.3f;

    private async void Start()
    {
        await FadeManager.FadeIn(canvasGroup, fadeInTime);

        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();

        gameStartButton = gameStartButton.GetComponent<Button>();
        gameStartButton.OnClickAsObservable()
                       .First()
                       .Subscribe(_ => {
                           OnClickGameStartButton().Forget();
                       })
                       .AddTo(gameStartButton);

        creditButton = creditButton.GetComponent<Button>();
        creditButton.OnClickAsObservable()
                       .First()
                       .Subscribe(_ => {
                           OnClickCreditButton().Forget();
                       })
                       .AddTo(gameStartButton);
    }
    
    /// <summary>
    /// GameStartボタンを押した時にチュートリアル画面へ遷移させるクラス
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OnClickGameStartButton()
    {
        await FadeManager.FadeOut(canvasGroup, fadeOutTime);
        SceneManager.LoadScene("tutorial");
    }

    /// <summary>
    /// Creditボタンを押した時にクレジット画面へ遷移させるクラス
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OnClickCreditButton()
    {
        await FadeManager.FadeOut(canvasGroup, fadeOutTime);
        SceneManager.LoadScene("Credit");
    }
}
