using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("�ɷ�ġ")]
    public float walkSpeed;
    public float runSpeed;
    public int Health;
    public int Ad;
    public int Def;
    public int Speed;

    [Space]
    [Header("�⺻ �ɷ�ġ")]
    public BaseAbilityDataSO player;
    [Space]
    [Header("���� ���� ����")]
    public bool canBattle;

    [Space]
    [Header("�÷��̾� ���� ��ų")]
    public Skill[] _skills;

    //public void ChangeStat(string statName, int addState)
    //{
    //    switch(statName)
    //    {
    //        case "ü��":
    //            Health = player._hp + addState;
    //            break;
    //        case "���ݷ�":
    //            Ad = player._atk + addState;
    //            break;
    //        case "����":
    //            Def = player._def + addState;
    //            break;
    //        case "������":
    //            Speed = player._speed + addState;
    //            break;
    //    }
    //}
}