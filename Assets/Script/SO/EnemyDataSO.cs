using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("적 모델(프리팹)")]
    public GameObject _enemyModle;

    [Space]
    [Header("적 기본 능력치")]
    public BaseAbilityDataSO _enemyAbility;
}