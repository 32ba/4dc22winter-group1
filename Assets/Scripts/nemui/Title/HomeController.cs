using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;

public class HomeController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup = default;
    [SerializeField] private Button gachaButton = default;
    [SerializeField] private Button quizButton = default;
    private float fadeInTime = 0.3f;
    private float fadeOutTime = 0.3f;

    private async void Start()
    {
        await FadeManager.FadeIn(canvasGroup, fadeInTime);

        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();

        gachaButton = gachaButton.GetComponent<Button>();
        gachaButton.OnClickAsObservable()
                       .First()
                       .Subscribe(_ => {
                           OnClickGachaButton().Forget();
                       })
                       .AddTo(this);

        quizButton = quizButton.GetComponent<Button>();
        quizButton.OnClickAsObservable()
                       .First()
                       .Subscribe(_ => {
                           OnClickQuizButton().Forget();
                       })
                       .AddTo(this);
    }
    
    /// <summary>
    /// Gachaボタンを押した時にガチャ画面へ遷移させるクラス
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OnClickGachaButton()
    {
        await FadeManager.FadeOut(canvasGroup, fadeOutTime);
        SceneManager.LoadScene("PlayGachaAndResult");
    }

    /// <summary>
    /// Quizボタンを押した時にクイズ画面へ遷移させるクラス
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OnClickQuizButton()
    {
        await FadeManager.FadeOut(canvasGroup, fadeOutTime);
        SceneManager.LoadScene("quiz");
    }
}

