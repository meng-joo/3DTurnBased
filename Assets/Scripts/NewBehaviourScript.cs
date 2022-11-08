using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    public Image fadeOutImage = null;

    private void Start()
    {
        MenuCutScene();
    }
    public void MenuCutScene()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            fadeOutImage.gameObject.SetActive(true);
        });
        seq.AppendInterval(0.5f);
        seq.Append(fadeOutImage.DOFade(0f, 3f));
        seq.AppendCallback(() =>
        {
            fadeOutImage.gameObject.SetActive(false);
        });


    }
}
