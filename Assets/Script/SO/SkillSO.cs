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
    [Header("��ų�̸�")]
    public string _skillName;

    [Header("��ų����")]
    public SkillType _skillType;

    [Header("��ų �̹���")]
    public Sprite _skillImage;

    [Header("��ų �ִϸ��̼�")]
    public AnimationClip _animationClip;

    [Header("��ų ����Ʈ")]
    public GameObject _particle;

    [Header("�÷��̾� ȹ�� ����")]
    public bool isPossession;

    [TextArea]
    [Header("��ų ����")]
    public string _skillExplanation;
}