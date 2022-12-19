using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AI;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    MainModule _mainModule;

    public CinemachineVirtualCamera playerHitCam;

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
    public int maxEnemyCount;

    public ChestCreateManager ccm;

    public GameObject bagImage;
    public GameObject settingImage;

    public GameObject relicParent;
    public GameObject battleRelicParent;

    public GameObject dirSprite;
    private void Start()
    {
        killenemyCount = 0;
        _mainModule = GameObject.Find("Player").GetComponent<MainModule>();
    }

    public void SetBattle()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendInterval(0.5f);
        seq.AppendCallback(() => SpawnMonster());
        seq.AppendInterval(1f);
        seq.AppendCallback(() =>
        {
            //for (int i = 0; i < 3; i++)
            // {
            //   _battleUI.costObj[i].SetActive(true);
            //}
            dirSprite.SetActive(true);
            bagImage.transform.DOMoveX(2000f, 0.5f);
            settingImage.transform.DOMoveX(2000f, 0.5f);
            relicParent.SetActive(false);
            battleRelicParent.SetActive(true);

            ccm.parentTrm.SetActive(false);
            _mainModule.canInven = false;
            _mainModule.isBattle = true;
            _battleUI.cemetryBtn.SetActive(true);
            _battleUI.pickCardBtn.SetActive(true);
            _battleUI.isCemetery = false;
            _wall.SetActive(false);
            _mainModule.SetBattleAni();
            BattleCameraEffect();
            SetBattleUI();
        });
    }

    public void SpawnMonster()
    {
        if (_mainModule.playerDataSO.killEnemy < maxEnemyCount)
        {
            for (int i = 0; i < GetEnemy(); i++)
            {
                Debug.Log("[이거 실행 된다? ㅋㅋ]");
                GameObject enemyPrefab = PoolManager.Instance.Pop(EnemyType()).gameObject;
                Debug.Log(enemyPrefab);
                fieldEnemies.Add(enemyPrefab);
                fieldEnemies[i].transform.position = _mainModule._enemySpawnPoint[i].position;
                fieldEnemies[i].transform.DOScale(1, 1f);
            }
        }
        else
        {
            if (_mainModule.playerDataSO.isPantograph)
            {
                HpModule hp = GameObject.Find("Player").GetComponent<HpModule>();
                hp.GetHp(25);
            }

            GameObject enemyPrefab = PoolManager.Instance.Pop(PoolType.Boss1 + (_mainModule.playerDataSO.stage - 1)).gameObject;
            enemyPrefab.transform.position = _mainModule._enemySpawnPoint[0].position;
            fieldEnemies.Add(enemyPrefab);
            fieldEnemies[0].transform.DOScale(1, 1f);
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

        Transform[] childList = _battleUI.costParentTrm.GetComponentsInChildren<Transform>();

        if (childList != null)
        {
            for (int i = 1; i < childList.Length; i++)
            {
                Destroy(childList[i].gameObject);
            }
        }
        dirSprite.SetActive(false);

        battleRelicParent.SetActive(false);
        relicParent.SetActive(true);
        bagImage.transform.DOMoveX(1900f, 0.3f);
        settingImage.transform.DOMoveX(1900f, 0.3f);
        _mainModule.playerDataSO.threeCnt = 0;
        if (_mainModule.playerDataSO.isBuringBlood)
        {
            HpModule hp = GameObject.Find("Player").GetComponent<HpModule>();
            hp.GetHp(6);
        }
        ccm.parentTrm.SetActive(true);

        _mainModule.isBattle = false;
        _battleUI.costObj.Clear();
        _mainModule._animator.Play("Win");
        _mainModule._animator.SetBool("Fight", false);
        _mainModule.twoView.Priority += 10;
        _mainModule.battleCam.Priority -= 10;
        _mainModule.playerDataSO.killEnemy++;
        _battleUI.cemetryBtn.SetActive(false);
        _battleUI.pickCardBtn.SetActive(false);
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

        if (_mainModule.playerDataSO.isAnchor)
        {
            HpModule hp = GameObject.Find("Player").GetComponent<HpModule>();
            hp.OnShield(10);
        }
        if (_mainModule.playerDataSO.isBloodVial)
        {
            HpModule hp = GameObject.Find("Player").GetComponent<HpModule>();
            hp.GetHp(2);
        }
    }

    public IEnumerator ChangeTurn(bool isPlayer)
    {
        if (isPlayer) { }

        else
        {
            SkillFunc.AddShield(_mainModule.gameObject, _mainModule.playerDataSO.Def, Color.blue);
            GameObject a = PoolManager.Instance.Pop(PoolType.VFX5).gameObject;
            a.transform.position = _mainModule.transform.position + new Vector3(0, 1, 0);

            playerHitCam.Priority += 100;
            _mainModule._HpModule.TurnEndAbnormalStatus();

            yield return new WaitForSeconds(0.7f);

            for (int i = 0; i < fieldEnemies.Count; i++)
            {
                fieldEnemies[i].GetComponent<HpModule>().shield = 0;
            }
            yield return new WaitForSeconds(1.5f);

            for (int i = 0; i < fieldEnemies.Count; i++)
            {
                fieldEnemies[i].GetComponent<AIModule>().WhatToDo();
                yield return new WaitForSeconds(1f);
                fieldEnemies[i].GetComponent<HpModule>().TurnEndAbnormalStatus();
            }

            _battleUI.cardCount = _battleUI.GetCardCount();
            yield return new WaitForSeconds(0.5f);
           _battleUI.TurnChangeEffect(true);
            yield return new WaitForSeconds(0.2f);
            _battleUI.SetActiveButton(true);

            playerHitCam.Priority -= 100;
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
        Debug.Log((PoolType)enumValues.GetValue(Random.Range((int)PoolType.Enemy1 + ((_mainModule.playerDataSO.stage - 1) * 3), (int)PoolType.Enemy1 + (_mainModule.playerDataSO.stage * 3))));
        return (PoolType)enumValues.GetValue(Random.Range((int)PoolType.Enemy1 + ((_mainModule.playerDataSO.stage - 1) * 3), (int)PoolType.Enemy1 + (_mainModule.playerDataSO.stage * 3)));
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
