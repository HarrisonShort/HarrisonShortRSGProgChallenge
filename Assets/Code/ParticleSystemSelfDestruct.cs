using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that takes care of destroying particle systems
/// when they have reached their respective durations
/// </summary>
public class ParticleSystemSelfDestruct : MonoBehaviour
{
    private ParticleSystem thisParticleSystem;

    void Start()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (thisParticleSystem)
        {
            if (!thisParticleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
