using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterationCol : MonoBehaviour
{
    [SerializeField] string _keyName;
    [SerializeField] string _behave;
    [SerializeField] string _func;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("A");
            other.GetComponent<MainModule>()._UIModule.OnInteractionKeyImage(true, _behave, _keyName, _func);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MainModule>()._UIModule.OnInteractionKeyImage(false, "", _keyName, _func);
        }
    }
}
