using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillType
{
    ATK,
    BENEFICIAL,
    CURSE
}

[Serializable]
public class SkillData
{
    [Header("스킬이름")]
    public string _skillName;

    [Header("스킬종류")]
    public SkillType _skillType;

    [Header("스킬색깔")]
    [ColorUsage(true)]
    public Color32 _skillCardColor;

    [Header("스킬 이미지")]
    public Sprite _skillImage;

    [Header("스킬 애니메이션")]
    public AnimationClip _animationClip;

    [Header("스킬 이펙트")]
    public GameObject _particle;

    [Header("스킬 함수이름")]
    public string _methodName; 

    [Header("플레이어 획득 여부")]
    public bool isPossession;

    [TextArea]
    [Header("스킬 설명")]
    public string _skillExplanation;
}
