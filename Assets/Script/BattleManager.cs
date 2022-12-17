using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    MainModule _mainModule;

    public EffectUI effectUI;
    public BattleUI _battleUI;

    public AllEnemySO currentEnemys;
    public List<GameObject> fieldEnemies;

    public GameObject _wall;

    [Space]
    [Header("적 생성 범위 지정")]
    public float enemySpawnMinPoint;
    public float enemySpawnMaxPoint;

    [Space]
    public int killenemyCount;

    private void Start()
    {
        killenemyCount = 0;
        _mainModule = GameObject.Find("Player").GetComponent<MainModule>();
    }

    public void SetBattle()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() => effectUI.StartBattleEffectUI());
        seq.AppendInterval(3f);
        seq.AppendCallback(() => SpawnMonster());
        if (_mainModule.playerDataSO.killEnemy < 2)
        {
            seq.AppendInterval(1f);
            seq.AppendCallback(() =>
            {
                _mainModule.canInven = false;
                _battleUI.cemetryBtn.SetActive(true);
                _battleUI.pickCardBtn.SetActive(true);
                _battleUI.isCemetery = false;
                _wall.SetActive(false);
                _mainModule.SetBattleAni();
                BattleCameraEffect();
                SetBattleUI();
            });
        }
        else
        {
            seq.AppendInterval(3f);
            seq.AppendCallback(() =>
            {
                _mainModule.canInven = false;
                _battleUI.cemetryBtn.SetActive(true);
                _battleUI.pickCardBtn.SetActive(true);
                _battleUI.isCemetery = false;
                _wall.SetActive(false);
                _mainModule.SetBattleAni();
                BattleCameraEffect();
                SetBattleUI();
            });
        }
    }

    public void SpawnMonster()
    {
        if (_mainModule.playerDataSO.killEnemy < 2)
        {
            for (int i = 0; i < GetEnemy(); i++)
            {
                GameObject enemyPrefab = PoolManager.Instance.Pop(EnemyType()).gameObject;
                fieldEnemies.Add(enemyPrefab);
                fieldEnemies[i].transform.position = _mainModule._enemySpawnPoint[i].position;
                fieldEnemies[i].transform.DOScale(1, 1f);
            }
        }
        else
        {
            GameObject enemyPrefab = PoolManager.Instance.Pop(PoolType.Boss1 + (_mainModule.playerDataSO.stage - 1)).gameObject;
            enemyPrefab.transform.position = _mainModule._enemySpawnPoint[0].position;
            fieldEnemies.Add(enemyPrefab);
            fieldEnemies[0].transform.DOScale(0.65f, 1f);
        }
    }

    public void BattleCameraEffect()
    {
        _mainModule._UIModule.OnInteractionKeyImage();
        //_mainModule.battleCam.m_Lens.
        _mainModule.battleCam.Priority += 10;
    }

    public void EndBattle(string isWin)
    {
        _mainModule.canInven = true;


        _mainModule._animator.Play("Win");
        _mainModule._animator.SetBool("Fight", false);
        _mainModule.twoView.Priority += 10;
        _mainModule.battleCam.Priority -= 10;
        _mainModule.playerDataSO.killEnemy++;
        killenemyCount++;
        _battleUI.GameEnd(isWin);
        _wall.SetActive(true);
        _mainModule._BattleModule.inBattle = false;
        _mainModule._BattleModule.EndBattle();
        StartCoroutine(UnlockBattleLimit());
    }

    public void SetBattleUI()
    {
        _mainModule._HpModule.SetAvtiveHpbar(true);
        _mainModule._HpModule.UpdateHPText();
        _battleUI.SetBattleUI();
    }

    public IEnumerator ChangeTurn(bool isPlayer)
    {
        if (isPlayer)
        {
            for (int i = 0; i < fieldEnemies.Count; i++)
            {
                int randDef = Random.Range(fieldEnemies[i].GetComponent<EnemyData>().Def - 1, fieldEnemies[i].GetComponent<EnemyData>().Def + 2);
                fieldEnemies[i].GetComponent<HpModule>().OnShield(randDef);
                yield return new WaitForSeconds(1f);
            }
        }

        else
        {
            _mainModule._HpModule.OnShield(_mainModule.playerDataSO.Def);
            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < fieldEnemies.Count; i++)
            {
                fieldEnemies[i].GetComponent<HpModule>().shield = 0;
            }
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < fieldEnemies.Count; i++)
            {
                fieldEnemies[i].GetComponent<AIModule>().WhatToDo();
                yield return new WaitForSeconds(1f);
            }

            _battleUI.cardCount = _battleUI.GetCardCount();
            yield return new WaitForSeconds(0.5f);

            //for (int i = 0; i < fieldEnemies.Count; i++)
            //{
            //    fieldEnemies[i].GetComponent<HpModule>().OnShield();
            //    yield return new WaitForSeconds(1f);
            //}

            _battleUI.TurnChangeEffect(true);
            yield return new WaitForSeconds(0.2f);
            _battleUI.SetActiveButton(true);
            /*if(!shieldMaintain)*/
            _mainModule._HpModule.shield = 0;
        }
    }

    public bool SetTurn()
    {
        if (_mainModule.playerDataSO.Speed >= fieldEnemies[0].GetComponent<EnemyData>().Speed)
            return true;
        else return false;
    }

    IEnumerator UnlockBattleLimit()
    {
        yield return new WaitForSeconds(3f);
        _mainModule.canMove = false;
        _mainModule.twoView.Priority -= 10;
        yield return new WaitForSeconds(1f);
        _mainModule._UIModule.TrophyUIManager.AppearTrophy();
    }

    int GetEnemy()
    {
        int n = Mathf.Min(Random.Range(1, _mainModule.playerDataSO.stage + 2), 3);

        return n;
    }

    PoolType EnemyType()
    {
        var enumValues = System.Enum.GetValues(enumType: typeof(PoolType));
        return (PoolType)enumValues.GetValue(Random.Range((int)PoolType.Enemy1 + ((_mainModule.playerDataSO.stage - 1) * 5 ), (int)PoolType.Enemy4 + (_mainModule.playerDataSO.stage - 1) * 5));
    }

    //public NavMeshHit SetMonsterPos()
    //{
    //    NavMeshHit hit;
    //    float randomAngle = Random.Range(0, 2 * Mathf.PI);
    //    Vector3 summonPos = _mainModule.transform.position + Random.Range(enemySpawnMinPoint, enemySpawnMaxPoint) * new Vector3(Mathf.Cos(randomAngle), 0, Mathf.Sin(randomAngle));

    //    if(NavMesh.SamplePosition(summonPos, out hit, 0.5f, NavMesh.AllAreas))
    //    {
    //        return hit;
    //    }
    //    return 
    //}
}
