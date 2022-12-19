using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : PoolAbleObject
{
    [Header("적 기본 데이터")]
    public EnemyDataSO enemyData;

    [Header("모듈")]
    public AIModule _aIModule;

    [Space]
    [Header("현재 적 데이터")]
    private int _hp;
    private int _atk;
    private int _def;
    private int _speed;

    public int Hp => _hp;
    public int Atk => _atk;
    public int Def => _def;
    public int Speed => _speed;

    public void Awake()
    {
        _aIModule = GetComponent<AIModule>();
    }

    public override void Init_Pop()
    {
        float randhp = Random.Range(0.7f, 1.3f) * Mathf.Min(Mathf.Min(_aIModule.player.playerDataSO.stage * Random.Range(0.4f, 1.5f), 1), 2);
        float randatk = Random.Range(0.7f, 1.3f);
        float randdef = Random.Range(0.7f, 1.3f);
        float randspeed = Random.Range(0.7f, 1.3f);

        _hp = (int)(enemyData._enemyAbility._hp * randhp);
        _atk = (int)(enemyData._enemyAbility._atk * randatk);
        _def = (int)(enemyData._enemyAbility._def * randdef);
        _speed = (int)(enemyData._enemyAbility._speed * randspeed);

        _aIModule = GetComponent<AIModule>();
        _aIModule.SetEnemy();
    }

    public override void Init_Push()
    {
    }
}
