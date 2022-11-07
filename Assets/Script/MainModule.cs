using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainModule : MonoBehaviour
{
    public PlayerDataSO playerDataSO;

    private MoveModule _moveModule;
    private InputModule _inputModule;

    private Animator _animator;

    private float playerSpeed;

    public Camera playerCam;

    private void Awake()
    {
        _moveModule = GetComponent<MoveModule>();
        _inputModule = GetComponent<InputModule>();
        _animator = GetComponent<Animator>();
        playerCam = Camera.main;
    }

    private void Update()
    {
        _animator.SetFloat("Speed", playerSpeed);
    }

    public void MovePlayer(Vector3 direc, bool isRun)
    {
        if (direc != Vector3.zero)
        {
            playerSpeed = isRun ? playerDataSO.runSpeed : playerDataSO.walkSpeed;
            _moveModule.MovePlayer(direc, playerSpeed);
            return;
        }

        playerSpeed = 0;
    }
}
