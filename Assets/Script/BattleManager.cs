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
        _mainModule.nomalCam.Priority -= 10;
    }

    public void SetBattleUI()
    {
        _battleUI.SetBattleUI();
    }

    public bool SetTurn()
    {
        if (_mainModule.playerDataSO.Speed >= fieldEnemies[0].GetComponent<EnemyData>()._speed)
            return true;
        else return false;
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
