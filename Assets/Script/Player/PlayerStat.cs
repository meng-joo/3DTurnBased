using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/PlayerStat")]
public class PlayerStat : ScriptableObject
{
    public int Health;
    public int Ad;
    public int Def;
    public int Speed;
}
