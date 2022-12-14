using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    public GameObject followObj;

    
    private void Update()
    {
        Debug.Log(Camera.main.ScreenToWorldPoint(followObj.transform.position));
        transform.position = followObj.transform.position + new Vector3(1, 1,-1);
    }
}
