using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public float zoomSpeed = 10.0f;

    private CinemachineVirtualCamera nomalCom;

    void Start()
    {
        nomalCom = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        float size = nomalCom.m_Lens.OrthographicSize;

        if (distance != 0)
        {
            size += distance;
            if (size <= 1 || size >= 6)
                size -= distance;
        }

        nomalCom.m_Lens.OrthographicSize = size;
    }
}
