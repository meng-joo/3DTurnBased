using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStack
{
    Str, //힘
    Hp, // 체력
    Benefit_Effect, //버프 
    Detrimental_Effect, // 저주
}

[System.Serializable]
public class ItemAbility
{
    public CharacterStack characterStack;
    public int valStack;

    [SerializeField] private int min;
    [SerializeField] private int max;

    public int Min => min;
    public int Max => max;

    public ItemAbility(int min, int max)
    {
        this.min = min;
        this.max = max;
        GetStackVal();
    }
    public void GetStackVal()
    {
        valStack = UnityEngine.Random.Range(min, max);
    }

    public void AddStackVal(ref int v)
    {
        v += valStack;
    }
}

