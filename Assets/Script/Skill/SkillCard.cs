using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCard : PoolAbleObject
{
    [Header("스킬 카드 요소")]
    public Image backGroundImage;
    public Image skillImage;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillInfo;

    private void Start()
    {
        backGroundImage = GetComponent<Image>();
        skillImage = transform.GetChild(0).GetComponent<Image>();
        skillName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        skillInfo = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void SetSkillCard(Skill skillData)
    {
        backGroundImage.color = skillData.skillInfo._skillCardColor;
        skillImage.sprite = skillData.skillInfo._skillImage;
        skillName.text = skillData.skillInfo._skillName;
        skillInfo.text = skillData.skillInfo._skillExplanation;
    }

    public override void Init_Pop()
    {
    }

    public override void Init_Push()
    {
    }
}
