using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BaseAbilitySO")]
public class BaseAbilityDataSO : ScriptableObject
{
    [Header("�⺻ �ɷ�ġ")]
    public int _hp;
    public int _def;
    public int _atk;
    public int _speed;
}
