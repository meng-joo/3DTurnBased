using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector3 camPosition;
    public Vector3 camRotation;
    public Transform followTarget;

    private void LateUpdate()
    {
        if (followTarget == null)
        {
            return;
        }

        transform.position = followTarget.position + camPosition;
        transform.localRotation = Quaternion.Euler(camRotation);
    }
}
