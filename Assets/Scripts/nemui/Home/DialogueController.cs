using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class DialogueController : MonoBehaviour
{
  [SerializeField] private Button screenTouchArea = default;
  [SerializeField] private string[] characterDialogue = default;
  private Text dialogueText;
  void Start()
  {
    dialogueText = this.gameObject.transform.GetChild(0).GetComponent<Text>();

    screenTouchArea = screenTouchArea.GetComponent<Button>();
    screenTouchArea.OnClickAsObservable()
                       .Subscribe(_ =>
                         OnClickScreenTouchArea()
                       )
                       .AddTo(this);

    OnClickScreenTouchArea(); // はじめに一回だけ実行する
  }

  private void OnClickScreenTouchArea()
  {
    dialogueText.text = characterDialogue[Random.Range(0, characterDialogue.Length)];
  }
}
