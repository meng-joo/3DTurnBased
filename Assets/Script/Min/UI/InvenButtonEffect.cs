using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
public class InvenButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Color _color;

    private RectTransform rect;
    private Image image;
    private Color _OriginColor;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _OriginColor = image.color;
        image.color = _color;
        rect.DOScale(new Vector3(1.15f, 1.25f, 1.15f), 0.5f).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = _OriginColor;
        rect.DOScale(Vector3.one, 0.5f).SetUpdate(true);
    }


}
