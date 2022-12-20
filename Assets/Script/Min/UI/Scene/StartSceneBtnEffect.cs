using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class StartSceneBtnEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOLocalMoveX(-700f, 0.5f);
        transform.GetComponent<Image>().DOColor(new Color(1, 1, 0), 0.5f);
    }
    public void DoFade(Color startColor, Color endColor, float time, Image image)
    {
       // DOTween.To(() => startColor, x => { startColor = x; image.color = endColor, time).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOLocalMoveX(-780f, 0.5f);
        transform.GetComponent<Image>().DOColor(new Color(1, 1, 1), 0.5f);
    }


}
