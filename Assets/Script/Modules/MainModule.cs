using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

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
    [Header("플레이어 움직일 수 있냐")]
    public bool canMove;

    private MapManager _mapManager;

    private MoveModule _moveModule;
    private InputModule _inputModule;
    private UIModule _uIModule;

    public MoveModule _MoveModule => _moveModule;
    public InputModule _InputModule => _inputModule;
    public UIModule _UIModule => _uIModule;

    private Animator _animator;

    private float playerSpeed;

    private void Awake()
    {
        _moveModule = GetComponent<MoveModule>();
        _inputModule = GetComponent<InputModule>();
        _uIModule = GetComponent<UIModule>();
        _animator = GetComponent<Animator>();
        _mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        playerCam = Camera.main;
    }

    private void Update()
    {
        _animator.SetFloat("Speed", playerSpeed);
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("왜ㅐㅐㅐㅐㅐ안돼ㅐㅐㅐㅐ");
        if (other.tag == "1floor")
        {
            vCam1.Priority = 20;
            vCam2.Priority = 10;
            vCam3.Priority = 10;
        }
        else if (other.tag == "2floor")
        {
            vCam1.Priority = 10;
            vCam2.Priority = 20;
            vCam3.Priority = 10;
        }
        else if (other.CompareTag("3floor"))
        {
            vCam1.Priority = 10;
            vCam2.Priority = 10;
            vCam3.Priority = 20;
        }
        else if(other.CompareTag("ToRoom"))
        {
            //Time.timeScale = 0f;
            _mapManager.StartInit();
        }
    }
}
