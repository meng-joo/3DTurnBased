using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class DialogManager : MonoSingleton<DialogManager>
{
    public TextMeshProUGUI _tipText;
    public bool _ischatting;

    public Image dialog;
    public ParticleSystem ps;   
    public void ShowText(string text)
    {
        AudioManager.PlayAudio(UISoundManager.Instance.data.tiperrorClip);
        StartCoroutine(ShowStoreBehave(text));
    }
    IEnumerator ShowStoreBehave(string text)
    {
        _ischatting = true;
        _tipText.DOFade(1f, 0.5f);
        dialog.DOFade(1f, 0.5f);

       GameObject obj =  Instantiate(ps, Camera.main.ScreenToWorldPoint(dialog.transform.position), Quaternion.identity).gameObject;

        Debug.Log(ps);
        for (int i = 0; i <= text.Length; i++)
        {
            _tipText.text = string.Format(text.Substring(0, i));
            yield return new WaitForSecondsRealtime(0.04f);
        }
        _ischatting = false;
        yield return new WaitForSecondsRealtime(1f);

        _tipText.DOFade(0f, 0.3f);
        dialog.DOFade(0f, 0.5f);
        Destroy(obj);
        //AblingButtons(true);
    }
}
