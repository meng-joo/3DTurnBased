using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("�� ��(������)")]
    public GameObject _enemyModle;

    [Space]
    [Header("�� �⺻ �ɷ�ġ")]
    public BaseAbilityDataSO _enemyAbility;
}