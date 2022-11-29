using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEnd : MonoBehaviour
{
    ParticleSystem particle;
    public PoolType poolType;

    bool isOne = false;

    private void OnDisable()
    {
        particle = GetComponent<ParticleSystem>();
        isOne = true;
    }

    private void OnEnable()
    {
        if (isOne)
        {
            particle.Play();
            Invoke("EndParticle", particle.duration);
        }
    }

    private void EndParticle()
    {
        PoolManager.Instance.Push(poolType, gameObject);
    }
}
