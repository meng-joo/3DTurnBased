using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BattleManager : MonoBehaviour
{
    MainModule _mainModule;

    public BattleUI _battleUI;

    public AllEnemySO currentEnemys;
    public List<GameObject> fieldEnemies;

    public GameObject _wall;

    [Space]
    [Header("적 생성 범위 지정")]
    public float enemySpawnMinPoint;
    public float enemySpawnMaxPoint;

    private void Start()
    {
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
            _mainModule.canInven = false;
            _battleUI.cemetryBtn.SetActive(true);
            _battleUI.pickCardBtn.SetActive(true);
            _battleUI.isCemetery = false;
            _wall.SetActive(false);
            _mainModule.SetBattleAni();
            Debug.Log("이런 씨발");
            BattleCameraEffect();
            SetBattleUI();
        });
    }

    public void SpawnMonster()
    {
        _mainModule.playerCam.cullingMask &= ~(1 << LayerMask.NameToLayer("Wall"));
        
        for (int i = 0; i < 1; i++)
        {
            GameObject enemyPrefab = currentEnemys.floor_1[0]._enemyModle;
            Debug.Log(enemyPrefab);
            fieldEnemies.Add(Instantiate(enemyPrefab, _mainModule._enemySpawnPoint[i].position, Quaternion.identity));
            fieldEnemies[i].transform.DOScale(1, 1f);
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
        if (isPlayer) { }

        else
        {
            yield return new WaitForSeconds(3.8f);

            for (int i = 0; i < fieldEnemies.Count; i++)
            {
                fieldEnemies[i].GetComponent<AIModule>().WhatToDo();
                yield return new WaitForSeconds(2.3f);
            }

            _battleUI.cardCount = _battleUI.GetCardCount();
            yield return new WaitForSeconds(1f);
           _battleUI.TurnChangeEffect(true);
            yield return new WaitForSeconds(0.5f);
            _battleUI.SetActiveButton(true);
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
        yield return new WaitForSeconds(1.5f);
        _mainModule._UIModule.TrophyUIManager.AppearTrophy();
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
