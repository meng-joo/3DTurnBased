using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("능력치")]
    public float walkSpeed;
    public float runSpeed;
    public int Health;
    public int Ad;
    public int Def;
    public int Speed;

    [Header("현재 스테이지 그리고 잡은 적")]
    public int stage;
    public int killEnemy;

    [Space]
    [Header("코스트")]
    public int cost;

    [Space]
    [Header("기본 능력치")]
    public BaseAbilityDataSO player;
    [Space]
    [Header("전투 가능 지역")]
    public bool canBattle;

    [Space]
    [Header("플레이어 보유 스킬")]
    public List<Skill> _skills;

    #region 유물관련 불변수들
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
    //        case "체력":
    //            Health = player._hp + addState;
    //            break;
    //        case "공격력":
    //            Ad = player._atk + addState;
    //            break;
    //        case "방어력":
    //            Def = player._def + addState;
    //            break;
    //        case "빠르기":
    //            Speed = player._speed + addState;
    //            break;
    //    }
    //}
}