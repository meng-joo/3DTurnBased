using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ATK,
    BENEFICIAL,
    CURSE
}

[CreateAssetMenu(menuName = "SO/Skill")]
public class SkillSO : ScriptableObject
{
    [Header("스킬이름")]
    public string _skillName;

    [Header("스킬종류")]
    public SkillType _skillType;

    [Header("스킬 이미지")]
    public Sprite _skillImage;

    [Header("스킬 애니메이션")]
    public AnimationClip _animationClip;

    [Header("스킬 이펙트")]
    public GameObject _particle;

    [Header("플레이어 획득 여부")]
    public bool isPossession;

    [TextArea]
    [Header("스킬 설명")]
    public string _skillExplanation;
}