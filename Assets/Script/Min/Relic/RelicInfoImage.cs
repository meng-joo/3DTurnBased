using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInfoImage : MonoBehaviour
{
    RectTransform thispos;
    Vector3 pos;

    private void Start()
    {
        thispos = GetComponent<RectTransform>();
        float a = thispos.rect.height / 2;
        pos = new Vector3(0, -a - 10, 0);
    }
    void Update()
    {
        transform.position = Input.mousePosition + pos;
    }
}
