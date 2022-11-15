using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/AllEnemySO")]
public class AllEnemySO : ScriptableObject
{
    public EnemyDataSO[] floor_1;
    public EnemyDataSO[] floor_2;
    public EnemyDataSO[] floor_3;
}
