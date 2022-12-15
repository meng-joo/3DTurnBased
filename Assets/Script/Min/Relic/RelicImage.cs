using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
public class RelicImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RelicSO relicSO;
    public GameObject explainTap;
    public Image relicImage;
    private void Awake()
    {
        explainTap = GameObject.Find("RelicCanvas").transform.Find("ExplainTap").gameObject;
        relicImage = GetComponent<Image>();
    }
    public void Set(RelicSO _relicSO)
    {
        relicSO = _relicSO;
        relicImage.sprite = _relicSO.relicImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
        explainTap.SetActive(true);
        explainTap.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = relicSO.relicName;
        explainTap.transform.Find("ExplainText").GetComponent<TextMeshProUGUI>().text = relicSO.relicExplain;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale((Vector3.one), 0.5f);
        explainTap.SetActive(false);
    }

  
}
