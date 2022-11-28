using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardImage : MonoBehaviour
{
    public Image backGroundImage;
    public Image skillImage;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillInfo;

    public SkillIInvenObj invenObj;

   // public List<MethodInfo> skillEffect = new List<MethodInfo>();

    private void Awake()
    {
        backGroundImage = GetComponent<Image>();
        skillImage = transform.GetChild(0).GetComponent<Image>();
        skillName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        skillInfo = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
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
        });           
        //value = skillData.skillInfo.value;
        //skillText = skillData.skillInfo._skillEffectColor;
        //target = skillData.skillInfo.target;
        //skillEffect = skillData._SetSkill._Methods;
    }
}
