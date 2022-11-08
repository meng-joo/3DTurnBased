using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCol : MonoBehaviour
{
    [SerializeField] string _keyName;
    [SerializeField] string _behave;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<MainModule>()._UIModule.OnInteractionKeyImage(true, _behave, _keyName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<MainModule>()._UIModule.OnInteractionKeyImage(false, "", _keyName);
        }
    }
}
