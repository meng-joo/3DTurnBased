using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class StartCardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Skill skill;
    public void Set(Skill _skill)
    {
        skill = _skill;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.5f);
    }


}
