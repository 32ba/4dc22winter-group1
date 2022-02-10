using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;

public class CreditController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup = default;
    [SerializeField] private Button backTitleButton = default;
    private float fadeInTime = 0.3f;
    private float fadeOutTime = 0.3f;

    private async void Start()
    {
        await FadeManager.FadeIn(canvasGroup, fadeInTime);

        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();

        backTitleButton = backTitleButton.GetComponent<Button>();
        backTitleButton.OnClickAsObservable()
                       .First()
                       .Subscribe(_ => {
                           OnClickBackTitleButton().Forget();
                       })
                       .AddTo(backTitleButton);
    }
    
    private async UniTaskVoid OnClickBackTitleButton()
    {
        await FadeManager.FadeOut(canvasGroup, fadeOutTime);
        SceneManager.LoadScene("Title");
    }
}
