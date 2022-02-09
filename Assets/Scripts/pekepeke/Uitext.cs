
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uitext : MonoBehaviour
{
    public Text nameText;
    public Text talkText;

    public bool playing = false;
    public float textSpeed = 0.1f;

    void Start() { }

    // �N���b�N�Ŏ��̃y�[�W��\�������邽�߂̊֐�
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    // �i���[�V�����p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string text)
    {
        nameText.text = "";
        StartCoroutine("CoDrawText", text);
    }
    // �ʏ��b�p�̃e�L�X�g�𐶐�����֐�
    public void DrawText(string name, string text)
    {
        nameText.text = name + "\n�u";
        StartCoroutine("CoDrawText", text + "�v");
    }

    // �e�L�X�g���k���k���o�Ă��邽�߂̃R���[�`��
    IEnumerator CoDrawText(string text)
    {
        playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            // �N���b�N�����ƈ�C�ɕ\��
            if (IsClicked()) break;

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > text.Length) break;
            talkText.text = text.Substring(0, len);
        }
        talkText.text = text;
        yield return 0;
        playing = false;
    }

}