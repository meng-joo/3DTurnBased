using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BaseAbilitySO")]
public class BaseAbilityDataSO : ScriptableObject
{
    [Header("기본 능력치")]
    public int _hp;
    public int _def;
    public int _atk;
    public int _speed;
}
