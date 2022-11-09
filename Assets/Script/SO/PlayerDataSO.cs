using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("�÷��̾� �⺻ �ɷ�ġ")]
    public float walkSpeed;
    public float runSpeed;
    public int atk;
    public int hp;
    public int def;

    [Space]
    [Header("�÷��̾� ���� ��ų")]
    public SkillSO[] _skills;
}