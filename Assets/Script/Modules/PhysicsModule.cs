using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsModule : MonoBehaviour
{
    public List<Collider> colliders;

    private MainModule mainModule;

    private void Awake()
    {
        mainModule = GetComponent<MainModule>();
    }
    public int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chest"))
        {
            string[] a = other.transform.parent.name.Split("_");
            index = a[1][0] - '0';
        }
    }
}
