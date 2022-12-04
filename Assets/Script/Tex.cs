using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tex : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public Vector3 startPos;
    public Vector3 targetPos;
    public float timer = 0f;
    public float duration = 3f;

    void Start()
    {
        startPos = new Vector3(0, 0, 0);
        targetPos = startPos + new Vector3(10, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float p = timer / duration;
        transform.position = Vector3.Slerp(startPos, targetPos, p);
    }
}
