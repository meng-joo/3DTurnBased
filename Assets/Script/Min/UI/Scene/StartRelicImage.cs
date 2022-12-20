using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class StartRelicImage : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public GameObject explainTap;
    public RelicSO relicSo;

    private void Awake()
    {
        explainTap = GameObject.Find("StartCanvas").transform.Find("ExplainTap").gameObject;
    }
    public void Set(RelicSO relic)
    {
        relicSo = relic;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        explainTap.SetActive(true);
        AudioManager.PlayAudio(UISoundManager.Instance.data.foucusClip);

        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);
        explainTap.transform.Find("NameText").GetComponent<TextMeshProUGUI>().text = relicSo.relicName;
        explainTap.transform.Find("ExplainText").GetComponent<TextMeshProUGUI>().text = relicSo.relicExplain;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.5f);
        explainTap.SetActive(false);
    }

   
}
