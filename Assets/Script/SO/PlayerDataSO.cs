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

    [Header("���� �������� �׸��� ���� ��")]
    public int stage;
    public int killEnemy;

    [Space]
    [Header("�ڽ�Ʈ")]
    public int cost;

    [Space]
    [Header("�⺻ �ɷ�ġ")]
    public BaseAbilityDataSO player;
    [Space]
    [Header("���� ���� ����")]
    public bool canBattle;

    [Space]
    [Header("�÷��̾� ���� ��ų")]
    public Skill[] _skills;

    #region �������� �Һ�����
    public bool isBuringBlood = false;
    public bool isAnchor = false;
    public bool isBagOfMarbles = false;
    public bool isStrawberry = false;
    public bool isLantern = false;
    public bool isBloodVial = false;
    public bool isPantograph = false;
    public bool isClockClasp = false;
    public bool isLetterOpener = false;
    public int threeCnt = 0;
    #endregion

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