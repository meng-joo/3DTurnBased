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
    [Header("��ų�̸�")]
    public string _skillName;

    [Header("��ų����")]
    public SkillType _skillType;

    [Header("��ų���")]
    public string target;

    [Header("��ųȿ��")]
    public string skilltypeText;

    [Header("ȿ�� ��ġ")]
    public int value;

    [Header("��ų����")]
    [ColorUsage(true)]
    public Color32 _skillCardColor;

    [Header("��ų��ġ����")]
    [ColorUsage(true)]
    public Color32 _skillEffectColor;

    [Header("��ų �̹���")]
    public Sprite _skillImage;

    [Header("��ų �ִϸ��̼�")]
    public AnimationClip _animationClip;

    [Header("��ų ����Ʈ")]
    public PoolType _poolType;

    [Header("��ų �Լ��̸�")]
    public string[] _methodName; 

    [Header("�÷��̾� ȹ�� ����")]
    public bool isPossession;

    [TextArea]
    [Header("��ų ����")]
    public string _skillExplanation;
}
