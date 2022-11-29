using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
public class CardImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image backGroundImage;
    public Image skillImage;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillInfo;

    public SkillIInvenObj invenObj;

    private TrophyUIManager trophyUIManager;

    private void Awake()
    {
        backGroundImage = GetComponent<Image>();
        skillImage = transform.GetChild(0).GetComponent<Image>();
        skillName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        skillInfo = transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        trophyUIManager = GameObject.Find("TrophyManager").GetComponent<TrophyUIManager>();
    }
    public void Init(Skill skillData)
    {
        backGroundImage.color = skillData.skillInfo._skillCardColor;
        skillImage.sprite = skillData.skillInfo._skillImage;
        skillName.text = skillData.skillInfo._skillName;
        skillInfo.text = skillData.skillInfo._skillExplanation;

        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            invenObj.cards.Add(skillData);
            trophyUIManager.trophyPanel.SetActive(true);
            trophyUIManager.selectPanel.SetActive(false);
            Debug.Log(trophyUIManager.parentTrm.childCount);
            trophyUIManager.InitTrophy();
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.4f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1.45f, 1.45f, 1.45f), 0.4f);
    }
}
