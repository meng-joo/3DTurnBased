using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    [Header("적 기본 데이터")]
    public EnemyDataSO enemyData;

    [Space]
    [Header("현재 적 데이터")]
    public int _hp;
    public int _atk;
    public int _def;
    public int _speed;

    private void Start()
    {
        float randhp = Random.Range(0.7f, 1.3f);
        float randatk = Random.Range(0.7f, 1.3f);
        float randdef = Random.Range(0.7f, 1.3f);
        float randspeed = Random.Range(0.7f, 1.3f);

        _hp = (int)(enemyData._enemyAbility._hp * randhp);
        _atk = (int)(enemyData._enemyAbility._atk * randatk);
        _def = (int)(enemyData._enemyAbility._def * randdef);
        _speed = (int)(enemyData._enemyAbility._speed * randspeed);
    }
}
