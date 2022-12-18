using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class DicBtnEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
        transform.GetComponent<Image>().DOColor(new Color32(255, 255, 255, 255), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.5f);
        transform.GetComponent<Image>().DOColor(new Color32(225, 215, 215, 255), 0.5f);
    }

}
