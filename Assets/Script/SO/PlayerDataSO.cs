using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("�ɷ�ġ")]
    public float walkSpeed;
    public float runSpeed;
    public BaseAbilityDataSO player;

    [Space]
    [Header("�÷��̾� ���� ��ų")]
    public Skill[] _skills;
}