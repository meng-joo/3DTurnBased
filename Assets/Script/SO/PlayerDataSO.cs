using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("플레이어 기본 능력치")]
    public float walkSpeed;
    public float runSpeed;
    public int atk;
    public int hp;
    public int def;

    [Space]
    [Header("플레이어 보유 스킬")]
    public SkillSO[] _skills;
}