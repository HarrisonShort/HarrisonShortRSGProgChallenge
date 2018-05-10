using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class that takes care of destroying particle systems
/// when they have reached their respective durations
/// </summary>
public class ParticleSystemSelfDestruct : MonoBehaviour
{
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particleSystem)
        {
            if (!particleSystem.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
