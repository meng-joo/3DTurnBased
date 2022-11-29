using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Playables;

public class MainModule : MonoBehaviour
{
    [Space]
    [Header("플레이어 데이터")]
    public PlayerDataSO playerDataSO;

    [Space]
    [Header("복도 카메라들")]
    public Camera playerCam;
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera vCam3;

    [Space]
    [Header("방 카메라들")]
    public CinemachineVirtualCamera nomalCam;
    public CinemachineVirtualCamera battleCam;

    [Space]
    [Header("적 소환위치")]
    public List<Transform> _enemySpawnPoint = new List<Transform>();

    [Space]
    [Header("전투 딜레이")]
    public float minTime;
    public float maxTime;

    [Space]
    [Header("플레이어 움직일 수 있냐")]
    public bool canMove;

    //[Space]
    //[Header("애니메이션")]
    //public Animation _animation;

    private MapManager _mapManager;

    private MoveModule _moveModule;
    private InputModule _inputModule;
    private UIModule _uIModule;
    private BattleMobule _battleMobule;
    private HpModule _hpModule;

    public HpModule _HpModule => _hpModule;
    public MoveModule _MoveModule => _moveModule;
    public InputModule _InputModule => _inputModule;
    public UIModule _UIModule => _uIModule;
    public BattleMobule _BattleModule => _battleMobule;
    public MapManager MapManager => _mapManager;

    public Animator _animator;

    private float playerSpeed;
    private float battleTime;

    public Animator chestAnimator;
    public AnimatorOverrideController _animatorOverride;
    [SerializeField] private ParticleSystem chestOpenParticle;
    
    public Animator ChestAnimator => chestAnimator;
    public ParticleSystem ChestOpenParticle => chestOpenParticle;

    private void Awake()
    {
        _moveModule = GetComponent<MoveModule>();
        _inputModule = GetComponent<InputModule>();
        _uIModule = GetComponent<UIModule>();
        _animator = GetComponent<Animator>();
        _battleMobule = GetComponent<BattleMobule>();
        _hpModule = GetComponent<HpModule>();
        _mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        _animatorOverride = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = _animatorOverride;
        //_animatorOverride = GetComponent<AnimatorOverrideController>();
        playerCam = Camera.main;
        SetBattleDelay();
    }

    private void Start()
    {
        _hpModule.SetAvtiveHpbar(false);

        _hpModule.InitHP(playerDataSO.Health, playerDataSO.Health);

        for (int i = 0; i < 3; i++)
        {
            _enemySpawnPoint.Add(transform.GetChild(i + 6));
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
            _mapManager.StartInit(1);
        }
    }
}
