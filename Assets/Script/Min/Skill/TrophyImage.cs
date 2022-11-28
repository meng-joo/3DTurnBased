using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
public class TrophyImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI cntTxt;

    void Update()
    {
        UpdateEffect();
    }
    private void Start()
    {
        // StartCoroutine(DagiEffect());
    }
    void UpdateEffect()
    {

    }

    IEnumerator DagiEffect()
    {
        while (true)
        {
            image.color = new Color32(180, 195, 195, 255);

            yield return new WaitForSeconds(0.5f);

            image.color = new Color32(255, 255, 255, 255);

            yield return new WaitForSeconds(0.5f);

            //seq.Append(image.DOColor(new Color(180f, 195f, 195f), 0.3f));
            //seq.Join(image.rectTransform.DOScale((new Vector3(1.2f, 1.2f, 1.2f)), 0.3f));

            //seq.AppendInterval(0.5f);

            //seq.Append(image.DOColor(new Color(255, 255, 255), 0.3f));
            //seq.Join(image.rectTransform.DOScale(Vector3.one, 0.3f));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(image.DOColor(new Color32(255, 255, 255, 255), 0.25f));
        nameTxt.color = new Color32(255, 255, 255, 255);
        cntTxt.color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(image.DOColor(new Color32(150, 150, 150, 255), 0.25f));


        nameTxt.color = new Color32(255, 255, 0, 255);
        cntTxt.color = new Color32(255, 255, 0, 255);
    }
}
