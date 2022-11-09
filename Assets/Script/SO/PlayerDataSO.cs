using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("능력치")]
    public float walkSpeed;
    public float runSpeed;
    public BaseAbilityDataSO player;

    [Space]
    [Header("플레이어 보유 스킬")]
    public Skill[] _skills;
}