using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstObject : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
