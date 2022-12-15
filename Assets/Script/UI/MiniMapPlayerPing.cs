using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapPlayerPing : MonoBehaviour
{
    private void OnEnable()
    {
        Quaternion rot = Quaternion.Euler(90, transform.rotation.y - transform.parent.rotation.y, 0);

        transform.rotation = rot;
    }
}