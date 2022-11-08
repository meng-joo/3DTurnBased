using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    MainModule mainModule;

    private void Start()
    {
        mainModule = GetComponent<MainModule>();
    }

    private void Update()
    {
        InputMove();
        InputUI();
    }

    void InputMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isRun = Input.GetKey(KeyCode.LeftShift);

        Vector3 direc = Vector3.zero;

        Vector3 forward = mainModule.playerCam.transform.TransformDirection(Vector3.forward);
        forward.y = 0f;
        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        direc = (forward * z + right * x).normalized;//.normalized; // πÊ«‚

        mainModule.MovePlayer(direc, isRun);
        //RotateBody(_amount);
    }

    void InputUI()
    {
        if (Input.GetKeyDown($"{mainModule._UIModule.KeyName}") && mainModule._UIModule.canInteration)
        {
            mainModule.canMove = true;
            mainModule._UIModule._uiManager.SignUIOn();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && mainModule._UIModule._uiManager.isSignUp)
        {
            UndoSignImage();
        }
    }

    public void UndoSignImage()
    {
        mainModule.canMove = false;
        mainModule._UIModule._uiManager.SignUIOff();
    }
}
