using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class MainModule : MonoBehaviour
{
    [Space]
    [Header("�÷��̾� ������")]
    public PlayerDataSO playerDataSO;

    [Space]
    [Header("���� ī�޶��")]
    public Camera playerCam;
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera vCam3;

    [Space]
    [Header("�� ī�޶��")]
    public CinemachineVirtualCamera nomalCam;
    public CinemachineVirtualCamera battleCam;

    [Space]
    [Header("�� ��ȯ��ġ")]
    public List<Transform> _enemySpawnPoint = new List<Transform>();

    [Space]
    [Header("���� ������")]
    public float minTime;
    public float maxTime;

    [Space]
    [Header("�÷��̾� ������ �� �ֳ�")]
    public bool canMove;

    private MapManager _mapManager;

    private MoveModule _moveModule;
    private InputModule _inputModule;
    private UIModule _uIModule;
    private BattleMobule _battleMobule;

    public MoveModule _MoveModule => _moveModule;
    public InputModule _InputModule => _inputModule;
    public UIModule _UIModule => _uIModule;
    public BattleMobule _BattleModule => _battleMobule;

    private Animator _animator;

    private float playerSpeed;
    private float battleTime;

    private void Awake()
    {
        _moveModule = GetComponent<MoveModule>();
        _inputModule = GetComponent<InputModule>();
        _uIModule = GetComponent<UIModule>();
        _animator = GetComponent<Animator>();
        _battleMobule = GetComponent<BattleMobule>();
        _mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        playerCam = Camera.main;
        SetBattleDelay();
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _enemySpawnPoint.Add(transform.GetChild(i + 7));
        }
    }

    private void Update()
    {
        _animator.SetFloat("Speed", playerSpeed);

        if(_moveModule.moveTime >= battleTime)
        {
            _battleMobule.StartBattle();
            _moveModule.moveTime = 0;
            SetBattleDelay();
        }
    }

    public void SetStat()
    {

    }

    public void MovePlayer(Vector3 direc, bool isRun)
    {
        _moveModule.MovePlayer(direc, playerSpeed);

        if (direc != Vector3.zero && !canMove)
        {
            playerSpeed = isRun ? playerDataSO.runSpeed : playerDataSO.walkSpeed;
            return;
        }

        playerSpeed = 0;
    }

    public void SetBattleDelay()
    {
        battleTime = Random.Range(minTime, maxTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ToRoom"))
        {
            //Time.timeScale = 0f;
            canMove = true;
            playerDataSO.canBattle = true;
            _mapManager.StartInit();
        }
    }
}
