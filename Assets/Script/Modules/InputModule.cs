using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    public bool canMove;

    MainModule mainModule;
    private void Start()
    {
        mainModule = GetComponent<MainModule>();
    }

    private void Update()
    {
        InputMove();
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

        direc = (forward * z + right * x);//.normalized; // πÊ«‚

        mainModule.MovePlayer(direc, isRun);
        //RotateBody(_amount);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("SignPanel"))
        {
            mainModule._UIModule.OnInteractionKeyImage(true);

            if (Input.GetKeyDown(KeyCode.F))
            {

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SignPanel"))
        {
            //mainModule._UIModule.OnInteractionKeyImage(false);
        }
    }
}
